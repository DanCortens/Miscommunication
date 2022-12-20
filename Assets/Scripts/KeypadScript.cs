using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadScript : MonoBehaviour
{
    bool doorOpenFlag = false;
    public AudioClip doorOpen;
    public AudioClip beep;
    AudioSource source;
    string answer = "12345";
    string playerAnswer = "";
    public GameObject keypadDisplay;
    public GameObject keypad;
    public Animator door;
    // Start is called before the first frame update
    void Start()
    {
        keypad.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            keypad.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerCam.inMenu = false;
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
            if(string.Equals(playerAnswer, answer) && doorOpenFlag==false)
            {
                source.PlayOneShot(doorOpen);
                door.Play("Door");
                doorOpenFlag = true;
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
