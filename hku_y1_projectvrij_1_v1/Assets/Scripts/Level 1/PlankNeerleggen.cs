using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlankNeerleggen : MonoBehaviour
{
    public GameObject plankGrond;
    public GameObject boxCollider;

    public NavMeshSurface surface;

    private ManagerUI managerUI;

    private void Start()
    {
        plankGrond.SetActive(false);
        boxCollider.SetActive(false);
        surface.BuildNavMesh();
        managerUI = FindObjectOfType<ManagerUI>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Character")
        {
            if(PlankOppakken.hasPlank)
            {
                managerUI.Pickup(true);
            }

            if(Input.GetButtonDown("Input E"))
            {
                if(PlankOppakken.hasPlank)
                {
                    plankGrond.SetActive(true);
                    boxCollider.SetActive(true);
                    StartCoroutine(GameObject.FindWithTag("Game Manager").GetComponent<BuildingNavmesh>().BuildNavMesh());
                    print("buildnav");
                    PlankOppakken.hasPlank = false;
                    managerUI.Pickup(false);
                }
            }
        }
    }
}
