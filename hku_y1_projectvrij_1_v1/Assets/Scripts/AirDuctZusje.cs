using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDuctZusje : MonoBehaviour
{
    public Transform exitPoint;

    private ManagerUI managerUI;

    // Start is called before the first frame update
    void Start()
    {
        managerUI = FindObjectOfType<ManagerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Character Zusje")
        {
            managerUI.Pickup(true);
            if(Input.GetAxis("Input E") > 0)
            {
                other.GetComponent<CharacterController>().enabled = false;
                other.transform.position = exitPoint.position;
                other.GetComponent<CharacterController>().enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Character Zusje")
        {
            managerUI.Pickup(false);
        }
    }
}
