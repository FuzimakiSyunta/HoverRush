﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BossScript : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;

    public GameObject Robot;
    public GameObject BossAir;
    //Bossの弾
    public GameObject Bossbullet;
    public GameObject BossBarstbullet_L;
    public GameObject BossBarstbullet_R;
    public GameObject FinalBossBarstbullet_L;
    public GameObject FinalBossBarstbullet_R;
    public GameObject Lazer_L;
    public GameObject Lazer_R;
    public GameObject BIGLAZER;
    public bool isfadeLazer;
    public bool isLazerWave;
    public float LazerTime;

    //弾のステータス
    private float MultibulletTimer = 0;
    private float bulletTimer = 0;
    private float FinalbulletTimer = 0;
    public float BossBattleTime = 0;
    private float MultiBulletCoolTime = 0;
    private float BulletCoolTime = 0;
    private float LazerBulletCoolTime = 0;

    //Bossのステータス
    public int bossHP;// ボスの最大HP
    private int wkHP;  // ボスの現在のHP
    public UnityEngine.UI.Slider hpSlider; //HPバー（スライダー）
    public ParticleSystem particle;
    public bool sliderBool;
    private Animator animator;
    private bool StartTime=false;

    //オーディオ
    public AudioClip DamegeSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        hpSlider.value = (float)bossHP;//HPバーの最初の値（最大HP）を設定
        wkHP = bossHP; // 現在のHPを最大HPに設定
        hpSlider.gameObject.SetActive(false);
        sliderBool = false;
        animator.SetBool("isMove", false);
        animator.SetBool("isLazer", false);
        animator.SetBool("isAirTransform", false);
        animator.SetBool("isRobotStay", false);
        animator.SetBool("isTransform", false);
        animator.SetBool("FinalWave", false);
        animator.SetBool("isFinalBullet", false);
        bulletTimer = 0;
        MultibulletTimer = 0;
        StartTime = false;
        BossBattleTime = 0;
        isLazerWave = false;
        Lazer_L.SetActive(false);
        Lazer_R.SetActive(false);
        BIGLAZER.SetActive(false);
        Robot.SetActive(false);
        BossAir.SetActive(true);
        BIGLAZER.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // スライダーの向きをカメラ方向に固定
        hpSlider.transform.rotation = Camera.main.transform.rotation;
        //スライダー表示
        if (sliderBool == true)
        {
            hpSlider.gameObject.SetActive(true);
        }
        
        if (gameManagerScript.IsGameStart() == true)
        {
            StartTime=true;
            
            if (StartTime == true)
            {
                BossBattleTime += Time.deltaTime;
            }else
            {
                animator.SetBool("isMove", false);
                animator.SetBool("isLazer", false);
                animator.SetBool("FinalWave", false);
                animator.SetBool("isFinalBullet", false);
            }
            
            if (BossBattleTime > 20)
            {
                BossWaveUpdate();
            }
           
        }
        // HPが0以下になった場合、自らを消す
        if (wkHP <= 0)
        {
            ParticleSystem newParticle = Instantiate(particle);
            //場所固定
            newParticle.transform.position = this.gameObject.transform.position;
            //発生
            newParticle.Play();
            //エフェクト消える
            Destroy(newParticle.gameObject, 0.5f);

            //ボス消える
            gameManagerScript.GameClearStart();//ゲームクリア
            Destroy(gameObject, 0f);

        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        //ボスと弾
        if (other.gameObject.tag == "Bullet")
        {
            audioSource.PlayOneShot(DamegeSound);
            wkHP -= 30;//一度当たるごとに30をマイナス
            hpSlider.value = (float)wkHP / (float)bossHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            //Slider表示
            sliderBool = true;
        }
        //ボスとマシンガン
        if (other.gameObject.tag == "Machinegun")
        {
            audioSource.PlayOneShot(DamegeSound);
            wkHP -= 10;//一度当たるごとに10をマイナス
            hpSlider.value = (float)wkHP / (float)bossHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            sliderBool = true;
        }
        
    }
    void FixedUpdate()
    {
        //散弾
        if (MultibulletTimer == 0.0f)
        {
            Vector3 position = transform.position;
            MultiBulletCoolTime++;
            if (animator.GetBool("isMove")==false && animator.GetBool("isLazer") == false && animator.GetBool("isRobotStay") == false && animator.GetBool("FinalWave") == false)
            {
                if(MultiBulletCoolTime>= 240)
                {
                    position.y += 0.3f;
                    position.z -= 30.0f;
                    Instantiate(Bossbullet, position, Quaternion.identity);
                    MultibulletTimer = 1.0f;

                }
                
            }
           
        }
        else
        {
            MultibulletTimer++;
            if (MultibulletTimer > 120.0f)
            {
                MultibulletTimer = 0.0f;
            }
            BulletCoolTime = 0;
        }

        //爆弾ウェーブ
        if (bulletTimer == 0.0f)
        {
            Vector3 positionR = transform.position;
            Vector3 positionL = transform.position;
            BulletCoolTime++;
            if (animator.GetBool("isMove") == true&& animator.GetBool("isFinalBullet") == false)
            {
                if (BulletCoolTime >= 60)
                {
                    Instantiate(BossBarstbullet_R, positionR, Quaternion.identity);
                    Instantiate(BossBarstbullet_L, positionL, Quaternion.identity);
                    bulletTimer = 1.0f;
                }
            }

        }
        else
        {
            bulletTimer++;
            if (bulletTimer > 45.0f)
            {
                bulletTimer = 0.0f;
            }
            MultiBulletCoolTime = 0;
        }


        //レーザーウェーブ
        if (animator.GetBool("isLazer") == true)
        {
            LazerTime += Time.deltaTime;
            LazerBulletCoolTime++;
            if(LazerBulletCoolTime>=60)
            {
                if (LazerTime <= 1.0f)
                {
                    Lazer_L.SetActive(true);
                    Lazer_R.SetActive(false);
                }
                if (LazerTime <= 2.5f && LazerTime > 1.5f)
                {
                    Lazer_L.SetActive(false);
                    Lazer_R.SetActive(true);
                }
                if (LazerTime <= 4.0f && LazerTime > 3.0f)
                {
                    LazerTime = 0.0f;
                }
            }

        }
        else
        {
            Lazer_L.SetActive(false);
            Lazer_R.SetActive(false);
            LazerBulletCoolTime = 0;
        }

        //最終レーザー
        if (animator.GetBool("FinalWave") == true)
        {
            BIGLAZER.SetActive(true);
        }
        else
        {
            BIGLAZER.SetActive(false);
        }

        //最終爆弾ウェーブ
        if (FinalbulletTimer == 0.0f)
        {
            Vector3 positionR = transform.position;
            Vector3 positionL = transform.position;
            BulletCoolTime++;
            if (animator.GetBool("isMove") == true && animator.GetBool("isFinalBullet") == true)
            {
                if (BulletCoolTime >= 60)
                {
                    Instantiate(FinalBossBarstbullet_R, positionR, Quaternion.identity);
                    Instantiate(FinalBossBarstbullet_L, positionL, Quaternion.identity);
                    FinalbulletTimer = 1.0f;
                }
            }

        }
        else
        {
            FinalbulletTimer++;
            if (FinalbulletTimer > 30.0f)
            {
                FinalbulletTimer = 0.0f;
            }
            MultiBulletCoolTime = 0;
        }


        //ロボット状態の切り替え
        if (animator.GetBool("isRobotStay")==true)
        {
            BossAir.SetActive(false);
            Robot.SetActive(true);
        }
        if (animator.GetBool("FinalWave") == true)
        {
            BossAir.SetActive(true);
            Robot.SetActive(false);
        }
        

    }

    void BossWaveUpdate()//一対一のアニメーション
    {
        if (BossBattleTime > 20 && BossBattleTime <= 40)
        {
            animator.SetBool("isMove", true);
        }
        if (BossBattleTime >= 40 && BossBattleTime < 60)
        {
            animator.SetBool("isMove", false);
        }
        if (BossBattleTime >= 60 && BossBattleTime < 80)
        {
            animator.SetBool("isLazer", true);
            isLazerWave = true;
        }
        if (BossBattleTime >= 80 && BossBattleTime < 100)
        {
            animator.SetBool("isLazer", false);
            animator.SetBool("isTransform",true);
            isLazerWave = false;
        }
        if (BossBattleTime >= 85&& BossBattleTime < 100)
        {
            animator.SetBool("isRobotStay", true);
        }
        if (BossBattleTime >= 100&& BossBattleTime < 103)
        {
            animator.SetBool("isAirTransform", true);
            animator.SetBool("isRobotStay", false);
        }
        if (BossBattleTime >= 101)
        {
            animator.SetBool("FinalWave", true);
        }
        if (BossBattleTime >= 131)
        {
            animator.SetBool("FinalWave", false);
            animator.SetBool("isMove", true);
            animator.SetBool("isFinalBullet", true);
        }
    }

    public bool IsFadeStart()
    {
        return isfadeLazer;
    }

}
