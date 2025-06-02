using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float moveSpeedZ = 30.0f;
    private float moveSpeedY = 10.0f;
    private float timeElapsed = 0f;
    private float rotateSpeed = 10f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, 1f); 
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // �ʒu���u�����ʒu �{ (���x �~ �o�ߎ���)�v�Ōv�Z
        Vector3 offset = new Vector3(0, -moveSpeedY * timeElapsed, -moveSpeedZ * timeElapsed);
        transform.position = startPosition + offset;

        // ��]�iY���ɉ�]�j
        rotateSpeed -= 60f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, rotateSpeed, 0);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
