using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    private bool isPaused = false; // �|�[�Y�������t���O
    private Vector3 fixedPosition; // �|�[�Y���̌Œ�ʒu

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

    // �v���C���[�֘A
    public GameObject player; // �v���C���[�I�u�W�F�N�g

    void Start()
    {
        // GameManager
        gameManagerScript = gameManager.GetComponent<GameManager>();

        // PanelEffect
        PanelEffectScript = panelEffect.GetComponent<PanelEffect>();
        pauseMenuSelectorScript = pauseMenuSelector.GetComponent<PauseMenuSelector>();

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
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 7")&& pauseMenuSelectorScript.IsOperation()==false)
            {
                TogglePause();
                isPauseOn = true;
            }

            if (isPaused)
            {
                PauseImage.SetActive(true);
                Ui.SetActive(false);

                // �v���C���[�̈ʒu���Œ�
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

            // ����������
            if (pauseMenuSelectorScript.IsOperation())
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
            // �v���C���[�̌��݈ʒu���L�^���ČŒ�
            if (player != null)
            {
                fixedPosition = player.transform.position;
            }
        }
    }

    public void DisablePause() // �|�[�Y�������\�b�h
    {
        isPaused = false;
        Time.timeScale = 1;

        // UI�̃��Z�b�g
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
