using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    // ����
    public GameObject gameTimer;
    private GameTimer gameTimerScript;

    // �Q�[���}�l�[�W���[
    public GameObject gameManager;
    private GameManager gameManagerScript;

    // �����N�摜
    public GameObject SS_rank;
    public GameObject S_rank;
    public GameObject A_rank;
    public GameObject B_rank;
    public GameObject RankImage;
    public GameObject RankCanvas;

    private bool isRankOpen = false;

    // �Q�[���N���A
    public GameObject GameClearImage;

    // �Q�[���N���A�\��
    private float GameClearActiveTime = 0;

    public Image[] targetImages; // ������Image�R���|�[�l���g�𑀍삷�邽�߂̔z��
    private float targetAlpha = 0.0f; // �ڕW�̓����x�i0.0f�`1.0f�j
    private float fadeSpeed = 2.0f; // �t�F�[�h���x

    private bool hasRankDisplayed = false; // �����N�摜���\���ς݂�����

    void Start()
    {
        gameTimerScript = gameTimer.GetComponent<GameTimer>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        RankCanvas.SetActive(false);
        GameClearImage.SetActive(false);
        GameClearActiveTime = 0;
        targetAlpha = 1.0f;
        isRankOpen = false;
        SS_rank.SetActive(false);
        S_rank.SetActive(false);
        A_rank.SetActive(false);
        B_rank.SetActive(false);
        hasRankDisplayed = false; // ������Ԃł͕\������Ă��Ȃ�
    }

    void Update()
    {
        if (gameManagerScript.IsGameClear())
        {
            GameClearActiveTime += Time.deltaTime;

            if (GameClearActiveTime <= 2.5f)
            {
                GameClearImage.SetActive(true);
            }
            else
            {
                foreach (var image in targetImages)
                {
                    Color color = image.color;
                    color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                    image.color = color;
                }
                GameClearImage.SetActive(false);
            }

            if (GameClearActiveTime >= 3f && !hasRankDisplayed) // �����N���\���̏ꍇ�̂ݏ������s
            {
                isRankOpen = true;
                RankCanvas.SetActive(true);
                RankImage.SetActive(true);

                if (gameTimerScript.GetElapsedTime() <= 150)
                {
                    SS_rank.SetActive(true);
                }
                else if (gameTimerScript.GetElapsedTime() > 150 && gameTimerScript.GetElapsedTime() <= 140)
                {
                    S_rank.SetActive(true);
                }
                else if (gameTimerScript.GetElapsedTime() > 140 && gameTimerScript.GetElapsedTime() <= 160)
                {
                    A_rank.SetActive(true);
                }
                else if (gameTimerScript.GetElapsedTime() > 160)
                {
                    B_rank.SetActive(true);
                }

                // �����N���\�����ꂽ��Ƀ^�C�}�[���~
                gameTimerScript.StopTimer();

                hasRankDisplayed = true; // �����N�\���ς݃t���O���X�V
            }
        }
        else
        {
            RankCanvas.SetActive(false);
            GameClearImage.SetActive(false);
            GameClearActiveTime = 0;
            isRankOpen = false;
            hasRankDisplayed = false; // �Q�[���N���A�I�����̓t���O�����Z�b�g
        }
    }

    public void FadeOut()
    {
        targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        targetAlpha = 1.0f;
    }

    public bool IsRankOpen()
    {
        return isRankOpen;
    }
}