using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExtraBullet : MonoBehaviour
{
    public Rigidbody rb; // Rigidbodyコンポーネント
    public float reflectSpeedMultiplier = 2.0f; // 反射後の速度倍率

    private Vector3 currentVelocity; // 弾丸の現在の速度
    private float lifetime = 3.0f; // 弾丸の寿命
    private float timer = 0.0f; // 経過時間を追跡

    void Start()
    {
        float moveSpeedX = 25.0f;
        float moveSpeedZ = 20.0f;

        currentVelocity = new Vector3(moveSpeedX, 0, -moveSpeedZ); // 初期速度を設定
        rb.velocity = currentVelocity; // Rigidbodyに速度を設定
    }

    void Update()
    {
        // 弾丸の寿命を管理
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject); // 寿命を超えたら弾丸を削除
        }

        // 速度を継続的に適用（必要なら追加処理）
        rb.velocity = currentVelocity;
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

            // 反射後の速度を適用（速度倍率含む）
            currentVelocity = -reflectDirection * reflectSpeedMultiplier;
        }
    }
}