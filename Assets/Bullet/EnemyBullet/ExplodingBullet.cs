using UnityEngine;

public class ExplodingBullet : MonoBehaviour
{
    public GameObject explosionEffect;  // �����G�t�F�N�g
    public GameObject damageAreaPrefab; // �_���[�W����p�I�u�W�F�N�g�iTrigger�t���j
    public float explosionDelay = 3f;   // �����܂ł̎���
    public float damageAreaLifetime = 0.5f; // �_���[�W����̎�������

    public string shieldTag = "Shield"; // �V�[���h�p�^�O

    void Start()
    {
        Invoke(nameof(Explode), explosionDelay);
    }

    void Explode()
    {
        // �����G�t�F�N�g����
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // �_���[�W����p�I�u�W�F�N�g����
        if (damageAreaPrefab != null)
        {
            GameObject damageArea = Instantiate(damageAreaPrefab, transform.position, Quaternion.identity);
            Destroy(damageArea, damageAreaLifetime); // ��莞�ԂŎ����j��
        }

        // �V�[���h��T���Ĕj�󂷂�
        GameObject shield = GameObject.FindWithTag(shieldTag);
        if (shield != null)
        {
            Destroy(shield);
            Debug.Log("�V�[���h�������܂����I");
        }

        // ���g�i�e�j��j��
        Destroy(gameObject);
    }
}
