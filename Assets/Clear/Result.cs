using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    //時間
    public GameObject gameTimer;
    private GameTimer gameTimerScript;
    //ゲームマネージャー
    public GameObject gameManager;
    private GameManager gameManagerScript;

    //ランク画像
    public GameObject S_rank;
    public GameObject A_rank;
    public GameObject B_rank;
    public GameObject RankImage;
    public GameObject RankCanvas;

    private bool isRankOpen = false;

    //ゲームクリア
    public GameObject GameClearImage;

    //ゲームクリア表示
    private float GameClearActiveTime = 0;

    public Image targetImage; // 透明度を操作する対象のImageコンポーネント
    private float targetAlpha = 0.0f; // 目標の透明度（0.0f～1.0f）
    private float fadeSpeed = 2.0f; // フェード速度

    // Start is called before the first frame update
    void Start()
    {
        gameTimerScript = gameTimer.GetComponent<GameTimer>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        RankCanvas.SetActive(false);
        GameClearImage.SetActive(false);
        GameClearActiveTime = 0;
        targetAlpha = 1.0f;
        isRankOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 現在の透明度を取得
        Color color = targetImage.color;
        //クリアしたら
        if (gameManagerScript.IsGameClear())
        {
            GameClearActiveTime += Time.deltaTime;
            if(GameClearActiveTime <=2f)
            {
                GameClearImage.SetActive(true);
            }
            else
            {
                // 目標の透明度に近づける
                color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                GameClearImage.SetActive(false);
            }
            if(GameClearActiveTime>=3f)
            {
                isRankOpen = true;
                ///ランク表示////////////////////////////////////
                RankCanvas.SetActive(true);
                RankImage.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 210)
                {
                    S_rank.SetActive(true);
                    A_rank.SetActive(false);
                    B_rank.SetActive(false);
                }
                else if (gameManagerScript.IsBatteryEnargy() >= 180 && gameManagerScript.IsBatteryEnargy() < 210)
                {
                    S_rank.SetActive(false);
                    A_rank.SetActive(true);
                    B_rank.SetActive(false);
                }
                else if (gameManagerScript.IsBatteryEnargy() < 180)
                {
                    S_rank.SetActive(false);
                    A_rank.SetActive(false);
                    B_rank.SetActive(true);
                }
                /////////////////////////////////////////////////
            }

        }
        else
        {
            RankCanvas.SetActive(false);
            GameClearImage.SetActive(false);
            GameClearActiveTime = 0;
            isRankOpen = false;
        }
        
    }
    // 透明度を下げるメソッド
    public void FadeOut()
    {
        targetAlpha = 0.0f; // 透明度を完全に0にする
    }

    // 透明度を上げるメソッド
    public void FadeIn()
    {
        targetAlpha = 1.0f; // 透明度を完全に1にする
    }

    public bool IsRankOpen() 
    {
        return isRankOpen;
    }
}
