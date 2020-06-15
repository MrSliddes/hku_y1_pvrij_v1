using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankOppakken : MonoBehaviour
{
    public static bool hasPlank = false;

    public GameObject plankMuur;

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
            managerUI.Pickup(true);
            if(Input.GetButtonDown("Input E"))
            {
                if(hasPlank == false)
                {
                    hasPlank = true;
                    plankMuur.SetActive(false);
                    gameObject.SetActive(false);
                    managerUI.Pickup(false);
                }
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
