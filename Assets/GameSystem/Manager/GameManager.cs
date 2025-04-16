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
    public GameObject hevyplaneEnemy;
    public GameObject blueplaneEnemy;
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
    public int batteryEnargy = 0;//強化用
    public int healBatteryEnargy = 0;//回復用
    private int healcount = 5;//回復回数

    //WAVE
    public int Wave;
    public float GamePlayCount;
    private bool BossWaveFlag;
    private int waveModifier;

    //ゲーム開始時出現
    public GameObject SpeedParticle;
    
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
        GamePlayCount = 0;
        SpeedParticle.SetActive(false);
        healcount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
        //セレクト///////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////////////



        //UI全体///////////////
        if(GameClearFlag==true)
        {
            UI.SetActive(false);
        }else
        {
            UI.SetActive(true);
        }
        ///////////////////////
        


        ///回復管理/////////////////////////////////////
        if(healcount<=0)
        {
            HealBatteryEnargyReset();
        }
        /////////////////////////////////////////////////////

        //ウェーブ管理/////////////////////////
        if (GameStartFlag == true)
        {
            GamePlayCount += Time.deltaTime;
            SpeedParticle.SetActive(true);

            if (GamePlayCount>=18&&GamePlayCount <= 40)
            {
                BossWaveFlag = true;
            }
            if (GamePlayCount >= 40 && GamePlayCount < 60)
            {
                BossWaveFlag = false;
                Wave = 1;
            }
            if (GamePlayCount >= 58 && GamePlayCount < 80)
            {
                BossWaveFlag = true;
               
            }
            if (GamePlayCount >= 80 && GamePlayCount < 125)
            {
                BossWaveFlag = false;
                Wave = 2;
               
            }
            if (GamePlayCount >= 125)
            {
                BossWaveFlag = true;
                Wave = 3;
            }
        }else
        {
            SpeedParticle.SetActive(false);
        }
        ///////////////////////////////////////
        
        
    }
    private void FixedUpdate()//敵出現
    {
        if (GameOverFlag == true) return;
        if (GameClearFlag == true) return;

        if (GameStartFlag == true)
        {
            int RandomEnemy = Random.Range(0, 15000); // 敵の出現頻度をランダムに決定
            int Style = Random.Range(0, 5); // 敵の種類をランダムに決定

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
            else if (Wave == 3)
            {
                waveModifier = 4;
            }

            // 出現条件（範囲）を設定
            int[][] Enemyranges = {
        new int[]{0, 5000 * waveModifier},    // WAVEに応じた出現範囲
        new int[]{100, 150 * waveModifier},   // 範囲2
        new int[]{150, 300 * waveModifier},   // 範囲3
        new int[]{300, 600 * waveModifier},   // 範囲4
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
                    if (CoolTime[i] >= 0.5f&&Wave<3)
                    {
                        // 攻撃可能な敵を生成
                        if (Style == 0 && Wave >= 1)
                        {
                            GameObject obj = Instantiate(enemy, positions[i], Quaternion.identity); // 敵を生成
                            obj.transform.position = new Vector3(obj.transform.position.x, 6.5f, obj.transform.position.z); // yを設定
                        }

                        // 隕石型の敵を生成
                        else if (Style == 1)
                        {
                            Instantiate(mineEnemy, MeteoPositions[i], Quaternion.identity); // 隕石敵を生成
                        }
                        // 回復機体の敵を生成
                        else if (Style == 2)
                        {
                            Instantiate(planeEnemy, positions[i], Quaternion.identity); // 黄色敵を生成
                        }
                        // 機体の敵を生成
                        else if (Style == 3 && Wave >= 1)
                        {
                            Instantiate(hevyplaneEnemy, positions[i], Quaternion.identity); // 重い敵を生成
                        }
                        // 機体の敵を生成
                        else if (Style == 4 && Wave >= 2)
                        {
                            Instantiate(blueplaneEnemy, positions[i], Quaternion.identity); // 青い敵を生成
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
    public void BatteryEnargyUp()
    {
        batteryEnargy += 1; // エネルギーを増加
    }

    public int GetBatteryEnargy()
    {
        return batteryEnargy; // 現在のエネルギー値を返す
    }
    public void HealBatteryEnargyReset()
    {
        healBatteryEnargy = 0; // バッテリーをリセット
    }
    public void HealBatteryEnargyUp()
    {
        healBatteryEnargy += 1; // バッテリーを増加
    }
    public int GetHealBatteryEnargy()
    {
        return healBatteryEnargy; // 現在のエネルギー値を返す
    }
    public void HealCounter()
    {
        healcount -= 1;//回復カウント
    }
    public int HealCount()
    {
        return healcount;
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
    public bool IsBossWave()
    {
        return BossWaveFlag;
    }
    public float IsGamePlayCount()
    {
        return GamePlayCount;
    }

    public void BossWaveCountStart()
    {
        GamePlayCount++;
    }
}
