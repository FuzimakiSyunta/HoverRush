using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BossScript : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;
    public GameObject Bossbullet;
    public int bossHP;// ボスの最大HP
    private int wkHP;  // ボスの現在のHP
    public UnityEngine.UI.Slider hpSlider; //HPバー（スライダー）
    public ParticleSystem particle;
    public bool sliderBool;
    private float bulletTimer = 0;
    private Animator animator;
    private float BossBattleTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        hpSlider.value = (float)bossHP;//HPバーの最初の値（最大HP）を設定
        wkHP = bossHP; // 現在のHPを最大HPに設定
        hpSlider.gameObject.SetActive(false);
        sliderBool = false;
        if (gameManagerScript.IsGameOver() == true)
        {
            return;
        }
        bulletTimer = 0;
        BossBattleTime = 0;
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
            //スピード
            float moveSpeed = -0.2f;
            float waveStartmoveSpeed = -0.05f;
            //移動
            Vector3 position = transform.position;
            transform.position += new Vector3(0, 0, moveSpeed);
            //ループ
            if (transform.position.z <=-50)
            {
                transform.position = new Vector3(0, 9.7f, 110.6f);
            }

            BossBattleTime = Time.time;
            if (BossBattleTime >= 30)
            {
                if(transform.position.y>4.2f&& transform.position.z>30)
                {
                    transform.position += new Vector3(0, waveStartmoveSpeed, waveStartmoveSpeed);
                }
                BossWaveUpdate();
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //ボスと弾
        if (other.gameObject.tag == "Bullet")
        {
            wkHP -= 30;//一度当たるごとに50をマイナス
            hpSlider.value = (float)wkHP / (float)bossHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            //Slider表示
            sliderBool = true;
        }
        //ボスとレーザー
        if (other.gameObject.tag == "Lazer")
        {
            wkHP -= 10;//一度当たるごとに10をマイナス
            hpSlider.value = (float)wkHP / (float)bossHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            sliderBool = true;
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
            Destroy(gameObject, 0f);
            gameManagerScript.Score();
        }
    }
    void FixedUpdate()
    {
        if (bulletTimer == 0.0f)
        {
            Vector3 position = transform.position;
            position.y += 0.3f;
            position.z -= 3.0f;
            Instantiate(Bossbullet, position, Quaternion.identity);
            bulletTimer = 1.0f;
        }
        else
        {
            bulletTimer++;
            if (bulletTimer > 45.0f)
            {
                bulletTimer = 0.0f;
            }
        }
    }

    void BossWaveUpdate()
    {
        transform.position = new Vector3(0, 4.2f, 30);
    }
}
