using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExtraBulletL : MonoBehaviour
{
    public Rigidbody rb; // Rigidbodyコンポーネント
    public float reflectSpeedMultiplier = 2.0f; // 反射後の速度倍率
    

    private Vector3 currentVelocity; // 弾丸の現在の速度

    void Start()
    {
        float moveSpeedX = 25.0f;
        float moveSpeedZ = 20.0f;

        currentVelocity = new Vector3(-moveSpeedX, 0, -moveSpeedZ); // 初期速度を設定
        rb.velocity = currentVelocity; // Rigidbodyに速度を設定
        Destroy(gameObject, 3); // 3秒後に弾丸を削除
    }

    void Update()
    {
        
        rb.velocity = currentVelocity; // 新しい速度をRigidbodyに設定
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject); // プレイヤーに当たったら弾丸を削除
        }
        else if (other.gameObject.tag == "Wall") // 壁に衝突した場合
        {
            // 壁の法線を取得（壁のColliderのTransformを使用）
            Vector3 normal = other.transform.forward;

            // 反射ベクトルを計算
            Vector3 reflectDirection = Vector3.Reflect(currentVelocity, normal);

            // 反射後の速度を時間で調整 (速度倍率適用)
            currentVelocity = -reflectDirection * reflectSpeedMultiplier;
            rb.velocity = currentVelocity; // 新しい速度をRigidbodyに設定
        }
    }
}