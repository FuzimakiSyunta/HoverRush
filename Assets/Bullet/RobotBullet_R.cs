using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet_R : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    private float moveSpeedX = 20.0f;
    private float moveSpeedY = -60.0f;
    private float moveSpeedZ = -125.0f;
    private float rotateZ;
    private float rotateX;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector3(moveSpeedX, moveSpeedY, moveSpeedZ);
        transform.rotation = Quaternion.Euler(rotateX, 0, rotateZ); // // Yé≤ÇíÜêSÇ…45ÅãâÒì]
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

    }
}
