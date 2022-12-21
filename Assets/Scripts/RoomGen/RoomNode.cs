using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject[] floor;
    [SerializeField] private GameObject[] nodes;
    [SerializeField] private GameObject[] shapes;
    [SerializeField] private GameObject roof;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject[] doorObs;
    public int type;
    
    public void UpdatePanels(bool[] status, int shape)
    {
        //loop through the panels, if status is false set to inactive
        for (int i = 0; i < status.Length; i++)
        {
            panels[i].SetActive(!status[i]);
            if (panels[i].activeInHierarchy)
            {
                //if active, instantiate the appropriate shape on the wall
                GameObject newShape = Instantiate(shapes[shape], nodes[i].transform);
            }
        }
            
    }
    public void CreateExit(bool deathDoor, Color col, Vector3 secOff)
    {
        
        panels[2].SetActive(false);
        nodes[2].SetActive(false);
        door.SetActive(true);
        door.GetComponent<Door>().deathDoor = deathDoor;
        door.GetComponent<Door>().secondaryOffset = secOff;
        doorObs[0].GetComponent<Renderer>().material.color = col;
        doorObs[1].GetComponent<Renderer>().material.color = col;
    }
    public void CreateEntrance()
    {
        panels[3].SetActive(false);
        nodes[3].SetActive(false);
    }
    public void SetColours(int type)
    {
        this.type = type;
        switch (type)
        {
            case 1:
                roof.GetComponent<Renderer>().material.color = new Color(1.0f, 0.6f, 0.0f);
                //floor.GetComponent<Renderer>().material.color = Color.blue;
                foreach (GameObject f in floor)
                    f.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 2:
                roof.GetComponent<Renderer>().material.color = Color.blue;
                //floor.GetComponent<Renderer>().material.color = Color.blue;
                foreach (GameObject f in floor)
                    f.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 3:
                roof.GetComponent<Renderer>().material.color = Color.blue;
                foreach (GameObject p in panels)
                    p.GetComponent<Renderer>().material.color = new Color(1.0f, 0.6f, 0.0f);
                break;
            case 4:
                roof.GetComponent<Renderer>().material.color = new Color(1.0f, 0.6f, 0.0f);
                foreach (GameObject p in panels)
                    p.GetComponent<Renderer>().material.color = Color.blue;
                break;
        }
    }
}
