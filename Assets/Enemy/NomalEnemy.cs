﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class NomalEnemy : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;
    private Animator animator;
    public int enemyHP;// 敵の最大HP
    private int EnemyNowHP;  // 敵の現在のHP
    public Slider hpSlider; //HPバー（スライダー）
    public ParticleSystem particle;
    public bool sliderBool;
    private float MoveSpeed = 0.02f;
    private float BackmoveSpeed = -0.05f;

    //private float MoveSpeed = 0.05f;
    //private float BackmoveSpeed = -0.15f;
    //private float[] bulletTimer = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        hpSlider.value = enemyHP;//HPバーの最初の値（最大HP）を設定
        EnemyNowHP = enemyHP; // 現在のHPを最大HPに設定
        hpSlider.gameObject.SetActive(false);
        sliderBool = false;
        Destroy(gameObject, 10);
        if (gameManagerScript.IsGameOver() == true)
        {
            return;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameStart() == true)
        {
            Vector3 velocity = new Vector3(0, 0, BackmoveSpeed);
            transform.position += transform.rotation * velocity;
        }
        // スライダーの向きをカメラ方向に固定
        hpSlider.transform.rotation = Camera.main.transform.rotation;

        //移動
        Vector3 position = transform.position;

        transform.position += new Vector3(0, 0, MoveSpeed);

        //スライダー表示
        if (sliderBool == true)
        {
            hpSlider.gameObject.SetActive(true);
        }
        // HPが0以下になった場合、自らを消す
        if (EnemyNowHP <= 0)
        {
            ParticleSystem newParticle = Instantiate(particle);
            //場所固定
            newParticle.transform.position = this.gameObject.transform.position;
            //発生
            newParticle.Play();
            //エフェクト消える
            Destroy(newParticle.gameObject, 0.5f);

            //スコア上昇
            gameManagerScript.Score();
            //敵消える
            Destroy(gameObject, 0f);


        }
    }
    void OnTriggerEnter(Collider other)
    {
        
        //敵と弾
        if (other.gameObject.tag == "Bullet")
        {
            EnemyNowHP -= 20;//一度当たるごとに20をマイナス
            hpSlider.value = (float)EnemyNowHP / (float)enemyHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
                                                                //Slider表示
            sliderBool = true;
        }
        //敵とマシンガン
        if (other.gameObject.tag == "Machinegun")
        {
        
            EnemyNowHP -= 15;//一度当たるごとに10をマイナス
        
            hpSlider.value = (float)EnemyNowHP / (float)enemyHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            sliderBool = true;
        }
        
        
    }

}
