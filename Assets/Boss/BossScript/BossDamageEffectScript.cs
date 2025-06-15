using System.Collections;
using UnityEngine;

public class BossDamageEffectScript : MonoBehaviour
{
    public Material defaultMaterial; // �ʏ�̃}�e���A��
    public Material damageMaterial; // �_���[�W���̃}�e���A��
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
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
        objectRenderer.material = damageMaterial; // �_���[�W���̃}�e���A���ɐ؂�ւ�
        yield return new WaitForSeconds(0.1f); // 0.1�b�ҋ@
        objectRenderer.material = defaultMaterial; // ���̃}�e���A���ɖ߂�
    }
}