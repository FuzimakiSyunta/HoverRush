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

    public GameObject ShieldImage; // �V�[���h��UI�C���[�W
    public GameObject ShieldBatteryImage; // �V�[���h�̃G�i�W�[UI�C���[�W

    // �Q�[���}�l�[�W���[�̎Q��
    private GameManager gameManagerScript;
    public GameObject gameManager;


    // �����_���z�u�p���W���X�g
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
        ShieldImage.SetActive(false); // �V�[���h��UI�C���[�W���\���ɂ���
        ShieldBatteryImage.SetActive(false); // �V�[���h�̃G�i�W�[UI�C���[�W���\���ɂ���
    }

    void Update()
    {
        if (!gameManagerScript.IsGameStart() || gameManagerScript.IsGameClear() || gameManagerScript.IsGameOver())
        {
            // �Q�[�����łȂ��Ȃ�UI���\��
            ShieldImage.SetActive(false);
            ShieldBatteryImage.SetActive(false);
            return;
        }

        // �ȉ��̓Q�[�����̂ݎ��s�����
        ShieldImage.SetActive(gameManagerScript.GetBatteryEnargy() >= 100);

        if (bossScript != null && bossScript.IsFinalBattle() && !isCharging)
        {
            Debug.Log("�K�E�Z�`���[�W�J�n�I");
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

    //    // y=0�Ƃ���Z����2D��y�Ƃ��Ďg��
    //    Vector3 spawnPos = new Vector3(pos2D.x, 0f, pos2D.y);
    //    Instantiate(shieldPrefab, spawnPos, Quaternion.identity);

    //    Debug.Log($"�V�[���h�z�u: {spawnPos}");
    //}
    void SpawnShieldAtPlayerPosition()
    {
        if (shieldPrefab == null || target == null) return;

        Vector3 spawnPos = new Vector3(target.position.x, 0f, target.position.z); // Y��0�Œ�AXZ�̓v���C���[�ɍ��킹��
        Instantiate(shieldPrefab, spawnPos, Quaternion.identity);

        Debug.Log($"�V�[���h�z�u�i�v���C���[�ʒu�j: {spawnPos}");
    }

}
