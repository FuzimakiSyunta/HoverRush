using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalExtraBullet_L : MonoBehaviour
{
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        float moveSpeedX = 9.0f;
        float moveSpeedZ = 25.0f;
        rb.velocity = new Vector3(-moveSpeedX, 0, -moveSpeedZ);
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -13)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

    }
}
