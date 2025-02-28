using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public Rigidbody rb;
  
    // Start is called before the first frame update
    void Start()
    {
        float moveSpeedZ = 110.0f;
        float moveSpeedY = 11.0f;
        rb.velocity = new Vector3(0, -moveSpeedY, -moveSpeedZ);
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        
    }


}
