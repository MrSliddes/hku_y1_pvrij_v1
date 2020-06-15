using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDuct : MonoBehaviour
{
    public Transform newPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetButtonDown("Input E"))
        {
            if(other.tag == "Character")
            {
                if(other.GetComponent<Zusje>() != null)
                {

                }
            }
        }
    }
}
