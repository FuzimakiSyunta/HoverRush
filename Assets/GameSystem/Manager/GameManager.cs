using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

using TMPro;


public class GameManager : MonoBehaviour
{
    //主要オブジェクト
    public GameObject UI;
    //text&Image
    public GameObject gameOverText;
    public GameObject gameClearText;
    public GameObject titleText;
    public GameObject StartButtonImage;

    //Select
    private bool isSelectorOpened = false;
    
    //ゲームシステム
    private bool GameOverFlag = false;
    private bool GameClearFlag = false;
    private bool GameStartFlag = false;
    public int batteryEnergy = 0;//強化用
    public int healBatteryEnergy = 0;//回復用
    private int healcount = 5;//回復回数

    //WAVE
    public int Wave;
    public float GamePlayCount;
    private bool BossWaveFlag;

    // WAVE数
    private const int MaxWave = 4;

    // 各WAVEが始まったか
    private bool[] waveStarted = new bool[MaxWave];
    // 各WAVEのボスフェーズが始まったか
    private bool[] bossStarted = new bool[MaxWave];
    // 通常WAVEの開始時間
    private float[] waveStartTimes = { 0f, 40f, 80f, 125f };

    // ボスWAVEの開始時間
    private float[] bossStartTimes = { 18f, 58f, -1f, 125f }; // -1 は Wave2 にボスがないと仮定



    //ゲーム開始時出現
    public GameObject SpeedParticle;

    // Start is called before the first frame update
    void Start()
    {
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
        if (isSelectorOpened == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                isSelectorOpened = true;
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
        if (GameStartFlag)
        {
            GamePlayCount += Time.deltaTime;
            SpeedParticle.SetActive(true);

            for (int i = 0; i < MaxWave; i++)
            {
                // 通常WAVE開始判定
                if (!waveStarted[i] && GamePlayCount >= waveStartTimes[i])
                {
                    Wave = i;
                    BossWaveFlag = false;
                    waveStarted[i] = true;
                    Debug.Log($"Wave {i} Started");
                }

                // ボスWAVE開始判定（有効な時間のみ）
                if (!bossStarted[i] && bossStartTimes[i] > 0 && GamePlayCount >= bossStartTimes[i])
                {
                    BossWaveFlag = true;
                    bossStarted[i] = true;
                    Debug.Log($"Wave {i} Boss Started");
                }
            }
        }
        else
        {
            SpeedParticle.SetActive(false);
        }
        ///////////////////////////////////////
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
        batteryEnergy += 1; // エネルギーを増加
    }

    public int GetBatteryEnargy()
    {
        return batteryEnergy; // 現在のエネルギー値を返す
    }
    public void HealBatteryEnargyReset()
    {
        healBatteryEnergy = 0; // バッテリーをリセット
    }
    public void HealBatteryEnargyUp()
    {
        healBatteryEnergy += 1; // バッテリーを増加
    }
    public int GetHealBatteryEnargy()
    {
        return healBatteryEnergy; // 現在のエネルギー値を返す
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
        return isSelectorOpened;
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

    public int IsWave()
    {
        return Wave;
    }
}
