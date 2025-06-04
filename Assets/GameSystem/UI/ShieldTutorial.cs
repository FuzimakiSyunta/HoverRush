using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTutorial : MonoBehaviour
{
    private BossScript bossScript; // BossScriptの参照
    public GameObject boss;        // Bossオブジェクト

    private GameManager gameManagerScript; // GameManagerの参照
    public GameObject gameManager; // GameManagerオブジェクト

    public GameObject shieldLuleImage;   // 最初に表示する画像（GameObject）
    public GameObject bossattackImage;   // 切り替える画像（GameObject）

    public GameObject gameTimer;
    private GameTimer gameTimerScript;

    private bool isImage1Active = true;
    private bool hasTriggered = false;
    private bool isPaused = false;

    private void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        gameTimerScript = gameTimer.GetComponent<GameTimer>();

        // 最初は非表示
        shieldLuleImage.SetActive(false);
        bossattackImage.SetActive(false);
    }

    void Update()
    {
        if(gameManagerScript.IsGameStart()&&!gameManagerScript.IsGameOver()&&!gameManagerScript.IsGameClear())
        {
            // 時間停止のトリガー
            if (!hasTriggered && gameTimerScript.GetElapsedTime()>=131)
            {
                shieldLuleImage.SetActive(true);
                bossattackImage.SetActive(false);

                isImage1Active = true;
                Time.timeScale = 0f;
                isPaused = true;
                hasTriggered = true;
            }

            if (isPaused)
            {
                // Spaceキーで画像切り替え
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SwitchImage();
                }

                // Enterキーで時間再開
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Time.timeScale = 1f;
                    isPaused = false;

                    // 画像を非表示に戻す
                    shieldLuleImage.SetActive(false);
                    bossattackImage.SetActive(false);
                }
            }
        }

    }

    void SwitchImage()
    {
        isImage1Active = !isImage1Active;

        shieldLuleImage.SetActive(isImage1Active);
        bossattackImage.SetActive(!isImage1Active);
    }
}
