using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingNavmesh : MonoBehaviour
{
    public GameObject disabledMeshRenderes;

    private NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// builds the stupid navmesh
    /// </summary>
    public IEnumerator BuildNavMesh()
    {
        print("Flipping coin that this works");

        // Turn on
        foreach(Transform child in disabledMeshRenderes.transform)
        {
            if(child.GetComponent<MeshRenderer>() != null)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        surface.BuildNavMesh();
        // Turn off
        foreach(Transform child in disabledMeshRenderes.transform)
        {
            if(child.GetComponent<MeshRenderer>() != null)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        yield break;
    }
}
