using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool hasBeenPickedup = false;

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
        if(other.transform.tag == "Character")
        {
            if(other.GetComponent<Character>().isAlive == false) return;

            managerUI.Pickup(!hasBeenPickedup);
            if(Input.GetButton("Input E") && !hasBeenPickedup)
            {
                // pickup trowable
                print("pcikup");
                hasBeenPickedup = true;
                other.transform.GetComponent<Character>().trowableAmount += 1;
                managerUI.Pickup(false);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Character")
        {
            managerUI.Pickup(false);
        }
    }
}
