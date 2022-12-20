using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadScript : MonoBehaviour
{
    public AudioClip dink;
    public static string leverList = "";
    bool doorOpenFlag = false;
    public AudioClip doorOpen;
    public AudioClip beep;
    AudioSource source;
    string leverAnswer = "132";
    string answer = "12345";
    string playerAnswer = "";
    public GameObject keypadDisplay;
    public GameObject keypad;
    public Animator door;
    bool leverSolved = false;
    bool keypadSolved = false;
    // Start is called before the first frame update
    void Start()
    {
        keypad.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leverList.Length >= 3)
        {
            if(string.Compare(leverList, leverAnswer)==0){
                leverSolved = true;
                source.PlayOneShot(dink);
            }
            leverList = "";
        }
        if (Input.GetKeyDown("escape"))
        {
            keypad.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerCam.inMenu = false;
        }
        if (doorOpenFlag == false && keypadSolved && leverSolved)
        {
            doorOpenFlag = true;
            source.PlayOneShot(doorOpen);
            door.Play("DoorOpen");
        }
    }
    public void DisplayLayer()
    {

    }
    public void EnterCombo(int number)
    {
        if (number == 11)
        {
            playerAnswer = "";
        }
        else if (number == 10)
        {
            if(string.Equals(playerAnswer, answer))
            {
                source.PlayOneShot(dink);
                keypadSolved = true;
              //Return success
            }
        }
        else
        {
            playerAnswer += number.ToString();
        }
        keypadDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = playerAnswer;
    }
    public void Beep()
    {
        source.PlayOneShot(beep);
    }
}
