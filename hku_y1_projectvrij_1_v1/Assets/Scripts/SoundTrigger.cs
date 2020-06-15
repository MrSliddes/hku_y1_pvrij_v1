using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SoundTrigger : MonoBehaviour
{
    public bool triggerOnce = true;
    private bool hasTriggerd = false;

    public GameObject particleSound;

    public AudioSource creak;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            if(triggerOnce)
            {
                if(!hasTriggerd)
                {
                    hasTriggerd = true;
                    GameObject.FindWithTag("Target Monster").transform.position = transform.position;
                    Instantiate(particleSound, transform.position, Quaternion.identity);
                    if(creak != null) creak.Play();
                }
            }
            else
            {
                GameObject.FindWithTag("Target Monster").transform.position = transform.position;
                Instantiate(particleSound, transform.position, Quaternion.identity);
            }
        }
    }
}
