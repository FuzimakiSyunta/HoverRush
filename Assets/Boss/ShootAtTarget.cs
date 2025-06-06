using System.Collections;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject boss;
    public GameObject shieldPrefab;
    public GameObject chargeEffectPrefab; // チャージエフェクトのプレハブ

    private BossScript bossScript;
    private bool isCharging = false;

    // ゲームマネージャーの参照
    private GameManager gameManagerScript;
    public GameObject gameManager;

    void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {

        // 以下はゲーム中のみ実行される
        if (bossScript != null && bossScript.IsFinalBattle() && !isCharging)
        {
            Debug.Log("必殺技チャージ開始！");
            StartCoroutine(ChargeAndFire());
            
        }

        if (isCharging)
        {
            // チャージ中のエフェクトを表示
            if (chargeEffectPrefab != null)
            {
                Instantiate(chargeEffectPrefab, firePoint.position, firePoint.rotation);
            }
        }
        else
        {
            // チャージ中でない場合はエフェクトを非表示にする
            if (chargeEffectPrefab != null)
            {
                foreach (var effect in GameObject.FindGameObjectsWithTag("ChargeEffect"))
                {
                    Destroy(effect);
                }
            }

        }


        IEnumerator ChargeAndFire()
        {
            isCharging = true;

            // チャージ中エフェクトは Update() 内で表示される

            yield return new WaitForSeconds(10f); // チャージ

            FireSpecialBullet(); // 発射

            yield return new WaitForSeconds(20f); // クールダウン

            isCharging = false;
        }

        void FireSpecialBullet()
        {
            if (target != null && bulletPrefab != null)
            {
                transform.LookAt(target);

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = transform.forward * 10f;
                }
            }
        }

    }

    
}
