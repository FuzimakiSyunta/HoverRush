using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem particle;
    private float moveSpeedZ;
    private float moveSpeedY;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeedZ = 25.0f;
        moveSpeedY = -35.0f;
        rb.velocity = new Vector3(0, moveSpeedY, moveSpeedZ);
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // 
        float timeFactor = Time.deltaTime;
        float newSpeedZ = moveSpeedZ + (timeFactor); 
        float newSpeedY = moveSpeedY + (timeFactor); 
        rb.velocity = new Vector3(0, newSpeedY, newSpeedZ);
    }
    

}
