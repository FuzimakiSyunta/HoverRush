using System.Collections;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject boss;
    public GameObject shieldPrefab;
    public GameObject chargeEffectPrefab;

    private BossController bossController;
    private GameManager gameManagerScript;
    public GameObject gameManager;

    private bool isCharging = false;
    private GameObject currentChargeEffect;

    void Start()
    {
        bossController = boss.GetComponent<BossController>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        if (bossController != null && bossController.IsFinalBattle() && !isCharging)
        {
            Debug.Log("必殺技チャージ開始！");
            StartCoroutine(ChargeAndFire());
        }

        if (isCharging && chargeEffectPrefab != null && currentChargeEffect == null)
        {
            currentChargeEffect = Instantiate(chargeEffectPrefab, firePoint.position, firePoint.rotation);
        }

        if (!isCharging && currentChargeEffect != null)
        {
            Destroy(currentChargeEffect);
        }
    }

    IEnumerator ChargeAndFire()
    {
        isCharging = true;

        yield return new WaitForSeconds(10f);
        FireSpecialBullet();

        yield return new WaitForSeconds(20f);
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
