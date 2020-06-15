using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KeyTrigger : MonoBehaviour
{
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
        if(other.tag == "Character")
        {
            managerUI.Pickup(!Player.hasKey);
            if(Input.GetAxis("Input E") == 1 && !Player.hasKey)
            {
                Player.hasKey = true;
                managerUI.Pickup(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Character")
        {
            managerUI.Pickup(false);
        }
    }
}
