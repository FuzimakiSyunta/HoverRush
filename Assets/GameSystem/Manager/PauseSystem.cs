using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    private bool isPaused = false;//pauseしたかフラグ
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;
    //SelectorMenu
    private SelectorMenu selectorMenuScript;
    public GameObject selectorMenu;

    //PanelEffect
    private PanelEffect PanelEffectScript;
    public GameObject panelEffect;

    private PauseMenuSelector pauseMenuSelectorScript;
    public GameObject pauseMenuSelector;

    //Image
    public GameObject PauseImage;

    public GameObject Ui;

    private bool isPauseOn;

    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //SelectorMenu
        selectorMenuScript = selectorMenu.GetComponent<SelectorMenu>();
        //PanelEffect
        PanelEffectScript = panelEffect.GetComponent<PanelEffect>();
        pauseMenuSelectorScript = pauseMenuSelector.GetComponent<PauseMenuSelector>();
        PauseImage.SetActive(false);
        Ui.SetActive(true);
        isPaused = false;
        isPauseOn = false;
    }

    void Update()
    {
        if(gameManagerScript.IsGameStart() == true&& PanelEffectScript.IsAlpha()==true)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 7"))
            {
                TogglePause();
                isPauseOn = true;
            }
            if(isPaused)
            {
                PauseImage.SetActive(true);
                Ui.SetActive(false);
                
            }
            else
            {
                PauseImage.SetActive(false);
                Ui.SetActive(true);
                isPauseOn = false;
            }
        }

    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

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
