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
    private bool OpenSelector = false;
    
    //ゲームシステム
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

    public int IsWave()
    {
        return Wave;
    }
}
