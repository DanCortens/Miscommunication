using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public LayerMask interactableMask=8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2, interactableMask))
            {
            

            }
        }
    }
}
