using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitPickup : MonoBehaviour
{
    public static int medAmount = 0;

    private bool hasPickedUp = false;

    public GameObject uiIcon;

    // Start is called before the first frame update
    void Start()
    {
        uiIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            if(!hasPickedUp)
            {
                hasPickedUp = true;
                medAmount++;
                uiIcon.SetActive(true);
                GetComponent<MeshRenderer>().enabled = false;
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
