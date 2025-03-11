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
        float moveSpeed = 47.0f;
        rb.velocity = new Vector3(0, 0, -moveSpeed);
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        RotateSpeed -= 1.0f;
        transform.rotation = Quaternion.Euler(0, RotateSpeed, 0); // // X���𒆐S��45����]
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
