using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverList : MonoBehaviour
{
    public RoomGenerator rg;

    public void LeverTurned(string lever)
    {
        rg.CheckInput($"L{lever}");
    }
}
