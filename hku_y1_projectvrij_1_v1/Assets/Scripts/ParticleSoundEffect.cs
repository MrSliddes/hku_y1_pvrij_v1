using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSoundEffect : MonoBehaviour
{
    public float aliveTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, aliveTime);
    }
}
