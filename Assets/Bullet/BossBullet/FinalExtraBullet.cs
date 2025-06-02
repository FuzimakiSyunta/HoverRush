using UnityEngine;

public class FinalExtraBullet : MonoBehaviour
{
    public float lifeTime = 5f; // �e��������܂ł̎���

    void Start()
    {
        // ��莞�Ԍ�ɒe���폜�i�������ߖ�j
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // �e���폜
        }
    }
}
