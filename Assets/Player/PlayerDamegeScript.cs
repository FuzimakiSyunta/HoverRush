using System.Collections;
using UnityEngine;

public class PlayerDamageScript : MonoBehaviour
{
    private Material defaultMaterial; // �ʏ�̃}�e���A��
    public Material damageMaterial; // �_���[�W���̃}�e���A��
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        // ���݂̃}�e���A�����f�t�H���g�}�e���A���Ƃ��Ď擾
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
        objectRenderer.material = damageMaterial; // �_���[�W���̃}�e���A���ɐ؂�ւ�
        yield return new WaitForSeconds(0.2f); // 0.1�b�ҋ@
        objectRenderer.material = defaultMaterial; // ���̃}�e���A���ɖ߂�
    }
}