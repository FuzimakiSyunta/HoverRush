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
    private int[] CoolTime = new int[5];
    private bool GameOverFlag = false;
    private bool GameStartFlag = false;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public TextMeshProUGUI startText;
    private int Wave;
    private bool BossWaveFlag;
    private float BossWaveCount;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i<5;i++)
        {
            CoolTime[i] = 0;
        }
        startText.enabled = true;
        BossWaveCount = 0;
        Wave = 0;
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
        }
        if(GameStartFlag==true)
        {
            BossWaveCount = Time.time;
            if (BossWaveCount>=30&&BossWaveCount<=60)
            {
                BossWaveFlag = true;
            }
            else
            {
                BossWaveFlag = false;
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
                int r = Random.Range(0, 8500);
                int Style = Random.Range(0, 6);
                int AttackEnemyStyle = Random.Range(0, 10);
                CoolTime[0]++;
                CoolTime[1]++;
                CoolTime[2]++;
                CoolTime[3]++;
                CoolTime[4]++;

                if (r <= 300)
                {
                    if (CoolTime[0] >= 30)
                    {
                        if (AttackEnemyStyle == 0)
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
                    if (CoolTime[1] >= 30)
                    {
                        if (AttackEnemyStyle == 1)
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
                    if (CoolTime[2] >= 30)
                    {
                        if (AttackEnemyStyle == 2)
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
                    if (CoolTime[3] >= 30)
                    {
                        if (AttackEnemyStyle == 3)
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
                
            }

            if (GameStartFlag == true && Wave == 1)//WAVE1
            {
                int r = Random.Range(0, 20000);
                CoolTime[0]++;
                CoolTime[1]++;
                CoolTime[2]++;
                CoolTime[3]++;
                CoolTime[4]++;

                if (r <= 300)
                {
                    if (CoolTime[0] >= 30)
                    {
                        Instantiate(enemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                        CoolTime[0] = 0;
                    }
                }
                if (r >= 2000 && r <= 2300)
                {
                    if (CoolTime[1] >= 30)
                    {
                        Instantiate(enemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                        CoolTime[1] = 0;
                    }
                }
                if (r >= 4000 && r <= 4300)
                {
                    if (CoolTime[2] >= 30)
                    {
                        Instantiate(enemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                        CoolTime[2] = 0;
                    }
                }
                if (r >= 6000 && r <= 6300)
                {
                    if (CoolTime[3] >= 30)
                    {
                        Instantiate(enemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                        CoolTime[3] = 0;
                    }
                }
                if (r >= 8000 && r <= 8300)
                {
                    if (CoolTime[4] >= 30)
                    {
                        Instantiate(enemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                        CoolTime[4] = 0;
                    }
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
