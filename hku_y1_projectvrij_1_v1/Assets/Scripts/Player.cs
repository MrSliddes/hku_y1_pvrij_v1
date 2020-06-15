using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Character")]
    public GameObject currentCharacterControlling;

    public GameObject[] characters;

    public static bool hasKey = false;

    [Header("Camera")]
    public bool followTargetOnYAxis = true;
    public Vector2 yAxisLimits;
    public bool followTarget = false;
    public float cameraSmoothSpeed = 0.125f;
    public Vector3 cameraOffset;
    private Vector3 velocity = Vector3.zero;

    private ManagerUI managerUI;

    [Header("Level 1")]
    public bool isLevel0 = true;
    public Vector3 cameraOffset_1;
    public int desiredPosXLevel1 = 1;
    public Vector2 yAxisLimitsLevel1;

    [Header("Level 2")]
    public bool isLevel2 = false;
    public Vector3 cameraOffset_2;
    public int desiredPosXLevel2 = 2;
    public Vector2 yAxisLimitsLevel2;

    private bool shakeCam = false;

    [HideInInspector] public static bool charVaderIsAlive = true;
    [HideInInspector] public static bool charZusIsAlive = true;
    [HideInInspector] public static bool charBroerIsAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        hasKey = false;
        managerUI = FindObjectOfType<ManagerUI>();

        QualitySettings.vSyncCount = 1;

        charVaderIsAlive = true;
        charZusIsAlive = true;
        charBroerIsAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCharater();

        // Check all dead
        if(charVaderIsAlive == false && charBroerIsAlive == false && charZusIsAlive == false)
        {
            managerUI.deadScreen.SetActive(true);
        }
    }      

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void SwitchCharater()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.GetComponent<Character>() != null)
                {
                    if(currentCharacterControlling != null)
                    {
                        if(currentCharacterControlling == hit.transform.gameObject) return;
                        currentCharacterControlling.GetComponent<Character>().isControlled = false; // disable old one
                    }

                    currentCharacterControlling = hit.transform.gameObject; // set new one
                    currentCharacterControlling.GetComponent<Character>().isControlled = true;
                    managerUI.currentCharacter = currentCharacterControlling.GetComponent<Character>();
                }
            }
        }
    }

    /// <summary>
    /// Used for ui
    /// </summary>
    /// <param name="index"></param>
    public void ChangeCharacter(int index)
    {
        if(currentCharacterControlling != null) currentCharacterControlling.GetComponent<Character>().isControlled = false; // disable old one
        currentCharacterControlling = characters[index]; // set new one
        currentCharacterControlling.GetComponent<Character>().isControlled = true;
        managerUI.currentCharacter = currentCharacterControlling.GetComponent<Character>();
        managerUI.Pickup(false);
    }

    

    private void FollowTarget()
    {
        if(!followTarget) return;
        if(currentCharacterControlling == null) return;

        Vector3 desiredPos;
        if(isLevel0)
        {
            desiredPos = currentCharacterControlling.transform.position + cameraOffset;
        }
        else if(!isLevel2)
        {
            desiredPos = currentCharacterControlling.transform.position + cameraOffset_1;
        }
        else
        {
            desiredPos = currentCharacterControlling.transform.position + cameraOffset_2;
        }


        if(followTargetOnYAxis)
        {
            if(isLevel0)
            {
                desiredPos.x = 0;
                if(desiredPos.y > yAxisLimits.x)//upper
                {
                    desiredPos.y = yAxisLimits.x;
                }
                else if(desiredPos.y < yAxisLimits.y)//lower
                {
                    desiredPos.y = yAxisLimits.y;
                }
            }
            else if(!isLevel2)
            {
                desiredPos.x = desiredPosXLevel1;
                /*
                if(desiredPos.y > yAxisLimitsLevel1.x)
                {
                    desiredPos.y = yAxisLimitsLevel1.x;
                }
                else if(desiredPos.y < yAxisLimitsLevel1.y)
                {
                    desiredPos.y = yAxisLimitsLevel1.y;
                }*/
            }
            else
            {
                desiredPos.x = desiredPosXLevel2;
            }


        }
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, cameraSmoothSpeed);
        transform.position = smoothPos;
        

        if(shakeCam)
        {
            print("shake");
            transform.localPosition = transform.localPosition + Random.insideUnitSphere * 0.1f;
        }
    }

    public IEnumerator ShakeScreen(float sec)
    {
        shakeCam = true;
        yield return new WaitForSeconds(sec);
        shakeCam = false;
        yield break;
    }
}
