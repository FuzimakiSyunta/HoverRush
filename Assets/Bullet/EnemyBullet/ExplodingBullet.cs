using UnityEngine;

public class ExplodingBullet : MonoBehaviour
{
    public GameObject explosionEffect;  // 爆発エフェクト
    public GameObject damageAreaPrefab; // ダメージ判定用オブジェクト（Trigger付き）
    public float explosionDelay = 3f;   // 爆発までの時間
    public float damageAreaLifetime = 0.5f; // ダメージ判定の持続時間

    void Start()
    {
        Invoke(nameof(Explode), explosionDelay);
    }

    void Explode()
    {
        // 爆発エフェクト生成
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // ダメージ判定用オブジェクト生成
        if (damageAreaPrefab != null)
        {
            GameObject damageArea = Instantiate(damageAreaPrefab, transform.position, Quaternion.identity);
            Destroy(damageArea, damageAreaLifetime); // 一定時間で自動破壊
        }

        // 自身（弾）を破壊
        Destroy(gameObject);
    }
}
