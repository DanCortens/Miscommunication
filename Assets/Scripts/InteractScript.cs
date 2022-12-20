using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    AudioSource source;
    public AudioClip clang;
    public LayerMask leverMask = 9;
    public GameObject interactMessage;
    public GameObject puzzleLayer;
    public LayerMask interactableMask;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
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
                source.PlayOneShot(clang);
            }
        }
        else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1000, leverMask))
        {
            interactMessage.SetActive(true);
            if (Input.GetKeyDown("f"))
            {
                hit.collider.gameObject.GetComponent<Animator>().Play("LeverBackward");
                string num = hit.collider.name;
                KeypadScript.leverList += num;
                source.PlayOneShot(clang);
            }
        }
        else
        {
            interactMessage.SetActive(false);
        }
    }
}
