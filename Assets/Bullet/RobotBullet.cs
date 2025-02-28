using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    private float moveSpeedX;
    private float moveSpeedY;
    private float moveSpeedZ;
    private float rotateZ;
    private float rotateX;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector3(moveSpeedX, moveSpeedY, moveSpeedZ);
        transform.rotation = Quaternion.Euler(rotateX, 0, rotateZ); // // Yé≤ÇíÜêSÇ…45ÅãâÒì]
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
       moveSpeedZ = -125.0f;
       moveSpeedY = -60.0f;
       moveSpeedX = 0;
       rotateX = 0;
       
       
       //moveSpeedZ = -125.0f;
       //moveSpeedY = -60.0f;
       //moveSpeedX = 50.0f;
       //rotateX = 45.0f;
       
       
       //moveSpeedZ = -125.0f;
       //moveSpeedY = -60.0f;
       //moveSpeedX = -50.0f;
       //rotateX = -45.0f;
        
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

    }
}
