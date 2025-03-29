using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    private Result ResultScript;
    public GameObject result;
    private PauseMenuSelector pauseMenuSelectorScript;
    public GameObject pauseMenuSelector;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        pauseMenuSelectorScript = pauseMenuSelector.GetComponent<PauseMenuSelector>();
        ResultScript = result.GetComponent<Result>();
    }

    void Update()
    {
        // isTitleBackがtrueの場合にLoadシーンへ移動
        if (pauseMenuSelectorScript.IsTitleBack())
        {
            SceneManager.LoadScene("Load");
        }

        // ゲームオーバー処理
        if (gameManagerScript.IsGameOver())
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("Load");
            }
        }

        // ゲームクリア処理
        if (gameManagerScript.IsGameClear()&& ResultScript.IsRankOpen())
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("Load");
            }
        }
    }
}
