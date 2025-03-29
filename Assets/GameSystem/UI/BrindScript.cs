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



    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //playerModels
        playerModelsScript = playerModels.GetComponent<PlayerModels>();

        // Initialize Elements
        SetActiveForObjects(false, boss, bossBullet, StartUi, GameOver, OptionButton, AllEnergy);
        PowerUpImage.SetActive(false); // 個別に制御
        SetActiveForObjects(true, StartTitleUi);
    }

    void Update()
    {
        // ゲーム開始状態の確認
        if (gameManagerScript.IsGameStart())
        {
            if (!gameManagerScript.IsGameClear() && !gameManagerScript.IsGameOver())
            {
                SetActiveForObjects(false, StartTitleUi,GameOver,PowerUpImage);
                SetActiveForObjects(true, boss, bossBullet, StartUi, OptionButton, AllEnergy);
                HandleEnergyLevels(); // エネルギー管理
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


        // ゲームの終了/クリア処理
        HandleGameOver(); // GameOverロジック
        HandleGameClear(); // GameClearロジック
    }


    private void HandleGameOver()
    {
        if (gameManagerScript.IsGameOver())
        {
            // GameOver画面を表示
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX,PowerUpImage);
            SetActiveForObjects(true, GameOver);

            
        }
    }

    private void HandleGameClear()
    {
        if (gameManagerScript.IsGameClear())
        {
            // GameClear画面を表示
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX,PowerUpImage);

        }
    }

    private IEnumerator HidePowerUpImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PowerUpImage.SetActive(false);
        hasPowerUpImageBeenHidden = true; // 非表示にされたことを記録
    }



    private void HandleEnergyLevels() // エネルギー管理
    {
        if (gameManagerScript.IsGameStart())
        {
            //自機タイプ単発時
            if (playerModelsScript.IsIndex() == 0)
            {
                if (gameManagerScript.IsBatteryEnargy() >= 5) EnergyMIN.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 10) EnergyMID.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 15 && !hasPowerUpImageBeenHidden)
                {
                    EnergyMAX.SetActive(true);
                    PowerUpImage.SetActive(true);
                    // PowerUpImageを2.0秒後に非表示にするコルーチンを開始
                    StartCoroutine(HidePowerUpImageAfterDelay(2.0f));

                }
            }
            
            //自機タイプレーザー時
            if (playerModelsScript.IsIndex() == 1)
            {
                if (gameManagerScript.IsBatteryEnargy() >= 5) EnergyMIN.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 10) EnergyMID.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 15) EnergyMAX.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 20 && !hasPowerUpImageBeenHidden)
                {

                    PowerUpImage.SetActive(true);
                    // PowerUpImageを2.0秒後に非表示にするコルーチンを開始
                    StartCoroutine(HidePowerUpImageAfterDelay(2.0f));

                }
            }

            //自機タイプ貫通弾時
            if (playerModelsScript.IsIndex() == 2)
            {
                if (gameManagerScript.IsBatteryEnargy() >= 5) EnergyMIN.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 10) EnergyMID.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 15) EnergyMAX.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 30 && !hasPowerUpImageBeenHidden)
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
