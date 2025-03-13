using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    private PauseMenuSelector pauseMenuSelectorScript;
    public GameObject pauseMenuSelector;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        pauseMenuSelectorScript = pauseMenuSelector.GetComponent<PauseMenuSelector>();
    }

    void Update()
    {
        // isTitleBack��true�̏ꍇ��Load�V�[���ֈړ�
        if (pauseMenuSelectorScript.IsTitleBack())
        {
            SceneManager.LoadScene("Load");
        }

        // �Q�[���I�[�o�[����
        if (gameManagerScript.IsGameOver())
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("Load");
            }
        }

        // �Q�[���N���A����
        if (gameManagerScript.IsGameClear())
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("Clear");
            }
        }
    }
}
