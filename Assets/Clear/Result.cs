using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    //����
    public GameObject gameTimer;
    private GameTimer gameTimerScript;
    //�Q�[���}�l�[�W���[
    public GameObject gameManager;
    private GameManager gameManagerScript;

    //�����N�摜
    public GameObject S_rank;
    public GameObject A_rank;
    public GameObject B_rank;
    public GameObject RankImage;
    public GameObject RankCanvas;

    private bool isRankOpen = false;

    //�Q�[���N���A
    public GameObject GameClearImage;

    //�Q�[���N���A�\��
    private float GameClearActiveTime = 0;

    public Image[] targetImages; // ������Image�R���|�[�l���g�𑀍삷�邽�߂̔z��
    private float targetAlpha = 0.0f; // �ڕW�̓����x�i0.0f�`1.0f�j
    private float fadeSpeed = 2.0f; // �t�F�[�h���x

    void Start()
    {
        gameTimerScript = gameTimer.GetComponent<GameTimer>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        RankCanvas.SetActive(false);
        GameClearImage.SetActive(false);
        GameClearActiveTime = 0;
        targetAlpha = 1.0f;
        isRankOpen = false;
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
            if (GameClearActiveTime >= 3f)
            {
                isRankOpen = true;
                RankCanvas.SetActive(true);
                RankImage.SetActive(true);

                int elapsedTime = (int)gameTimerScript.GetTotalElapsedTime();
                if (elapsedTime <= 120)
                {
                    S_rank.SetActive(true);
                    A_rank.SetActive(false);
                    B_rank.SetActive(false);
                }
                else if (elapsedTime > 120 && elapsedTime <= 140)
                {
                    S_rank.SetActive(false);
                    A_rank.SetActive(true);
                    B_rank.SetActive(false);
                }
                else if (elapsedTime > 140)
                {
                    S_rank.SetActive(false);
                    A_rank.SetActive(false);
                    B_rank.SetActive(true);
                }
            }
        }
        else
        {
            RankCanvas.SetActive(false);
            GameClearImage.SetActive(false);
            GameClearActiveTime = 0;
            isRankOpen = false;
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