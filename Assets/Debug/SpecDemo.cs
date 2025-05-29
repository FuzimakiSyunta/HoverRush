using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecDemo : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gameManagerScript;

    public GameObject boss;
    private BossScript bossScript;

    public GameObject player;
    private PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        bossScript = boss.GetComponent<BossScript>();
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            gameManagerScript.BatteryEnargyUp();//スコア上昇
        }

        if (Input.GetKey(KeyCode.N))
        {
            bossScript.BossWaveTime();//ボス戦時間操作
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            gameManagerScript.BossWaveCountStart();//ウェーブ操作
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            playerScript.DownHp();//PlayerHp減少
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            playerScript.UpHp();//PlayerHp増加
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            bossScript.DownHp();//BossHp増加
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            bossScript.UpHp();//BossHp増加
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gameManagerScript.HealCounter();//回復回数増加
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale += 0.5f;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 10f); 
            Debug.Log("TimeScaleを上昇: " + Time.timeScale);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Time.timeScale -= 0.5f;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 5f); // 0以下にならないように
            Debug.Log("TimeScaleを減少: " + Time.timeScale);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = 1.0f; // 通常速度に戻す
            Debug.Log("TimeScaleリセット: " + Time.timeScale);
        }
    }
}
