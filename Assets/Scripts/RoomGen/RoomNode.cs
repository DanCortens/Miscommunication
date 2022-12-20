using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject[] floorTiles;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject roof;
    [SerializeField] private GameObject door;
    public void UpdatePanels(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
            panels[i].SetActive(!status[i]);
    }
    public void CreateExit()
    {
        panels[2].SetActive(false);
        door.SetActive(true);
    }
    public void CreateEntrance()
    {
        panels[3].SetActive(false);
    }
    public void SetColours(int type)
    {
        switch (type)
        {
            case 1:
                roof.GetComponent<Renderer>().material.color = new Color(1.0f, 0.64f, 0.0f);
                floor.GetComponent<Renderer>().material.color = Color.blue;
                //foreach (GameObject f in floorTiles)
                //    f.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 2:
                roof.GetComponent<Renderer>().material.color = Color.blue;
                floor.GetComponent<Renderer>().material.color = Color.blue;
                //foreach (GameObject f in floorTiles)
                //    f.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 3:
                roof.GetComponent<Renderer>().material.color = Color.blue;
                foreach (GameObject p in panels)
                    p.GetComponent<Renderer>().material.color = new Color(1.0f, 0.64f, 0.0f);
                break;
            case 4:
                roof.GetComponent<Renderer>().material.color = new Color(1.0f, 0.64f, 0.0f);
                foreach (GameObject p in panels)
                    p.GetComponent<Renderer>().material.color = Color.blue;
                break;
        }
    }
}
