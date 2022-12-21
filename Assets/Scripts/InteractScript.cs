using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public GameObject interactMessage;
    public GameObject keyboardBg;
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
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4, interactableMask))
        {
            Debug.Log($"Dist: {hit.distance}");
            interactMessage.SetActive(true);
            if (Input.GetKeyDown("f"))
            {
                hit.transform.gameObject.GetComponent<Interactable>().Interact();
            }
        }
        else
        {
            interactMessage.SetActive(false);
        }
    }
}
