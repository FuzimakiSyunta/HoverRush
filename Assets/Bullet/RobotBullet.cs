using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    private Transform target; // �I�u�W�F�N�g2 (�^�[�Q�b�g)
    public float speed = 13f; // �ړ����x
    public float rotationSpeed = 200f; // ��]���x

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform; // �^�[�Q�b�g��ݒ�
    }

    void Start()
    { 
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if (target == null) return;

        // �^�[�Q�b�g�̕������v�Z
        Vector3 direction = (target.position - transform.position).normalized;

        // �^�[�Q�b�g�̕����ɉ�]
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // �^�[�Q�b�g�Ɍ������Ĉړ�
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HomingTargrt")
        {
            Destroy(this.gameObject);
        }

    }
}
