using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingScript : MonoBehaviour
{
    public Transform target; // �ǔ��Ώۂ̃I�u�W�F�N�g�i�I�u�W�F�N�g2�j
    public float speed = 5f; // �ړ����x
    public float rotationSpeed = 10f; // ��]���x

    void Update()
    {
        // �^�[�Q�b�g�̕������v�Z
        Vector3 direction = (target.position - transform.position).normalized;

        // �^�[�Q�b�g�̕����ɉ�]
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // �^�[�Q�b�g�Ɍ������Ĉړ�
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}

