using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingScript : MonoBehaviour
{
    public Transform target; // 追尾対象のオブジェクト（オブジェクト2）
    public float speed = 5f; // 移動速度
    public float rotationSpeed = 10f; // 回転速度

    void Update()
    {
        // ターゲットの方向を計算
        Vector3 direction = (target.position - transform.position).normalized;

        // ターゲットの方向に回転
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // ターゲットに向かって移動
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}

