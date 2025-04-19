using System.Collections;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour
{
    public Transform target; // ロックオンするターゲット
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform firePoint; // 弾が発射される位置
    public float fireInterval = 0.5f; // 発射間隔（秒）

    // Boss
    public GameObject boss;
    private BossScript bossScript;

    void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
        
    }
    void Update()
    {
        if (bossScript != null && bossScript.IsFinalBattle() && !IsInvoking(nameof(FireOnce)))
        {
            Debug.Log("Final Battle開始！弾を発射します");
            InvokeRepeating(nameof(FireOnce), 0f, fireInterval);
        }
    }


    void FireOnce()
    {
        if (target != null && bulletPrefab != null)
        {
            // ターゲットの方向を向く
            transform.LookAt(target);
            

            // 弾を生成して発射
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // 弾の動きを制御する場合、例えばRigidbodyで力を加える
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * 200f; // 弾を前方に飛ばす（速度20）
            }
        }
    }

}