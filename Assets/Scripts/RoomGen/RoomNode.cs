using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
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
}
