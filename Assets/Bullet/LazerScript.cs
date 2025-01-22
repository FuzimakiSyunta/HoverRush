using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        float moveSpeed = 57.0f;
        rb.velocity = new Vector3(0, 0, moveSpeed);
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Boss")
        {
            Destroy(this.gameObject);
        }
    }

}
