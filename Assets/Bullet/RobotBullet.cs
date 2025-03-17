using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    private Transform target; // オブジェクト2 (ターゲット)
    private float speed = 20f; // 移動速度
    private float rotationSpeed = 660f; // 回転速度
    private float SpawnedTimer =  0;
    

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform; // ターゲットを設定
    }

    void Start()
    { 
        Destroy(gameObject, 6f);
    }

    void Update()
    {
        if (target == null) return;

        // ターゲットの方向を計算
        Vector3 direction = (target.position - transform.position).normalized;

        // ターゲットの方向に回転
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // ターゲットに向かって移動
        transform.position += transform.forward * speed * Time.deltaTime;

        SpawnedTimer += Time.deltaTime;

        if(SpawnedTimer>=1)
        {
            speed = 100f;
            rotationSpeed = 0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HomingTargrt")
        {
            Destroy(this.gameObject);
        }

    }
}
