using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    //enumerator defining possible shape types
    public enum ShapeType
    {
        Triangle,
        Square,
        Diamond,
        Circle,
        X
    }
    //definition of a cell for room generation
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [Header("Level Generation")]
    public Vector2 size;
    public int startPos = 0;
    private List<Cell> board;
    [SerializeField] private GameObject roomNode;
    [SerializeField] private GameObject room;
    [SerializeField] private Vector2 offset;
    [Header("Puzzle Generation")]
    [SerializeField] private ShapeType rType;
    [SerializeField] private int currStep = 0;
    [SerializeField] private List<string> solutionSteps = new List<string>();
    [SerializeField] private GameObject puzzle;

    // Start is called before the first frame update
    void Start()
    {
        //on start, generate a path
        PathGeneration();
    }

    private void GenerateRoom()
    {
        rType = (ShapeType)Random.Range(0, 5);
        int roomType = 1;
        int localRoomType;

        Vector2Int puzzleLoc = RandomLocation();
        for (int i = 0; i < size.x; i++)
        {
            if (i < ((size.x - 1) / 2))
            {
                roomType = 1;
            }
            else
            {
                roomType = 3;
            }
            for (int j = 0; j < size.y; j++)
            {
                if (j < ((size.y - 1) / 2))
                {
                    localRoomType = roomType;
                }
                else
                {
                    localRoomType = roomType + 1;
                }
                GameObject newNode = Instantiate(roomNode, (new Vector3(i * offset.x, 0f, -j * offset.y) + transform.position), Quaternion.identity, transform);
                newNode.GetComponent<RoomNode>().UpdatePanels(board[Mathf.FloorToInt(i+j*size.x)].status,(int)rType);
                newNode.name = $"Node {i}-{j}";
                newNode.GetComponent<RoomNode>().SetColours(localRoomType);
                //if on the node that the puzzle is in, instantiate the puzzle
                if (i == puzzleLoc.x && j == puzzleLoc.y)
                {
                    GameObject newPuzzle = Instantiate(puzzle, newNode.transform);
                    newPuzzle.GetComponent<Puzzle>().rg = gameObject.GetComponent<RoomGenerator>();
                    SetSolution(localRoomType);
                }
                    
                if (j == (size.y - 2)) 
                {
                    if (i == (size.x - 1))
                    {
                        bool death = (rType == ShapeType.X);
                        newNode.GetComponent<RoomNode>().CreateExit(death, Color.blue, new Vector3 (0f,0f,10.1f));
                    }
                        
                    else if (!gameObject.name.Contains("Start") && i == 0)
                        newNode.GetComponent<RoomNode>().CreateEntrance();
                }
                else if (j == 1)
                {
                    if (i == (size.x - 1))
                    {
                        bool death = (rType == ShapeType.Circle);
                        newNode.GetComponent<RoomNode>().CreateExit(death, new Color(1.0f, 0.6f, 0.0f), new Vector3(0f, 0f, -10.1f));
                    }

                    else if (!gameObject.name.Contains("Start") && i == 0)
                        newNode.GetComponent<RoomNode>().CreateEntrance();
                }
            }
        }
    }
    private void PathGeneration()
    {
        //create a new board
        board = new List<Cell>();
        //fill it with a number of cells equal to size.x*size.y
        for (int i = 0; i < size.x * size.y; i++)
            board.Add(new Cell());
        //set current cell
        int currCell = startPos;
        //create a stack for the path
        Stack<int> path = new Stack<int>();
        
        while(true)
        {
            //mark current cell as visited
            board[currCell].visited = true;
            //check if neighbours have been visited
            List<int> neighbours = CheckNeighbours(currCell);
            //if no neighbours:
            if (neighbours.Count == 0)
            {
                //if there is nothing left in the path, end the loop
                if (path.Count == 0)
                    break;
                else
                {
                    currCell = path.Pop();
                }
            }
            //there are neighbours:
            else
            {
                path.Push(currCell);
                int newCell = neighbours[Random.Range(0, neighbours.Count)];
                if (newCell > currCell)
                {
                    if (newCell - 1 == currCell)
                    {
                        board[currCell].status[2] = true;
                        currCell = newCell;
                        board[currCell].status[3] = true;
                    }
                    else
                    {
                        board[currCell].status[1] = true;
                        currCell = newCell;
                        board[currCell].status[0] = true;
                    }
                }
                else
                {
                    if (newCell + 1 == currCell)
                    {
                        board[currCell].status[3] = true;
                        currCell = newCell;
                        board[currCell].status[2] = true;
                    }
                    else
                    {
                        board[currCell].status[0] = true;
                        currCell = newCell;
                        board[currCell].status[1] = true;
                    }
                }
            }
        }
        //when complete, generate rooms based on path
        GenerateRoom();
    }
    private List<int> CheckNeighbours(int cell)
    {
        //check each neighbouring node to see if the path visited it during generation
        List<int> neighbours = new List<int>();
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visited)
            neighbours.Add(Mathf.FloorToInt(cell - size.x));
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
            neighbours.Add(Mathf.FloorToInt(cell + size.x));
        if ((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
            neighbours.Add(Mathf.FloorToInt(cell + 1));
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
            neighbours.Add(Mathf.FloorToInt(cell - 1));
        return neighbours;
    }

    private void SetSolution(int nodeType)
    {
        //first check the room type
        string keycode = "";
        string[] levercode = new string[4];

        switch (nodeType)
        {
            case 1:
                keycode = "1928";
                levercode[0] = "L1";
                levercode[1] = "L4";
                levercode[2] = "L2";
                levercode[3] = "L3";
                break;
            case 2:
                keycode = "0287";
                levercode[0] = "L3";
                levercode[1] = "L4";
                levercode[2] = "L1";
                levercode[3] = "L1";
                break;
            case 3:
                keycode = "7829";
                levercode[0] = "L2";
                levercode[1] = "L2";
                levercode[2] = "L4";
                levercode[3] = "L1";
                break;
            case 4:
                keycode = "9240";
                levercode[0] = "L3";
                levercode[1] = "L1";
                levercode[2] = "L4";
                levercode[3] = "L2";
                break;
        }
        //next check for wall decoration (note, circle and x are tied to doors)
        if (rType == ShapeType.Triangle)
        {
            //if triangle, swap order of the steps
            if (nodeType == 1 || nodeType == 4)
            {
                foreach (string lc in levercode)
                    solutionSteps.Add(lc);
                solutionSteps.Add(keycode);
            }
            else
            {
                solutionSteps.Add(keycode);
                foreach (string lc in levercode)
                    solutionSteps.Add(lc);
            }
        }
        else
        {
            if (rType == ShapeType.Square)
            {
                //if shape is square, reverse keycode
                char[] chars = keycode.ToCharArray();
                System.Array.Reverse(chars);
                keycode = new string(chars);
            }
            else if (rType == ShapeType.Diamond)
            {
                //if shape is diamond, reverse levercode
                System.Array.Reverse(levercode);
            }
            //add steps to solution
            if (nodeType == 1 || nodeType == 4)
            {
                solutionSteps.Add(keycode);
                foreach (string lc in levercode)
                    solutionSteps.Add(lc);
            }
            else
            {
                foreach (string lc in levercode)
                    solutionSteps.Add(lc);
                solutionSteps.Add(keycode);
            }
        }
    }
    public void CheckInput(string inp)
    {
<<<<<<< HEAD
<<<<<<< HEAD
=======
/*        Debug.Log($"input: {inp} solutionstep: {solutionSteps[currStep]}");*/
>>>>>>> origin/Final
=======
        Debug.Log($"input: {inp} solutionstep: {solutionSteps[currStep]}");
>>>>>>> parent of 2adc93f (Final)
        //if the player enters too many inputs, they lose
        if (currStep >= solutionSteps.Count)
        {
            //game over
            FindObjectOfType<UIManager>().GameOver();
        }
        else if (inp == solutionSteps[currStep])
        {
            //if the input matches solution, increment current step
            currStep++;
            if (currStep == solutionSteps.Count)
            {
                //if the player has done all steps, unlock all doors
                Door[] doors = FindObjectsOfType<Door>();
                foreach (Door d in doors)
                    d.UnlockDoor();
            }
        }
        else
        {
            //game over
            FindObjectOfType<UIManager>().GameOver();
        }
    }

    private Vector2Int RandomLocation()
    {
        //create a random integer vector to select the position of the puzzle
        Vector2Int loc = new Vector2Int(0,0);
        while (loc.x == 0 && loc.y == 0)
            loc = new Vector2Int(Random.Range(0, (int)size.x - 1), Random.Range(0, (int)size.y - 1));
        return loc;
    }
}