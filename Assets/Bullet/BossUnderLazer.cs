using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnderLazer : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem particle;
    private float rotateY = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, rotateY, 0); // // Y���𒆐S��45����]
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
