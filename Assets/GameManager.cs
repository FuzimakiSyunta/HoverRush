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
    public GameObject player;
    public GameObject gameOverText;
    int[] CoolTime = new int[5];
    private bool GameOverFlag = false;
    private bool GameStartFlag = false;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public TextMeshProUGUI startText;


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
        }
        
    }
    private void FixedUpdate()
    {
        if (GameOverFlag == true) return;

        //敵生成
        if (GameStartFlag==true)
        {
            int r = Random.Range(0, 15000);
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
    public void GameStart()
    {
        GameStartFlag = false;
    }
    public bool IsGameStart()
    {
        return GameStartFlag;
    }
    
}
