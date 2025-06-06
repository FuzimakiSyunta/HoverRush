using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("HP設定")]
    [SerializeField] private int bossMaxHp = 10000;
    private int currentHp;

    [Header("管理フラグ")]
    [SerializeField] private bool isFinalBattle = false;
    private bool isGameStart = false;
    private float bossBattleTime = 0f;

    [Header("参照")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject finalRobot;
    [SerializeField] private GameObject bossAir;
    [SerializeField] private GameObject enemyPositionObject;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private GameObject damegeCanvas;
    [SerializeField] private GameManager gameManager;

    public float BossTime => bossBattleTime;

    private void Start()
    {
        currentHp = bossMaxHp;
        animator.SetBool("isMove", false);
        animator.SetBool("isLazer", false);
        animator.SetBool("isAirTransform", false);
        animator.SetBool("isRobotStay", false);
        animator.SetBool("isTransform", false);
        animator.SetBool("FinalWave", false);
        animator.SetBool("isFinalBattle", false);
        finalRobot.SetActive(false);
        bossAir.SetActive(true);
        enemyPositionObject.SetActive(false);
    }

    private void Update()
    {
        if (gameManager.IsGameStart())
        {
            isGameStart = true;
        }

        if (isGameStart)
        {
            bossBattleTime += Time.deltaTime;
            UpdateBossWaveState();

            if (gameManager.IsBossWave())
            {
                enemyPositionObject.SetActive(true);
            }
            else
            {
                enemyPositionObject.SetActive(false);
            }
        }

        if (currentHp <= 0)
        {
            TriggerBossDeath();
        }
    }

    public void ReduceHp(int amount)
    {
        currentHp -= amount;
    }

    public float GetHpRate()
    {
        return (float)currentHp / bossMaxHp;
    }

    public bool IsInBossWave()
    {
        return gameManager != null && gameManager.IsBossWave();
    }

    public void BossWaveTimeAdd(float amount)
    {
        bossBattleTime += amount;
    }

    public bool IsFinalBattle()
    {
        return isFinalBattle;
    }

    private void TriggerBossDeath()
    {
        GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Destroy(particle, 2.5f);

        enemyPositionObject.SetActive(false);
        damegeCanvas.SetActive(false);
        gameManager.GameClearStart();
        gameManager.BatteryEnargyUp();
        Destroy(gameObject);
    }

    private void UpdateBossWaveState()
    {
        animator.SetBool("isMove", bossBattleTime > 20 && bossBattleTime <= 40 || bossBattleTime >= 131);
        animator.SetBool("isLazer", bossBattleTime >= 60 && bossBattleTime < 80);
        animator.SetBool("isTransform", bossBattleTime >= 80 && bossBattleTime < 100);
        animator.SetBool("isRobotStay", bossBattleTime >= 85 && bossBattleTime < 100);
        animator.SetBool("isAirTransform", bossBattleTime >= 100 && bossBattleTime < 103);
        animator.SetBool("FinalWave", bossBattleTime >= 101 && bossBattleTime < 130);
        animator.SetBool("isFinalTransForm", bossBattleTime >= 131 && bossBattleTime < 132);
        animator.SetBool("isFinalBattle", bossBattleTime >= 132);

        isFinalBattle = bossBattleTime >= 132;

        if (isFinalBattle)
        {
            finalRobot.SetActive(true);
            bossAir.SetActive(false);
            bossAir.transform.position = new Vector3(transform.position.x, 6.0f, transform.position.z);
        }

        if (animator.GetBool("FinalWave"))
        {
            bossAir.SetActive(true);
        }
    }
}
