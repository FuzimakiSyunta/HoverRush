using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrindScript : MonoBehaviour
{
    // GameManager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    //models
    private PlayerModels playerModelsScript;
    public GameObject playerModels;

    // Boss
    public GameObject boss;
    public GameObject bossBullet;
    //public GameObject RobotBullet;
    //開始前UI
    public GameObject StartTitleUi;
    //開始後UI
    public GameObject StartUi;
    //Energy
    public GameObject AllEnergy;
    public GameObject EnergyMIN;
    public GameObject EnergyMID;
    public GameObject EnergyMAX;
    //Selector
    public GameObject Selector;
    //最初のタイトル
    public GameObject TitleUi;
    //ゲームオーバー
    public GameObject GameOver;
    //設定ボタン
    public GameObject OptionButton;
    //パワーアップ
    public GameObject PowerUpImage;
    private bool hasPowerUpImageBeenHidden = false;
    //回復可能
    public GameObject HealOkImage;
    private bool hasHealOkImageBeenHidden = false;


    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //playerModels
        playerModelsScript = playerModels.GetComponent<PlayerModels>();

        // Initialize Elements
        SetActiveForObjects(false, boss, bossBullet, StartUi, GameOver, OptionButton, AllEnergy);
        PowerUpImage.SetActive(false); // 個別に制御
        HealOkImage.SetActive(false); // 個別に制御
        SetActiveForObjects(true, StartTitleUi);
    }

    void Update()
    {
        // ゲーム開始状態の確認
        if (gameManagerScript != null && gameManagerScript.IsGameStart())
        {
            if (!gameManagerScript.IsGameClear() && !gameManagerScript.IsGameOver())
            {
                SetActiveForObjects(false, StartTitleUi, GameOver, PowerUpImage, HealOkImage);
                SetActiveForObjects(true, boss, bossBullet, StartUi, OptionButton, AllEnergy);
                HandleEnergyLevels();
            }else
            {
                Destroy(AllEnergy);
                AllEnergy = null;
            }

            if (gameManagerScript.GetHealBatteryEnargy() <= 2&& !gameManagerScript.IsGameClear() && !gameManagerScript.IsGameOver())
            {
                EnergyMIN.SetActive(false);
                EnergyMID.SetActive(false);
                EnergyMAX.SetActive(false);
            }
        }

        // セレクタの状態確認
        if (gameManagerScript.IsOpenSelector())
        {
            SetActiveForObjects(true, Selector); // Selector画像を有効化
        }
        else
        {
            SetActiveForObjects(false, Selector); // セレクタが閉じた場合は非表示
        }

        //回復UIロジック/////////////////////////////////////////////////////////////////////////////////////////////////
        if (playerModelsScript.IsIndex() == 0 || playerModelsScript.IsIndex() == 1 || playerModelsScript.IsIndex() == 2)
        {
            int healBatteryEnergy = gameManagerScript.GetHealBatteryEnargy();

            // エネルギー状態を切り替え
            UpdateEnergyState(healBatteryEnergy);

            // エネルギーが最大に達している場合
            if (healBatteryEnergy >= 9 && !hasHealOkImageBeenHidden&&gameManagerScript.IsGameOver()==false&&gameManagerScript.IsGameClear()==false)
            {
                HealOkImage.SetActive(true);
                StartCoroutine(HideHealOkImageAfterDelay(2.0f)); // 2秒後に非表示
            }
            // 状態リセットのための条件
            if (healBatteryEnergy < 9)
            {
                hasHealOkImageBeenHidden = false; // フラグをリセット
            }


        }
        // ゲームの終了/クリア処理
        HandleGameOver(); // GameOverロジック
        HandleGameClear(); // GameClearロジック
    }

    void UpdateEnergyState(int healBatteryEnergy)
    {
        if(!gameManagerScript.IsGameClear() && !gameManagerScript.IsGameOver())
        {
            // エネルギーUIの状態を管理
            EnergyMIN.SetActive(healBatteryEnergy >= 3);
            EnergyMID.SetActive(healBatteryEnergy >= 6);
            EnergyMAX.SetActive(healBatteryEnergy >= 9);
        }
        
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   


    private void HandleGameOver()
    {
        if (gameManagerScript.IsGameOver())
        {
            // GameOver画面を表示
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX,PowerUpImage,HealOkImage);
            SetActiveForObjects(true, GameOver);

            
        }
    }

    private void HandleGameClear()
    {
        if (gameManagerScript.IsGameClear())
        {
            // GameClear画面を表示
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX,PowerUpImage, HealOkImage);

        }
    }

    private IEnumerator HidePowerUpImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PowerUpImage.SetActive(false);
        hasPowerUpImageBeenHidden = true; // 非表示にされたことを記録
    }

    private IEnumerator HideHealOkImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HealOkImage.SetActive(false);
        hasHealOkImageBeenHidden = true; // 非表示にされたことを記録
    }

    private void HandleEnergyLevels() // エネルギー管理
    {
        if (gameManagerScript.IsGameStart())
        {
            
            //自機タイプ単発時
            if (playerModelsScript.IsIndex() == 0)
            {
                
                if (gameManagerScript.GetBatteryEnargy() >= 25 && !hasPowerUpImageBeenHidden)
                {

                    PowerUpImage.SetActive(true);
                    // PowerUpImageを2.0秒後に非表示にするコルーチンを開始
                    StartCoroutine(HidePowerUpImageAfterDelay(2.0f));

                }
            }
            
            //自機タイプレーザー時
            if (playerModelsScript.IsIndex() == 1)
            {
                
                if (gameManagerScript.GetBatteryEnargy() >= 20 && !hasPowerUpImageBeenHidden)
                {

                    PowerUpImage.SetActive(true);
                    // PowerUpImageを2.0秒後に非表示にするコルーチンを開始
                    StartCoroutine(HidePowerUpImageAfterDelay(2.0f));

                }
            }

            //自機タイプ貫通弾時
            if (playerModelsScript.IsIndex() == 2)
            {
               
                if (gameManagerScript.GetBatteryEnargy() >= 30 && !hasPowerUpImageBeenHidden)
                {

                    PowerUpImage.SetActive(true);
                    // PowerUpImageを2.0秒後に非表示にするコルーチンを開始
                    StartCoroutine(HidePowerUpImageAfterDelay(2.0f));

                }
            }

        }
    }


    private void SetActiveForObjects(bool state, params GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null) obj.SetActive(state);
        }
    }

    
}
