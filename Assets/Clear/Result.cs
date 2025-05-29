using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    // 時間
    public GameObject gameTimer;
    private GameTimer gameTimerScript;

    // ゲームマネージャー
    public GameObject gameManager;
    private GameManager gameManagerScript;

    // ランク画像
    public GameObject SS_rank;
    public GameObject S_rank;
    public GameObject A_rank;
    public GameObject B_rank;
    public GameObject RankImage;
    public GameObject RankCanvas;

    private bool isRankOpen = false;

    // ゲームクリア
    public GameObject GameClearImage;

    // ゲームクリア表示
    private float GameClearActiveTime = 0;

    public Image[] targetImages; // 複数のImageコンポーネントを操作するための配列
    private float targetAlpha = 0.0f; // 目標の透明度（0.0f～1.0f）
    private float fadeSpeed = 2.0f; // フェード速度

    private bool hasRankDisplayed = false; // ランク画像が表示済みか判定

    void Start()
    {
        gameTimerScript = gameTimer.GetComponent<GameTimer>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        RankCanvas.SetActive(false);
        GameClearImage.SetActive(false);
        GameClearActiveTime = 0;
        targetAlpha = 1.0f;
        isRankOpen = false;
        SS_rank.SetActive(false);
        S_rank.SetActive(false);
        A_rank.SetActive(false);
        B_rank.SetActive(false);
        hasRankDisplayed = false; // 初期状態では表示されていない
    }

    void Update()
    {
        if (gameManagerScript.IsGameClear())
        {
            GameClearActiveTime += Time.deltaTime;

            if (GameClearActiveTime <= 2.5f)
            {
                GameClearImage.SetActive(true);
            }
            else
            {
                foreach (var image in targetImages)
                {
                    Color color = image.color;
                    color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                    image.color = color;
                }
                GameClearImage.SetActive(false);
            }

            if (GameClearActiveTime >= 3f && !hasRankDisplayed)
            {
                isRankOpen = true;
                RankCanvas.SetActive(true);
                RankImage.SetActive(true);

                // ★ ランクを一度リセット
                SS_rank.SetActive(false);
                S_rank.SetActive(false);
                A_rank.SetActive(false);
                B_rank.SetActive(false);

                // ★ 新しいランク表示条件
                float elapsedTime = gameTimerScript.GetElapsedTime();

                if (elapsedTime <= 160f)
                {
                    SS_rank.SetActive(true);
                }
                else if (elapsedTime <= 170f)
                {
                    S_rank.SetActive(true);
                }
                else if (elapsedTime <= 180f)
                {
                    A_rank.SetActive(true);
                }
                else
                {
                    B_rank.SetActive(true);
                }

                gameTimerScript.StopTimer();
                hasRankDisplayed = true;
            }
        }
        else
        {
            RankCanvas.SetActive(false);
            GameClearImage.SetActive(false);
            GameClearActiveTime = 0;
            isRankOpen = false;
            hasRankDisplayed = false; // ゲームクリア終了時はフラグをリセット
        }
    }

    public void FadeOut()
    {
        targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        targetAlpha = 1.0f;
    }

    public bool IsRankOpen()
    {
        return isRankOpen;
    }
}