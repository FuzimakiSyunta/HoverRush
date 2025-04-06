using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalExtraBullet_L : MonoBehaviour
{
    public Rigidbody rb;
    public float baseSpeedZ = 15.0f;

    void Start()
    {
        Destroy(gameObject, 3); // 3•bŒã‚ÉƒIƒuƒWƒFƒNƒg‚ð”j‰ó
    }

    void Update()
    {
        // ‘¬“x‚ð‹t•ûŒü‚ÉÝ’è (z ‚Ì•„†‚ð”½“])
        rb.velocity = new Vector3(0, 0, -(baseSpeedZ + Time.time));

        // x•ûŒü‚Ì§ŒÀ
        if (transform.position.x >= 13)
        {
            Destroy(gameObject);
        }
    }
}