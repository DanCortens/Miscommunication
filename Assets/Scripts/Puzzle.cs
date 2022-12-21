using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private KeypadScript keypad;
    [SerializeField] private LeverList leverList;
    public KeypadScript Keypad { get; set; }
    public RoomGenerator rg;
    private void Start()
    {
        keypad = transform.Find("Keypad").GetComponent<KeypadScript>();
        keypad.rg = rg;
        leverList.rg = rg;
    }
}
