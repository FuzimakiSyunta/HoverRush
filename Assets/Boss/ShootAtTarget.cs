using System.Collections;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject boss;
    public GameObject shieldPrefab;

    private BossScript bossScript;
    private bool isCharging = false;

    public GameObject ShieldImage; // シールドのUIイメージ
    public GameObject ShieldBatteryImage; // シールドのエナジーUIイメージ

    // ゲームマネージャーの参照
    private GameManager gameManagerScript;
    public GameObject gameManager;


    // ランダム配置用座標リスト
    private Vector2[] shieldSpawnPositions = new Vector2[]
    {
        new Vector2(11f, 8f),
        new Vector2(-6f, 4f),
        new Vector2(-17f, 10f),
        new Vector2(-11f, 5f),
        new Vector2(-4.6f, 6f)
    };

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
        ShieldImage.SetActive(gameManagerScript.GetBatteryEnargy() >= 100);

        if (bossScript != null && bossScript.IsFinalBattle() && !isCharging)
        {
            Debug.Log("必殺技チャージ開始！");
            StartCoroutine(ChargeAndFire());
            ShieldBatteryImage.SetActive(true);
        }

        if (gameManagerScript.GetBatteryEnargy() >= 100 && Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            gameManagerScript.ShieldBatteryEnargy();
            SpawnShieldAtPlayerPosition();
        }
    }


    IEnumerator ChargeAndFire()
    {
        isCharging = true;

        yield return new WaitForSeconds(10f);

        FireSpecialBullet();

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

    //void SpawnShieldAtRandomPosition()
    //{
    //    if (shieldPrefab == null) return;

    //    int index = Random.Range(0, shieldSpawnPositions.Length);
    //    Vector2 pos2D = shieldSpawnPositions[index];

    //    // y=0としてZ軸を2Dのyとして使う
    //    Vector3 spawnPos = new Vector3(pos2D.x, 0f, pos2D.y);
    //    Instantiate(shieldPrefab, spawnPos, Quaternion.identity);

    //    Debug.Log($"シールド配置: {spawnPos}");
    //}
    void SpawnShieldAtPlayerPosition()
    {
        if (shieldPrefab == null || target == null) return;

        Vector3 spawnPos = new Vector3(target.position.x, 0f, target.position.z); // Yは0固定、XZはプレイヤーに合わせる
        Instantiate(shieldPrefab, spawnPos, Quaternion.identity);

        Debug.Log($"シールド配置（プレイヤー位置）: {spawnPos}");
    }

}
