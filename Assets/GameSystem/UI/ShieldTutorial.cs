using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTutorial : MonoBehaviour
{
    private BossScript bossScript; // BossScript�̎Q��
    public GameObject boss;        // Boss�I�u�W�F�N�g

    private GameManager gameManagerScript; // GameManager�̎Q��
    public GameObject gameManager; // GameManager�I�u�W�F�N�g

    public GameObject shieldLuleImage;   // �ŏ��ɕ\������摜�iGameObject�j
    public GameObject bossattackImage;   // �؂�ւ���摜�iGameObject�j

    public GameObject gameTimer;
    private GameTimer gameTimerScript;

    private bool isImage1Active = true;
    private bool hasTriggered = false;
    private bool isPaused = false;

    private void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        gameTimerScript = gameTimer.GetComponent<GameTimer>();

        // �ŏ��͔�\��
        shieldLuleImage.SetActive(false);
        bossattackImage.SetActive(false);
    }

    void Update()
    {
        if(gameManagerScript.IsGameStart()&&!gameManagerScript.IsGameOver()&&!gameManagerScript.IsGameClear())
        {
            // ���Ԓ�~�̃g���K�[
            if (!hasTriggered && gameTimerScript.GetElapsedTime()>=131)
            {
                shieldLuleImage.SetActive(true);
                bossattackImage.SetActive(false);

                isImage1Active = true;
                Time.timeScale = 0f;
                isPaused = true;
                hasTriggered = true;
            }

            if (isPaused)
            {
                // Space�L�[�ŉ摜�؂�ւ�
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SwitchImage();
                }

                // Enter�L�[�Ŏ��ԍĊJ
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Time.timeScale = 1f;
                    isPaused = false;

                    // �摜���\���ɖ߂�
                    shieldLuleImage.SetActive(false);
                    bossattackImage.SetActive(false);
                }
            }
        }

    }

    void SwitchImage()
    {
        isImage1Active = !isImage1Active;

        shieldLuleImage.SetActive(isImage1Active);
        bossattackImage.SetActive(!isImage1Active);
    }
}
