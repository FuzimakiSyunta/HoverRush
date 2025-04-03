using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExtraBullet : MonoBehaviour
{
    public Rigidbody rb; // Rigidbody�R���|�[�l���g
    public float reflectSpeedMultiplier = 2.0f; // ���ˌ�̑��x�{��

    private Vector3 currentVelocity; // �e�ۂ̌��݂̑��x
    private float lifetime = 3.0f; // �e�ۂ̎���
    private float timer = 0.0f; // �o�ߎ��Ԃ�ǐ�

    void Start()
    {
        float moveSpeedX = 25.0f;
        float moveSpeedZ = 20.0f;

        currentVelocity = new Vector3(moveSpeedX, 0, -moveSpeedZ); // �������x��ݒ�
        rb.velocity = currentVelocity; // Rigidbody�ɑ��x��ݒ�
    }

    void Update()
    {
        // �e�ۂ̎������Ǘ�
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject); // �����𒴂�����e�ۂ��폜
        }

        // ���x���p���I�ɓK�p�i�K�v�Ȃ�ǉ������j
        rb.velocity = currentVelocity;
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

            // ���ˌ�̑��x��K�p�i���x�{���܂ށj
            currentVelocity = -reflectDirection * reflectSpeedMultiplier;
        }
    }
}