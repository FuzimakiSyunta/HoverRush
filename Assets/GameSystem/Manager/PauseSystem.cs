using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.HDROutputUtils;

public class PauseSystem : MonoBehaviour
{
    private bool isPaused = false; // ポーズしたかフラグ
    private Vector3 fixedPosition; // ポーズ中の固定位置

    // GameManager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    // PanelEffect
    private PanelEffect PanelEffectScript;
    public GameObject panelEffect;

    private PauseMenuSelector pauseMenuSelectorScript;
    public GameObject pauseMenuSelector;

    // Image
    public GameObject PauseImage;

    public GameObject Ui;

    private bool isPauseOn;

    // プレイヤー関連
    public GameObject player; // プレイヤーオブジェクト
    private Rigidbody playerRigidbody; // プレイヤーのRigidbody

    void Start()
    {
        // GameManager
        gameManagerScript = gameManager.GetComponent<GameManager>();

        // PanelEffect
        PanelEffectScript = panelEffect.GetComponent<PanelEffect>();
        pauseMenuSelectorScript = pauseMenuSelector.GetComponent<PauseMenuSelector>();

        // プレイヤーのRigidbody取得
        playerRigidbody = player.GetComponent<Rigidbody>();

        PauseImage.SetActive(false);
        Ui.SetActive(true);
        isPaused = false;
        isPauseOn = false;
    }

    void Update()
    {
        if (gameManagerScript.IsGameStart() == true && PanelEffectScript.IsAlpha() == true &&
            gameManagerScript.IsGameOver() == false && gameManagerScript.IsGameClear() == false)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 7"))
            {
                TogglePause();
                isPauseOn = true;
            }

            if (isPaused)
            {
                PauseImage.SetActive(true);
                Ui.SetActive(false);

                // プレイヤーの位置を固定
                if (player != null)
                {
                    player.transform.position = fixedPosition;
                }
            }
            else
            {
                PauseImage.SetActive(false);
                Ui.SetActive(true);
                isPauseOn = false;
            }

            // 操作説明画面
            if (pauseMenuSelectorScript.IsOperation() == true)
            {
                PauseImage.SetActive(false);
                if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1"))
                {
                    PauseImage.SetActive(true);
                }
            }
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        if (isPaused)
        {
            // プレイヤーの現在位置を記録して固定
            if (player != null)
            {
                fixedPosition = player.transform.position;
            }

            // Rigidbodyの動作を停止
            if (playerRigidbody != null)
            {
                playerRigidbody.isKinematic = true;
            }
        }
        else
        {
            // ポーズ解除時にRigidbodyを再有効化
            if (playerRigidbody != null)
            {
                playerRigidbody.isKinematic = false;
            }
        }

        // 一時停止中にゲームオブジェクトを停止する
        foreach (var obj in GameObject.FindGameObjectsWithTag("Pauseable"))
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = isPaused;
            }
        }
    }

    public void DisablePause() // ポーズ解除メソッド
    {
        isPaused = false;
        Time.timeScale = 1;

        // プレイヤーの動きを再開
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = false;
        }

        // 一時停止状態のオブジェクトを元に戻す
        foreach (var obj in GameObject.FindGameObjectsWithTag("Pauseable"))
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }

        // UIのリセット
        PauseImage.SetActive(false);
        Ui.SetActive(true);
        Debug.Log("Pause system disabled.");
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public bool IsPausedOn()
    {
        return isPauseOn;
    }
}
