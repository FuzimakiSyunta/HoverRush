using System.Collections;
using UnityEngine;

public class PlayerDamageScript : MonoBehaviour
{
    private Material defaultMaterial; // 通常のマテリアル
    public Material damageMaterial; // ダメージ時のマテリアル
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        // 現在のマテリアルをデフォルトマテリアルとして取得
        if (objectRenderer != null)
        {
            defaultMaterial = objectRenderer.material;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "EnemyBullet":
                StartCoroutine(DamageEffectCoroutine());
                break;
            case "BossExtraBullet":
                StartCoroutine(DamageEffectCoroutine());
                break;
            case "BossBullet":
                StartCoroutine(DamageEffectCoroutine());
                break;
            case "RobotBullet":
                StartCoroutine(DamageEffectCoroutine());
                break;
        }
    }
    void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Lazer":
                StartCoroutine(DamageEffectCoroutine());
                break;
            case "FinalLazer":
                StartCoroutine(DamageEffectCoroutine());
                break;
            
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(DamageEffectCoroutine());

        }

    }

    IEnumerator DamageEffectCoroutine()
    {
        objectRenderer.material = damageMaterial; // ダメージ時のマテリアルに切り替え
        yield return new WaitForSeconds(0.2f); // 0.1秒待機
        objectRenderer.material = defaultMaterial; // 元のマテリアルに戻す
    }
}