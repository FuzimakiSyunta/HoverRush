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

    public Image targetImage; // �����x�𑀍삷��Ώۂ�Image�R���|�[�l���g
    private float targetAlpha = 0.0f; // �ڕW�̓����x�i0.0f�`1.0f�j
    private float fadeSpeed = 2.0f; // �t�F�[�h���x

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        // ���݂̓����x���擾
        Color color = targetImage.color;
        //�N���A������
        if (gameManagerScript.IsGameClear())
        {
            GameClearActiveTime += Time.deltaTime;
            if(GameClearActiveTime <=2f)
            {
                GameClearImage.SetActive(true);
            }
            else
            {
                // �ڕW�̓����x�ɋ߂Â���
                color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                GameClearImage.SetActive(false);
            }
            if(GameClearActiveTime>=3f)
            {
                isRankOpen = true;
                ///�����N�\��////////////////////////////////////
                RankCanvas.SetActive(true);
                RankImage.SetActive(true);
                if (gameManagerScript.GetBatteryEnargy() >= 200)
                {
                    S_rank.SetActive(true);
                    A_rank.SetActive(false);
                    B_rank.SetActive(false);
                }
                else if (gameManagerScript.GetBatteryEnargy() >= 130 && gameManagerScript.GetBatteryEnargy() < 200)
                {
                    S_rank.SetActive(false);
                    A_rank.SetActive(true);
                    B_rank.SetActive(false);
                }
                else if (gameManagerScript.GetBatteryEnargy() < 130)
                {
                    S_rank.SetActive(false);
                    A_rank.SetActive(false);
                    B_rank.SetActive(true);
                }
                /////////////////////////////////////////////////
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
    // �����x�������郁�\�b�h
    public void FadeOut()
    {
        targetAlpha = 0.0f; // �����x�����S��0�ɂ���
    }

    // �����x���グ�郁�\�b�h
    public void FadeIn()
    {
        targetAlpha = 1.0f; // �����x�����S��1�ɂ���
    }

    public bool IsRankOpen() 
    {
        return isRankOpen;
    }
}
