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
    private SelectorMenu selectorMenuScrpt;
    public GameObject selectorMenu;

    //Image
    public GameObject PauseImage;

    public GameObject AllUi;

    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //SelectorMenu
        selectorMenuScrpt = selectorMenu.GetComponent<SelectorMenu>();
        PauseImage.SetActive(false);
        AllUi.SetActive(true);
    }

    void Update()
    {
        if(gameManagerScript.IsGameStart() == true)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 7"))
            {
                TogglePause();
            }
            if(isPaused)
            {
                PauseImage.SetActive(true);
                AllUi.SetActive(false);
            }else
            {
                PauseImage.SetActive(false);
                AllUi.SetActive(true);
            }
        }
        
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        // 一時停止中にゲームオブジェクトを停止する例
        foreach (var obj in GameObject.FindGameObjectsWithTag("Pauseable"))
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = isPaused;
            }
        }
    }
}
