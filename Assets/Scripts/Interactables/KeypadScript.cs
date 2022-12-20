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
    [SerializeField] private Door _door;
    [SerializeField] private LeverList leverList;
    public Door ConnectedDoor { get { return _door; } set { _door = value; } }

    bool doorOpenFlag = false;

    [SerializeField] private string answer = "12345";
    string playerAnswer = "";
    
    bool keypadSolved = false;
    // Start is called before the first frame update
    void Start()
    {
        Button button = GameObject.Find("key1").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(1); });
        button = GameObject.Find("key2").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(2); });
        button = GameObject.Find("key3").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(3); });
        button = GameObject.Find("key4").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(4); });
        button = GameObject.Find("key5").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(5); });
        button = GameObject.Find("key6").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(6); });
        button = GameObject.Find("key7").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(7); });
        button = GameObject.Find("key8").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(8); });
        button = GameObject.Find("key9").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(9); });
        button = GameObject.Find("key0").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(0); });
        button = GameObject.Find("Enter").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(10); });
        button = GameObject.Find("Back").GetComponent<Button>();
        button.onClick.AddListener(delegate { EnterCombo(11); });
        keypad.SetActive(false);

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
            PlayerMovement.inMenu = false;
        }
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
                dink.Play();
                keypadSolved = true;
              //Return success
            }
        }
        else
        {
            playerAnswer += number.ToString();
        }
        keypadDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = playerAnswer;
        if(doorOpenFlag==false && keypadSolved && leverList.solved)
        {
            doorOpenFlag = true;
            _door.UnlockDoor();
        }
    }
    public void Beep()
    {
        beep.Play();
    }

    public override void Interact()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PlayerCam.inMenu = true;
        PlayerMovement.inMenu = true;
        keypad.SetActive(true);
        Beep();
    }
}
