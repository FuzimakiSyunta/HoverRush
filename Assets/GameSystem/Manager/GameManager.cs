using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

using TMPro;
//using TMPro;


public class GameManager : MonoBehaviour
{
    //主要オブジェクト
    public GameObject enemy;
    public GameObject mineEnemy;
    public GameObject planeEnemy;
    public GameObject player;
    public GameObject UI;
    //text&Image
    public GameObject gameOverText;
    public GameObject gameClearText;
    public GameObject titleText;
    public GameObject StartButtonImage;

    //Select
    private bool OpenSelector = false;
    
    //ゲームシステム
    private float[] CoolTime = new float[5];
    private bool GameOverFlag = false;
    private bool GameClearFlag = false;
    private bool GameStartFlag = false;
    private int score = 0;

    //WAVE
    public int Wave;
    public float BossWaveCount;
    private bool BossWaveFlag;
    private int waveModifier;
    

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i<5;i++)
        {
            CoolTime[i] = 0;
        }
        titleText.SetActive(true);
        StartButtonImage.SetActive(true);
        Wave = 0;
        BossWaveCount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //セレクト
        if (OpenSelector == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                OpenSelector = true;
                titleText.SetActive(false);
                StartButtonImage.SetActive(false);
                GameStartFlag = false;
            }
        }
        if(GameClearFlag==true)
        {
            UI.SetActive(false);
        }else
        {
            UI.SetActive(true);
        }

            
        if (GameStartFlag == true)
        {
            BossWaveCount += Time.deltaTime;
            
            if (BossWaveCount>=18&&BossWaveCount <= 40)
            {
                BossWaveFlag = true;
                
            }
            if (BossWaveCount >= 40 && BossWaveCount < 60)
            {
                BossWaveFlag = false;
                Wave = 1;
               
            }
            if (BossWaveCount >= 58 && BossWaveCount < 80)
            {
                BossWaveFlag = true;
               
            }
            if (BossWaveCount >= 80 && BossWaveCount < 125)
            {
                BossWaveFlag = false;
                Wave = 2;
               
            }
            
        }
        
    }
    private void FixedUpdate()//敵出現
    {
        if (GameOverFlag == true) return;
        if (GameClearFlag == true) return;

        if (GameStartFlag == true&&BossWaveFlag==false) // ゲームが開始しておりボスウェーブじゃないとき
        {
            int RandomEnemy = Random.Range(0, 15000); // 敵の出現頻度をランダムに決定
            int Style = Random.Range(0, 3); // 敵の種類をランダムに決定
            int AttackEnemy = Random.Range(0, 3); // 攻撃してくる敵をランダムに決定

            // 現在のWAVEによって確率範囲を変更
            if (Wave == 0)
            {
                waveModifier = 1;
            }
            if (Wave == 1)
            {
                waveModifier = 2; 
            }
            else if (Wave == 2)
            {
                waveModifier = 3; 
            }

            // 出現条件（範囲）を設定
            int[][] Enemyranges = {
        new int[]{0, 100 * waveModifier},       // WAVEに応じた出現範囲
        new int[]{100, 200 * waveModifier},   // 範囲2
        new int[]{200, 600 * waveModifier},   // 範囲3
        //new int[]{6000, 6300 * waveModifier},   // 範囲4
        //new int[]{7000, 7100 * waveModifier}    // 範囲5

    };

            // 各CoolTime（敵が再度出現するまでの待機時間）をカウントアップ
            for (int i = 0; i < CoolTime.Length; i++)
            {
                CoolTime[i]+=Time.deltaTime;
            }

            // 機体の敵の初期位置（通常座標）
            Vector3[] positions = {
        new Vector3(-8.0f, 1.5f, 45.0f), // 左端
        new Vector3(0.0f, 1.5f, 45.0f),  // 中央
        new Vector3(8.0f, 1.5f, 45.0f),  // 右端
        new Vector3(4.0f, 1.5f, 45.0f),  // 中央右
        new Vector3(-4.0f, 1.5f, 45.0f)  // 中央左
    };

            // 隕石型の敵専用の初期位置（上部）
            Vector3[] MeteoPositions = {
        new Vector3(-8.0f, 8.0f, 45.0f), // 左端上
        new Vector3(0.0f, 8.0f, 45.0f),  // 中央上
        new Vector3(8.0f, 8.0f, 45.0f),  // 右端上
        new Vector3(4.0f, 8.0f, 45.0f),  // 中央右上
        new Vector3(-4.0f, 8.0f, 45.0f)  // 中央左上
    };

            // 出現範囲と敵の情報をもとに敵を生成
            for (int i = 0; i < Enemyranges.Length; i++)
            {
                // ランダム値が現在の範囲に含まれるかチェック
                if (RandomEnemy >= Enemyranges[i][0] && RandomEnemy <= Enemyranges[i][1])
                {
                    // CoolTimeが一定値以上なら敵を生成
                    if (CoolTime[i] >= 0.5f)
                    {
                        // 攻撃可能な敵を生成
                        if (AttackEnemy == i && Style == 0)
                        {
                            Instantiate(enemy, positions[i], Quaternion.identity); // 敵を生成
                        }
                        // 隕石型の敵を生成
                        else if (Style == 1)
                        {
                            Instantiate(mineEnemy, MeteoPositions[i], Quaternion.identity); // 地雷敵を生成
                        }
                        // 機体の敵を生成
                        else if (Style == 2)
                        {
                            Instantiate(planeEnemy, positions[i], Quaternion.identity); // 黄色敵を生成
                        }
                        // 機体の敵を生成
                        else if (Style == 3)
                        {
                            Instantiate(planeEnemy, positions[i], Quaternion.identity); // 黄色敵を生成
                        }

                        // CoolTimeをリセット
                        CoolTime[i] = 0;
                    }
                }
            }
        }
    }

    public void GameOverStart()//ゲームオーバー
    {
        GameOverFlag = true;
        gameOverText.SetActive(true);
    }
    public bool IsGameOver()
    {
        return GameOverFlag;
    }
    public void GameClearStart()//ゲームクリア
    {
        GameClearFlag = true;
        gameClearText.SetActive(true);
    }
    public bool IsGameClear()
    {
        return GameClearFlag;//クリアのフラグ
    }
    public void Score()//スコア
    {
        score += 1;
    }
    public int IsScore()
    {
        return score;
    }
    public void GameStart()//ゲームスタート
    {
        GameStartFlag = true;
    }
    public bool IsGameStart()
    {
        return GameStartFlag;
    }
    public bool IsOpenSelector()
    {
        return OpenSelector;
    }
    public bool IsBossWaveStart()
    {
        return BossWaveFlag;
    }
    public float IsBossWaveCount()
    {
        return BossWaveCount;
    }
}
