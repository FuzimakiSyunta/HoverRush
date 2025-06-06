using UnityEngine;

public class BossDamageHandler : MonoBehaviour
{
    private BossStatus bossStatus;

    void Start()
    {
        bossStatus = GetComponent<BossStatus>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            bossStatus.ApplyDamage(10);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Lazer"))
        {
            bossStatus.ApplyDamage(1);
        }
    }
}