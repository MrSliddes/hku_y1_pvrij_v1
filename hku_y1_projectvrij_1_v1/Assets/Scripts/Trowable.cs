using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trowable : MonoBehaviour
{
    public GameObject particleSound;

    public AudioSource brick;

    // Start is called before the first frame update
    void Start()
    {
        // Trow itself on mouse pos
        /*
        Vector3 clickPos;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f))
        {
            clickPos = hit.point;
            print(clickPos);
        }
        else
        {
            print("no");
            return;
        }

        transform.localEulerAngles = rotation;
        print(transform.localEulerAngles);
        rb = GetComponent<Rigidbody>();
        if(goRight)
        {
            rb.AddForce(transform.right * trowForce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce((transform.right * -1) * trowForce, ForceMode.Impulse);
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject.FindWithTag("Target Monster").transform.position = transform.position;
        Instantiate(particleSound, transform.position, Quaternion.identity);
        brick.Play();
    }
}
