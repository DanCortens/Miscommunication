using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private GameObject nextRoom;
    private Animator anim;
    [SerializeField] private bool unlocked;
    [SerializeField] private Vector3 offset;
    [SerializeField] private AudioSource openSound;
    public bool deathDoor = false;
    public Vector3 secondaryOffset;
    public override void Interact()
    {
        //when the player interacts with this object:
        if (unlocked)
        {
            //if the door is a death door, player dies
            if (deathDoor)
            {
                //gameover
                FindObjectOfType<UIManager>().GameOver();
            }
            else
            {
                //open the door
                OpenDoor();
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //doors start off locked; get the animator connected to this object
        unlocked = false;
        anim = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        //play open sound and open animation
        anim.SetTrigger("open");
        openSound.Play();
        //create the next room
        GameObject newRoom = Instantiate(nextRoom, transform.position + offset + secondaryOffset, Quaternion.identity);
        //disable the collider for this door
        GetComponent<BoxCollider>().enabled = false;
        FindObjectOfType<UIManager>().RoomCleared();
    }
    public void UnlockDoor()
    {
        //unlock the door
        unlocked = true;
    }
}
