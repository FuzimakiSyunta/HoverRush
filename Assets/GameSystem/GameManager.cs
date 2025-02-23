using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using static UnityEditor.PlayerSettings;
//using TMPro;


public class GameManager : MonoBehaviour
{
    //��v�I�u�W�F�N�g
    public GameObject enemy;
    public GameObject mineEnemy;
    public GameObject yellowEnemy;
    public GameObject player;
    //text&Image
    public GameObject gameOverText;
    public GameObject gameClearText;
    //public TextMeshProUGUI scoreText;
    public GameObject titleText;
    public GameObject StartButtonImage;

    //Select
    private bool OpenSelector = false;
    
    //�Q�[���V�X�e��
    private float[] CoolTime = new float[5];
    private bool GameOverFlag = false;
    private bool GameClearFlag = false;
    private bool GameStartFlag = false;
    private int score = 0;

    //WAVE
    public int Wave;
    public float BossWaveCount;
    private bool BossWaveFlag;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i<5;i++)
        {
            CoolTime[i] = 0;
        }
        titleText.SetActive(true);
        StartButtonImage.SetActive(true);
        Wave = 0;
        BossWaveCount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        ////�X�R�A
        //scoreText.text = "ENERGY  " + score;

        //�Z���N�g
        if (OpenSelector == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                OpenSelector = true;
                titleText.SetActive(false);
                StartButtonImage.SetActive(false);
                GameStartFlag = false;
            }
        }
        
        
        //WAVE
        if (GameStartFlag == true)
        {
            BossWaveCount += Time.deltaTime;
            if (BossWaveCount>=18&&BossWaveCount<=40)
            {
                BossWaveFlag = true;
                
            }
            if (BossWaveCount >= 40 && BossWaveCount < 60)
            {
                BossWaveFlag = false;
                Wave = 1;
               
            }
            if (BossWaveCount >= 58 && BossWaveCount < 80)
            {
                BossWaveFlag = true;
               
            }
            if (BossWaveCount >= 80 && BossWaveCount < 100)
            {
                BossWaveFlag = false;
                Wave = 2;
               
            }
            if (BossWaveCount >= 100)
            {
                BossWaveFlag = true;
                Wave = 3;
               
            }
        }
        
    }
    private void FixedUpdate()//�G�o��
    {
        if (GameOverFlag == true) return;

        //�{�X�E�F�[�u����Ȃ��Ƃ�
        if(BossWaveFlag==false)
        {
            //�G����
            if (GameStartFlag == true && Wave == 0)//WAVE0
            {
                int RandomEnemy = Random.Range(0, 9000);//�o���p�x
                int Style = Random.Range(0, 10);//�G�̎��
                int AttackEnemy = Random.Range(0, 10);//�U�����Ă���G
                for (int i = 0; i < 5; i++)
                {
                    CoolTime[i]++;
                }

                if (RandomEnemy <= 300)
                {
                    if (CoolTime[0] >= 60)
                    {
                        if (AttackEnemy == 0&&Style==0)
                        {
                            Instantiate(enemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 1)
                        {
                            Instantiate(mineEnemy, new Vector3(-8.0f, 8.0f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowEnemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[0] = 0;
                    }
                }
                if (RandomEnemy >= 2000 && RandomEnemy <= 2300)
                {
                    if (CoolTime[1] >= 60)
                    {
                        if (AttackEnemy == 1 && Style == 0)
                        {
                            Instantiate(enemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 1)
                        {
                            Instantiate(mineEnemy, new Vector3(0.0f, 8.0f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowEnemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[1] = 0;
                    }
                }
                if (RandomEnemy >= 4000 && RandomEnemy <= 4300)
                {
                    if (CoolTime[2] >= 60)
                    {
                        if (AttackEnemy == 2 && Style == 0)
                        {
                            Instantiate(enemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 1)
                        {
                            Instantiate(mineEnemy, new Vector3(8.0f, 8.0f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowEnemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[2] = 0;
                    }
                }
                if (RandomEnemy >= 6000 && RandomEnemy <= 6300)
                {
                    if (CoolTime[3] >= 60)
                    {
                        if (AttackEnemy == 3 && Style == 0)
                        {
                            Instantiate(enemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 1)
                        {
                            Instantiate(mineEnemy, new Vector3(4.0f, 8.0f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowEnemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[3] = 0;
                    }
                }
                if (RandomEnemy >= 7000 && RandomEnemy <= 7100)
                {
                    if (CoolTime[4] >= 60)
                    {
                        if (AttackEnemy == 3 && Style == 0)
                        {
                            Instantiate(enemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 1)
                        {
                            Instantiate(mineEnemy, new Vector3(-4.0f, 8.0f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowEnemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[4] = 0;
                    }
                }

            }

            if (GameStartFlag == true && Wave == 1)//WAVE1
            {
                int r = Random.Range(0, 7000);
                int EnemyStyle = Random.Range(0, 6);
                int AttackEnemyStyle = Random.Range(0, 4);
                
                for (int i = 0; i < 5; i++)
                {
                    CoolTime[i]++;
                }

                if (r <= 300)
                {
                    if (CoolTime[0] >= 60)
                    {
                        if (AttackEnemyStyle == 0 && EnemyStyle == 0)
                        {
                            Instantiate(enemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (EnemyStyle == 1)
                        {
                            Instantiate(mineEnemy, new Vector3(-8.0f, 8.0f, 45.0f), Quaternion.identity);
                        }
                        if (EnemyStyle == 2)
                        {
                            Instantiate(yellowEnemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[0] = 0;
                    }
                }
                if (r >= 1000 && r <= 1300)
                {
                    if (CoolTime[1] >= 60)
                    {
                        if (AttackEnemyStyle == 1 && EnemyStyle == 0)
                        {
                            Instantiate(enemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (EnemyStyle == 1)
                        {
                            Instantiate(mineEnemy, new Vector3(0.0f, 8.0f, 45.0f), Quaternion.identity);
                        }
                        if (EnemyStyle == 2)
                        {
                            Instantiate(yellowEnemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[1] = 0;
                    }
                }
                if (r >= 2000 && r <= 2300)
                {
                    if (CoolTime[2] >= 60)
                    {
                        if (AttackEnemyStyle == 2 && EnemyStyle == 0)
                        {
                            Instantiate(enemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (EnemyStyle == 1)
                        {
                            Instantiate(mineEnemy, new Vector3(8.0f, 8.0f, 45.0f), Quaternion.identity);
                        }
                        if (EnemyStyle == 2)
                        {
                            Instantiate(yellowEnemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[2] = 0;
                    }
                }
                if (r >= 4000 && r <= 4300)
                {
                    if (CoolTime[3] >= 60)
                    {
                        if (AttackEnemyStyle == 3 && EnemyStyle == 0)
                        {
                            Instantiate(enemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (EnemyStyle == 1)
                        {
                            Instantiate(mineEnemy, new Vector3(4.0f, 8.0f, 45.0f), Quaternion.identity);
                        }
                        if (EnemyStyle == 2)
                        {
                            Instantiate(yellowEnemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[3] = 0;
                    }
                }
                if (r >= 6000 && r <= 6300)
                {
                    if (CoolTime[4] >= 60)
                    {
                        if (AttackEnemyStyle == 3 && EnemyStyle == 0)
                        {
                            Instantiate(enemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (EnemyStyle == 1)
                        {
                            Instantiate(mineEnemy, new Vector3(-4.0f, 8.0f, 45.0f), Quaternion.identity);
                        }
                        if (EnemyStyle == 2)
                        {
                            Instantiate(yellowEnemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[4] = 0;
                    }
                }
            }
        }
        if (GameStartFlag == true && Wave == 2)//WAVE2
        {
            int r = Random.Range(0, 7000);
            int Style = Random.Range(0, 3);
            int AttackEnemyStyle = Random.Range(0, 3);
            for (int i = 0; i < 5; i++)
            {
                CoolTime[i]++;
            }

            if (r <= 300)
            {
                if (CoolTime[0] >= 60)
                {
                    if (AttackEnemyStyle == 0 && Style == 0)
                    {
                        Instantiate(enemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 1)
                    {
                        Instantiate(mineEnemy, new Vector3(-8.0f, 8.0f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 2)
                    {
                        Instantiate(yellowEnemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    CoolTime[0] = 0;
                }
            }
            if (r >= 1000 && r <= 1300)
            {
                if (CoolTime[1] >= 60)
                {
                    if (AttackEnemyStyle == 1 && Style == 0)
                    {
                        Instantiate(enemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 1)
                    {
                        Instantiate(mineEnemy, new Vector3(0.0f, 8.0f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 2)
                    {
                        Instantiate(yellowEnemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    CoolTime[1] = 0;
                }
            }
            if (r >= 2000 && r <= 2300)
            {
                if (CoolTime[2] >= 60)
                {
                    if (AttackEnemyStyle == 2 && Style == 0)
                    {
                        Instantiate(enemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 1)
                    {
                        Instantiate(mineEnemy, new Vector3(8.0f, 8.0f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 2)
                    {
                        Instantiate(yellowEnemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    CoolTime[2] = 0;
                }
            }
            if (r >= 4000 && r <= 4300)
            {
                if (CoolTime[3] >= 60)
                {
                    if (AttackEnemyStyle == 3 && Style == 0)
                {
                    Instantiate(enemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                }
                if (Style == 1)
                {
                    Instantiate(mineEnemy, new Vector3(4.0f, 8.0f, 45.0f), Quaternion.identity);
                }
                if (Style == 2)
                {
                    Instantiate(yellowEnemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                }
                    CoolTime[3] = 0;
                }
            }
            if (r >= 6000 && r <= 6300)
            {
                if (CoolTime[4] >= 60)
                {
                    if (AttackEnemyStyle == 3 && Style == 0)
                    {
                        Instantiate(enemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 1)
                    {
                        Instantiate(mineEnemy, new Vector3(-4.0f, 8.0f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 2)
                    {
                        Instantiate(yellowEnemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    CoolTime[4] = 0;
                }
            }
        }
    }

    public void GameOverStart()//�Q�[���I�[�o�[
    {
        GameOverFlag = true;
        gameOverText.SetActive(true);
    }
    public bool IsGameOver()
    {
        return GameOverFlag;
    }
    public void GameClearStart()//�Q�[���N���A
    {
        GameClearFlag = true;
        gameClearText.SetActive(true);
    }
    public bool IsGameClear()
    {
        return GameClearFlag;
    }
    public void Score()//�X�R�A
    {
        score += 1;
    }
    public int IsScore()
    {
        return score;
    }
    public void GameStart()//�Q�[���X�^�[�g
    {
        GameStartFlag = true;
    }
    public bool IsGameStart()
    {
        return GameStartFlag;
    }
    public bool IsOpenSelector()
    {
        return OpenSelector;
    }
    public bool IsBossWaveStart()
    {
        return BossWaveFlag;
    }
    public float IsBossWaveCount()
    {
        return BossWaveCount;
    }
}
