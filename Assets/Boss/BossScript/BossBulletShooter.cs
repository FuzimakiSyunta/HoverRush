using UnityEngine;

public class BossBulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.0f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            if (bulletPrefab != null && firePoint != null)
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }
}