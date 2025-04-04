using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationScript : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        float moveSpeed = 27.0f;
        rb.velocity = new Vector3(0, 0, moveSpeed);
        Destroy(gameObject, 1.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
