using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public enum PuzzleType
    {
        Lever,
        Keypad
    }
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }
    public Vector2 size;
    public int startPos = 0;
    private List<Cell> board;
    [SerializeField] private GameObject roomNode;
    [SerializeField] private GameObject room;
    [SerializeField] private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        PathGeneration();
    }

    private void GenerateRoom()
    {
        PuzzleType puzzle = (PuzzleType)Random.Range(0, 2);
        
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject newNode = Instantiate(roomNode, (new Vector3(i * offset.x, 0f, -j * offset.y) + transform.position), Quaternion.identity, transform);
                newNode.GetComponent<RoomNode>().UpdatePanels(board[Mathf.FloorToInt(i+j*size.x)].status);
                newNode.name = $"Node {i}-{j}";

                if (puzzle == PuzzleType.Lever)
                    newNode.GetComponent<RoomNode>().SetColours(Color.red);
                else if (puzzle == PuzzleType.Keypad)
                    newNode.GetComponent<RoomNode>().SetColours(Color.green);

                if (j == 5)
                {
                    if (i == 10)
                        newNode.GetComponent<RoomNode>().CreateExit();
                    else if (!gameObject.name.Contains("Start") && i == 0)
                        newNode.GetComponent<RoomNode>().CreateEntrance();
                }
            }
        }
    }
    private void PathGeneration()
    {
        board = new List<Cell>();
        for (int i = 0; i < size.x * size.y; i++)
            board.Add(new Cell());

        int currCell = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;
        while(true)
        {
            board[currCell].visited = true;
            List<int> neighbours = CheckNeighbours(currCell);
            if (neighbours.Count == 0)
            {
                if (path.Count == 0)
                    break;
                else
                {
                    currCell = path.Pop();
                }
            }
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
        GenerateRoom();
    }
    private List<int> CheckNeighbours(int cell)
    {
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
}
