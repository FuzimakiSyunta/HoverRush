using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrindScript : MonoBehaviour
{
    // GameManager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    // Boss
    public GameObject boss;
    public GameObject bossBullet;
    //開始前UI
    public GameObject StartTitleUi;
    //開始後UI
    public GameObject StartUi;
    //GameOver&GameClear
    public GameObject ClearGameOverUI;
    //Energy
    public GameObject AllEnergy;
    public GameObject EnergyMIN;
    public GameObject EnergyMID;
    public GameObject EnergyMAX;
    //Selector
    public GameObject Selector;
    //最初のタイトル
    public GameObject TitleUi;
    //
    public GameObject GameClear;
    public GameObject GameOver;
    //
    public GameObject OptionButton;


    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();

        // Initialize Elements
        SetActiveForObjects(false, boss, bossBullet, StartUi,GameOver,GameClear,ClearGameOverUI,OptionButton, AllEnergy);
        SetActiveForObjects(true, StartTitleUi);
    }

    void Update()
    {
        // ゲーム開始状態の確認
        if (gameManagerScript.IsGameStart())
        {
            if (!gameManagerScript.IsGameClear() && !gameManagerScript.IsGameOver())
            {
                SetActiveForObjects(false, StartTitleUi, ClearGameOverUI);
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
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX);
            SetActiveForObjects(true, GameOver);

            // デバッグ用ログ
            Debug.Log("ゲームオーバー: GameOver画面を表示しました。");
        }
    }

    private void HandleGameClear()
    {
        if (gameManagerScript.IsGameClear())
        {
            // GameClear画面を表示
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX);
            SetActiveForObjects(true, GameClear);

            // デバッグ用ログ
            Debug.Log("ゲームクリア: GameClear画面を表示しました。");
        }
    }

    private void HandleEnergyLevels()//energy
    {
        if (gameManagerScript.IsGameStart())
        {
            if (gameManagerScript.IsScore() >= 5) EnergyMIN.SetActive(true);
            if (gameManagerScript.IsScore() >= 10) EnergyMID.SetActive(true);
            if (gameManagerScript.IsScore() >= 15) EnergyMAX.SetActive(true);
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
