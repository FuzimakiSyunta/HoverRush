using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnderLazer : MonoBehaviour
{
    public Rigidbody rb;
    //public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Damaged()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Damaged();
    }
}
