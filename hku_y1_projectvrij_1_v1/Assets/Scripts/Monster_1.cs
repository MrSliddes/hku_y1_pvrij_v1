using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_1 : MonoBehaviour
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
    private Vector3 currentTarget;
    private Vector3 oldTargetPos;

    [Header("Sound")]
    public AudioSource audioBite;
    public AudioSource lightning;

    [Header("Route")]
    public int currentFloor = 0;
    private bool reachedPointA;
    public Transform point_0A, point_0B;
    public Transform point_1A, point_1B;
    public Transform point_2A, point_2B;

    private ManagerUI managerUI;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentTarget = point_0A.position;
        managerUI = FindObjectOfType<ManagerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentFloor)
        {
            case 0:
                if(!reachedPointA)
                {
                    currentTarget = point_0A.position;
                    // Close enough
                    if(Vector3.Distance(transform.position, point_0A.position) < 1)
                    {
                        reachedPointA = true;
                    }
                }
                else
                {
                    currentTarget = point_0B.position;
                    // Close enough
                    if(Vector3.Distance(transform.position, point_0B.position) < 1)
                    {
                        Teleport(1);
                    }
                }
                break;
            case 1:
                if(!reachedPointA)
                {
                    currentTarget = point_1A.position;
                    // Close enough
                    if(Vector3.Distance(transform.position, point_1A.position) < 1)
                    {
                        reachedPointA = true;
                    }
                }
                else
                {
                    currentTarget = point_1B.position;
                    // Close enough
                    if(Vector3.Distance(transform.position, point_1B.position) < 1)
                    {
                        Teleport(2);
                    }
                }
                break;
            case 2:
                if(!reachedPointA)
                {
                    currentTarget = point_2A.position;
                    // Close enough
                    if(Vector3.Distance(transform.position, point_2A.position) < 1)
                    {
                        reachedPointA = true;
                    }
                }
                else
                {
                    currentTarget = point_2B.position;
                    // Close enough
                    if(Vector3.Distance(transform.position, point_2B.position) < 1)
                    {
                        Teleport(0);
                    }
                }
                break;
            default:
                break;
        }

        agent.SetDestination(currentTarget);

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
            print(hit.transform.name);
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

    public void Teleport(int floor)
    {
        reachedPointA = false;
        agent.enabled = false;
        currentFloor = floor;
        if(FindObjectOfType<Player>().isLevel0 == false && FindObjectOfType<Player>().isLevel2 == false)
        {
            managerUI.lightningMonster1.GetComponent<Animator>().Play("lightningEffectMonster1");
            lightning.Play();
        }

        switch(floor)
        {
            case 0:
                transform.position = point_0B.position;
                break;
            case 1:
                transform.position = point_1B.position;
                break;
            case 2:
                transform.position = point_2B.position;
                break;
            default:
                break;
        }

        agent.enabled = true;
    }
}
