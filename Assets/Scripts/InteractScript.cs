using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public GameObject interactMessage;
    public GameObject puzzleLayer;
    public LayerMask interactableMask;
    // Start is called before the first frame update
    void Start()
    {
        interactMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1000, interactableMask))
        {
            interactMessage.SetActive(true);
            Debug.Log(hit.collider.name);
            if (Input.GetKeyDown("f"))
            {
                puzzleLayer.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                PlayerCam.inMenu = true;
            }
        }
        else
        {
            interactMessage.SetActive(false);
        }
    }
}
