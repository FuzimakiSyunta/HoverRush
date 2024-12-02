using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        float moveSpeed = 27.0f;
        rb.velocity = new Vector3(0, -moveSpeed, 0);
        Destroy(gameObject, 3);
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
        if (other.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
    }
}
