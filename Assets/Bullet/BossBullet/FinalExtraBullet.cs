using UnityEngine;

public class FinalExtraBullet : MonoBehaviour
{
    public float lifeTime = 5f; // 弾が消えるまでの時間

    void Start()
    {
        // 一定時間後に弾を削除（メモリ節約）
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // 弾を削除
        }
    }
}
