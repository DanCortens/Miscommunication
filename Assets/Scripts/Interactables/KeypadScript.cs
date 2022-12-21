using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadScript : Interactable
{
    [SerializeField] private AudioSource dink;
    [SerializeField] private AudioSource beep;

    [SerializeField] private GameObject keypadDisplay;
    [SerializeField] private GameObject keypad;

    public RoomGenerator rg;

    string playerAnswer = "";
    
    // Start is called before the first frame update
    void Start()
    {
        keypad.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("x"))
        {
            keypad.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerCam.inMenu = false;
            PlayerMovement.inMenu = false;
        }
    }
    public void EnterCombo(int number)
    {
        beep.Play();
        
        if (number == 11)
        {
            playerAnswer = "";
        }
        else if (number == 10)
        {
            //send string to room to check if valid
            rg.CheckInput(playerAnswer);
        }
        else
        {
            if (playerAnswer.Length < 4)
            {
                playerAnswer += number.ToString();
            }
        }
        keypadDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = playerAnswer;
        
    }
    public override void Interact()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PlayerCam.inMenu = true;
        PlayerMovement.inMenu = true;
        keypad.SetActive(true);
        beep.Play();
    }
}
