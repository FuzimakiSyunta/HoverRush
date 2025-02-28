using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    
    public Transform player; // �v���C���[�I�u�W�F�N�g��Transform
    public float moveSpeed = 5.0f; // �G�̈ړ����x

    // Start is called before the first frame update
    void Start()
    {
        

        //float moveSpeedZ = 125.0f;
        //float moveSpeedY = 60.0f;
        //float rotateZ = 45.0f;

        //rb.velocity = new Vector3(0, -moveSpeedY, -moveSpeedZ);
        //transform.rotation = Quaternion.Euler(0, 0, rotateZ); // // Y���𒆐S��45����]
        //Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // �v���C���[�̈ʒu������
            transform.LookAt(player);

            // �v���C���[�Ɍ������Ĉړ�
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

    }
}
