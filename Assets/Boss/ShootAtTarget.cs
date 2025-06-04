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

    public GameObject ShieldImage; // シールドのUIイメージ
    public GameObject ShieldBatteryImage; // シールドのエナジーUIイメージ

    // ゲームマネージャーの参照
    private GameManager gameManagerScript;
    public GameObject gameManager;

    void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        ShieldImage.SetActive(false); // シールドのUIイメージを非表示にする
        ShieldBatteryImage.SetActive(false); // シールドのエナジーUIイメージを非表示にする
    }

    void Update()
    {
        if (!gameManagerScript.IsGameStart() || gameManagerScript.IsGameClear() || gameManagerScript.IsGameOver())
        {
            // ゲーム中でないならUIを非表示
            ShieldImage.SetActive(false);
            ShieldBatteryImage.SetActive(false);
            return;
        }

        // 以下はゲーム中のみ実行される
        ShieldImage.SetActive(gameManagerScript.GetBatteryEnargy() >= 30);

        if (bossScript != null && bossScript.IsFinalBattle() && !isCharging)
        {
            Debug.Log("必殺技チャージ開始！");
            StartCoroutine(ChargeAndFire());
            ShieldBatteryImage.SetActive(true);
        }

        if (gameManagerScript.GetBatteryEnargy() >= 30 &&
        (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.Q)))
        {
            gameManagerScript.ShieldBatteryEnargy();
            SpawnShieldAtPlayerPosition();
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

        
        void SpawnShieldAtPlayerPosition()
        {
            if (shieldPrefab == null || target == null) return;

            Vector3 spawnPos = new Vector3(target.position.x, 0f, target.position.z); // Yは0固定、XZはプレイヤーに合わせる
            Instantiate(shieldPrefab, spawnPos, Quaternion.identity);

            Debug.Log($"シールド配置（プレイヤー位置）: {spawnPos}");
        }

    }

    
}
