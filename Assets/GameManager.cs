using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;


public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject blueenemy;
    public GameObject yellowenemy;
    public GameObject player;
    public GameObject gameOverText;
    private float[] CoolTime = new float[5];
    private bool GameOverFlag = false;
    private bool GameStartFlag = false;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public TextMeshProUGUI startText;
    public int Wave = 0;
    private float BossWaveCount;
    private bool BossWaveFlag;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i<5;i++)
        {
            CoolTime[i] = 0;
        }
        startText.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        //スコア
        scoreText.text = "SCORE" + score;

        //スタート
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameStartFlag = true;
            startText.enabled = false;
            BossWaveCount = 0;
            Wave = 0;
        }
        if(GameStartFlag==true)
        {
            BossWaveCount = Time.time;
            if (BossWaveCount>=15&&BossWaveCount<=40)
            {
                BossWaveFlag = true;
            }
            if (BossWaveCount >= 40 && BossWaveCount < 60)
            {
                BossWaveFlag = false;
                Wave = 1;
            }
            if (BossWaveCount >= 55 && BossWaveCount < 80)
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
    private void FixedUpdate()
    {
        if (GameOverFlag == true) return;

        //ボスウェーブじゃないとき
        if(BossWaveFlag==false)
        {
            //敵生成
            if (GameStartFlag == true && Wave == 0)//WAVE0
            {
                int r = Random.Range(0, 9000);
                int Style = Random.Range(0, 10);
                int AttackEnemyStyle = Random.Range(0, 10);
                for (int i = 0; i < 5; i++)
                {
                    CoolTime[i]++;
                }

                if (r <= 300)
                {
                    if (CoolTime[0] >= 60)
                    {
                        if (AttackEnemyStyle == 0&&Style==0)
                        {
                            Instantiate(enemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 1)
                        {
                            Instantiate(blueenemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowenemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[0] = 0;
                    }
                }
                if (r >= 2000 && r <= 2300)
                {
                    if (CoolTime[1] >= 60)
                    {
                        if (AttackEnemyStyle == 1 && Style == 0)
                        {
                            Instantiate(enemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 1)
                        {
                            Instantiate(blueenemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowenemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[1] = 0;
                    }
                }
                if (r >= 4000 && r <= 4300)
                {
                    if (CoolTime[2] >= 60)
                    {
                        if (AttackEnemyStyle == 2 && Style == 0)
                        {
                            Instantiate(enemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 1)
                        {
                            Instantiate(blueenemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowenemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[2] = 0;
                    }
                }
                if (r >= 6000 && r <= 6300)
                {
                    if (CoolTime[3] >= 60)
                    {
                        if (AttackEnemyStyle == 3 && Style == 0)
                        {
                            Instantiate(enemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 1)
                        {
                            Instantiate(blueenemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowenemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[3] = 0;
                    }
                }
                if (r >= 7000 && r <= 7100)
                {
                    if (CoolTime[4] >= 60)
                    {
                        if (AttackEnemyStyle == 3 && Style == 0)
                        {
                            Instantiate(enemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 1)
                        {
                            Instantiate(blueenemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowenemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        CoolTime[4] = 0;
                    }
                }

            }

            if (GameStartFlag == true && Wave == 1)//WAVE1
            {
                int r = Random.Range(0, 7000);
                int Style = Random.Range(0, 6);
                int AttackEnemyStyle = Random.Range(0, 4);
                
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
                            Instantiate(blueenemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowenemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
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
                            Instantiate(blueenemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowenemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
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
                            Instantiate(blueenemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowenemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
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
                            Instantiate(blueenemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowenemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
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
                            Instantiate(blueenemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                        }
                        if (Style == 2)
                        {
                            Instantiate(yellowenemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
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
                        Instantiate(blueenemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 2)
                    {
                        Instantiate(yellowenemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
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
                        Instantiate(blueenemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 2)
                    {
                        Instantiate(yellowenemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
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
                        Instantiate(blueenemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 2)
                    {
                        Instantiate(yellowenemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
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
                    Instantiate(blueenemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                }
                if (Style == 2)
                {
                    Instantiate(yellowenemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
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
                        Instantiate(blueenemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    if (Style == 2)
                    {
                        Instantiate(yellowenemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                    }
                    CoolTime[4] = 0;
                }
            }
        }
    }

    public void GameOverStart()
    {
        GameOverFlag = true;
        gameOverText.SetActive(true);
    }
    public bool IsGameOver()
    {
        return GameOverFlag;
    }
    public void Score()
    {
        score += 1;
    }
    public int IsScore()
    {
        return score;
    }
    public void GameStart()
    {
        GameStartFlag = false;
    }
    public bool IsGameStart()
    {
        return GameStartFlag;
    }
    
}
