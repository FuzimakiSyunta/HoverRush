using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExtraBulletL : MonoBehaviour
{
    public Rigidbody rb; // Rigidbody�R���|�[�l���g
    public float reflectSpeedMultiplier = 2.0f; // ���ˌ�̑��x�{��
    

    private Vector3 currentVelocity; // �e�ۂ̌��݂̑��x

    void Start()
    {
        float moveSpeedX = 25.0f;
        float moveSpeedZ = 20.0f;

        currentVelocity = new Vector3(-moveSpeedX, 0, -moveSpeedZ); // �������x��ݒ�
        rb.velocity = currentVelocity; // Rigidbody�ɑ��x��ݒ�
        Destroy(gameObject, 3); // 3�b��ɒe�ۂ��폜
    }

    void Update()
    {
        
        rb.velocity = currentVelocity; // �V�������x��Rigidbody�ɐݒ�
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject); // �v���C���[�ɓ���������e�ۂ��폜
        }
        else if (other.gameObject.tag == "Wall") // �ǂɏՓ˂����ꍇ
        {
            // �ǂ̖@�����擾�i�ǂ�Collider��Transform���g�p�j
            Vector3 normal = other.transform.forward;

            // ���˃x�N�g�����v�Z
            Vector3 reflectDirection = Vector3.Reflect(currentVelocity, normal);

            // ���ˌ�̑��x�����ԂŒ��� (���x�{���K�p)
            currentVelocity = -reflectDirection * reflectSpeedMultiplier;
            rb.velocity = currentVelocity; // �V�������x��Rigidbody�ɐݒ�
        }
    }
}