using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KastKlimop : MonoBehaviour
{
    public Transform kastPos;
    public Transform exitPos;

    public bool kastInPositie = false;

    private Vector3 warpPos = Vector3.zero;
    private GameObject broer;

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

    private void LateUpdate()
    {
        if(warpPos != Vector3.zero)
        {
            broer.GetComponent<CharacterController>().enabled = false;
            broer.transform.position = warpPos;
            warpPos = Vector3.zero;
            broer.GetComponent<CharacterController>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Kast Klimop")
        {
            if(!kastInPositie)
            {
                kastInPositie = true;
                other.transform.position = kastPos.position;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Character Broer")
        {
            broer = other.gameObject;
            if(kastInPositie && broer.GetComponent<Character>().isControlled)
            {
                managerUI.Pickup(true);
                if(Input.GetAxis("Input E") > 0)
                {
                    warpPos = exitPos.position;
                    managerUI.Pickup(false);
                }
            }
            else
            {
                managerUI.Pickup(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Character Broer")
        {
            managerUI.Pickup(false);
        }
    }
}
