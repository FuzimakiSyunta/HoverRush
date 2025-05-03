using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody rb;
    private float RotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        float moveSpeedZ = 40.0f;
        float moveSpeedY = 1.5f;
        rb.velocity = new Vector3(0, -moveSpeedY, -moveSpeedZ);
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        RotateSpeed -= 1.0f;
        transform.rotation = Quaternion.Euler(0, RotateSpeed, 0); // // Xé≤ÇíÜêSÇ…45ÅãâÒì]
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
