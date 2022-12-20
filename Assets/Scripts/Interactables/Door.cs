using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private GameObject nextRoom;
    private Animator anim;
    [SerializeField] private bool unlocked;
    [SerializeField] private Vector3 offset;
    public override void Interact()
    {
        if (unlocked)
        {
            OpenDoor();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        unlocked = true;
        anim = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        anim.SetTrigger("open");
        GameObject newRoom = Instantiate(nextRoom, transform.position + offset, Quaternion.identity);
        GetComponent<BoxCollider>().enabled = false;
    }
    public void UnlockDoor()
    {
        unlocked = true;
    }
}
