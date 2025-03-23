using System.Collections;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public Material defaultMaterial; // 通常のマテリアル
    public Material damageMaterial; // ダメージ時のマテリアル
    private Renderer objectRenderer;

    public GameObject boss;
    private BossScript bossScript;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        bossScript = boss.GetComponent<BossScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Bullet":
                StartCoroutine(DamageEffectCoroutine());
                break;
            case "Machinegun":
                StartCoroutine(DamageEffectCoroutine());
                break;
            case "PenetrationBullet":
                StartCoroutine(DamageEffectCoroutine());
                break;
        }
    }
    void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "PlayerLazer":
                StartCoroutine(DamageEffectCoroutine());
                break;
            case "PlayerLazer_L":
                StartCoroutine(DamageEffectCoroutine());
                break;
            case "PlayerLazer_R":
                StartCoroutine(DamageEffectCoroutine());
                break;
        }
    }

    IEnumerator DamageEffectCoroutine()
    {
        objectRenderer.material = damageMaterial; // ダメージ時のマテリアルに切り替え
        yield return new WaitForSeconds(0.1f); // 0.1秒待機
        objectRenderer.material = defaultMaterial; // 元のマテリアルに戻す
    }
}