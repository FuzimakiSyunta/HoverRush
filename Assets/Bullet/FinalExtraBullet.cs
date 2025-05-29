using UnityEngine;

public class FinalExtraBullet : MonoBehaviour
{
    public float lifeTime = 5f; // ’e‚ªÁ‚¦‚é‚Ü‚Å‚ÌŠÔ

    void Start()
    {
        // ˆê’èŠÔŒã‚É’e‚ğíœiƒƒ‚ƒŠß–ñj
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // ’e‚ğíœ
        }
    }
}
