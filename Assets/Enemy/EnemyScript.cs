using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;
    public GameObject Enemybullet;
    private Animator animator;
    public int enemyHP;// 敵の最大HP
    private int EnemyNowHP;  // 敵の現在のHP
    public Slider hpSlider; //HPバー（スライダー）
    public ParticleSystem particle;
    public bool sliderBool;
    private float[] bulletTimer = new float[3];
    private float MoveSpeed = 2.0f;
    private float BackmoveSpeed = -27.0f;

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
        hpSlider.value = enemyHP;//HPバーの最初の値（最大HP）を設定
        EnemyNowHP = enemyHP; // 現在のHPを最大HPに設定
        hpSlider.gameObject.SetActive(false);
        sliderBool= false;
        Destroy(gameObject, 10);
        if (gameManagerScript.IsGameOver() == true)
        {
            return;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        for (int i = 0; i < 3; i++)
        {
            bulletTimer[i] = 0.0f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // 時間依存の移動
        float move = MoveSpeed * Time.deltaTime;
        float backMove = BackmoveSpeed * Time.deltaTime;

        if (gameManagerScript.IsGameStart() == true)
        {
            Vector3 velocity = new Vector3(0, 0, backMove);
            transform.position += transform.rotation * velocity;
        }
        // スライダーの向きをカメラ方向に固定
        hpSlider.transform.rotation = Camera.main.transform.rotation;

        //移動
        Vector3 position = transform.position;

        transform.position += new Vector3(0, 0, move);

        //スライダー表示
        if(sliderBool == true)
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

            //敵消える
            Destroy(gameObject, 0f);

            //スコア上昇
            gameManagerScript.BatteryEnargyUp();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        
        //敵と弾
        if (other.gameObject.tag == "Bullet")
        {
            //audioSource.PlayOneShot(DamegeSound);
            EnemyNowHP -= 200;//一度当たるごとに200をマイナス
            hpSlider.value = (float)EnemyNowHP / (float)enemyHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            //Slider表示
            sliderBool = true;
        }
        //敵とマシンガン
        if (other.gameObject.tag == "Machinegun")
        {
            //audioSource.PlayOneShot(DamegeSound);
            EnemyNowHP -= 150;//一度当たるごとに150をマイナス
            hpSlider.value = (float)EnemyNowHP / (float)enemyHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            sliderBool = true;
        }
        //敵と貫通弾
        if (other.gameObject.tag == "PenetrationBullet")
        {
            //audioSource.PlayOneShot(DamegeSound);
            EnemyNowHP -= 400;//一度当たるごとに100をマイナス
            hpSlider.value = (float)EnemyNowHP / (float)enemyHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            sliderBool = true;
        }

    }
    void OnTriggerStay(Collider other)
    {
        //ボスとレーザー
        if (other.gameObject.tag == "PlayerLazer" || other.gameObject.tag == "PlayerLazer_L" || other.gameObject.tag == "PlayerLazer_R")
        {
            //audioSource.PlayOneShot(DamegeSound);
            EnemyNowHP -= 30;//一度当たるごとに25をマイナス
            hpSlider.value = (float)EnemyNowHP / (float)enemyHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            sliderBool = true;
        }
    }

    void FixedUpdate()
    {
        if (gameManagerScript.IsGameOver() == false)
        {
            if (bulletTimer[0] == 0.0f)
            {
                Vector3 position = transform.position;
                position.y += 0.3f;
                position.z -= 3.0f;
                Instantiate(Enemybullet, position, Quaternion.identity);
                bulletTimer[0] = 1.0f;
            }
            else
            {
                bulletTimer[0]+= Time.deltaTime;
                if (bulletTimer[0] > 2.0f)
                {
                    bulletTimer[0] = 0.0f;
                }
            }
        }
        
    }

    public float Speed()
    {
        return MoveSpeed * Time.deltaTime;
    }
    public float BackSpeed()
    {
        return BackmoveSpeed * Time.deltaTime;
    }

}
