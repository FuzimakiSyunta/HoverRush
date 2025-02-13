using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameOver() == true)//ゲームオーバー時
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("Load");
            }
        }

        if (gameManagerScript.IsGameClear() == true)//ゲームクリア時
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("Clear");
            }
        }

    }
}
