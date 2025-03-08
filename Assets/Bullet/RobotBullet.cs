using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    private Transform target; // オブジェクト2 (ターゲット)
    public float speed = 13f; // 移動速度
    public float rotationSpeed = 200f; // 回転速度

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform; // ターゲットを設定
    }

    void Start()
    { 
        Destroy(gameObject, 5f);
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
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HomingTargrt")
        {
            Destroy(this.gameObject);
        }

    }
}
