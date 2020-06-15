using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_0 : MonoBehaviour
{
    public float attackRange = 2f;
    public Transform target;
    public Transform monsterTransform;
    public SpriteRenderer spr;

    public GameObject[] characters;

    private NavMeshAgent agent;

    public LayerMask monsterFloorDetection;

    //
    public float monsterResetTime = 5f;
    private float monsterResetTimer;
    public Transform resetPosition;
    private Transform currentTarget;
    private Vector3 oldTargetPos;

    [Header("Sound")]
    public AudioSource audioBite;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentTarget = resetPosition;
    }

    // Update is called once per frame
    void Update()
    {   
        if(oldTargetPos != target.position)
        {
            currentTarget = target;
        }
        oldTargetPos = target.position;

        if(currentTarget != resetPosition)
        {
            if(Vector3.Distance(target.position, transform.position) < 1)
            {
                // Close enough
                monsterResetTimer -= Time.deltaTime;
                if(monsterResetTimer <= 0)
                {
                    // Reset to old pos
                    currentTarget = resetPosition;
                }
            }
            else
            {
                monsterResetTimer = monsterResetTime;
            }
        }

        agent.SetDestination(currentTarget.position);

        // Set dir
        if(agent.desiredVelocity.x > 0)
        {
            spr.flipX = false;
        }
        else
        {
            spr.flipX = true;
        }

        // Rotate sprite
        RaycastHit hit;
        if(Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 7, monsterFloorDetection))
        {
            //print(hit.transform.name);
            float r = hit.transform.localEulerAngles.z;
            monsterTransform.localEulerAngles = new Vector3(0, 0, r);
        }

        // Attacking
        Transform closest = null;
        foreach(GameObject a in characters)
        {
            if(closest == null)
            {
                closest = a.transform;
            }
            else
            {
                if(Vector3.Distance(a.transform.position, transform.position) < Vector3.Distance(closest.position, transform.position))
                {
                    // Newer closest
                    closest = a.transform;
                }
            }
        }

        if(closest != null)
        {
            if(Vector3.Distance(closest.position, transform.position) <= attackRange)
            {
                // Attack
                if(closest.GetComponent<Character>().isAlive && closest.GetComponent<Character>().isHidden == false)
                {
                    closest.GetComponent<Character>().TakeDamage();
                    audioBite.Play();
                    print("Character got killed");
                }
            }
        }
    }
}
