using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalExtraBullet_L : MonoBehaviour
{
    public Rigidbody rb;
    public float baseSpeedZ = 15.0f;

    void Start()
    {
        Destroy(gameObject, 3); // 3�b��ɃI�u�W�F�N�g��j��
    }

    void Update()
    {
        // ���x���t�����ɐݒ� (z �̕����𔽓])
        rb.velocity = new Vector3(0, 0, -(baseSpeedZ + Time.time));

        // x�����̐���
        if (transform.position.x >= 13)
        {
            Destroy(gameObject);
        }
    }
}