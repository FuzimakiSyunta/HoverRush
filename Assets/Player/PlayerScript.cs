using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerScript : MonoBehaviour
{
    private float MoveSpeed = 0.08f;
    public GameObject bullet;
    public GameObject Lazer;
    public EnemyScript enemy;
    float[] bulletTimer = new float[3];
    private GameManager gameManagerScript;
    public GameObject gameManager;
    private Animator animator;
    public int playerHP;// 敵の最大HP
    private int MaxHp;// 敵の現在のHP
    public Slider hpSlider;//HPバー（スライダー）
    private int ShotChenge = 0;//射撃パターン追加
    public Image DamageImg;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        hpSlider.value = (float)playerHP;//HPバーの最初の値（最大HP）を設定
        MaxHp = playerHP; // 現在のHPを最大HPに設定
        for (int i = 0; i < 3; i++)
        {
            bulletTimer[i] = 0.0f;
        }
        DamageImg.color = Color.clear;
    }

    void Damaged()
    {
        DamageImg.color = new Color(0.7f, 0, 0, 0.7f);
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameOver() == true)
        {
            animator.SetBool("GameOver", true);
            return;
        }
        else
        {
            animator.SetBool("GameOver", false);
        }

        ///ゲームスタートしたら
        if (gameManagerScript.IsGameStart() == true)
        {
            if (Input.GetKey(KeyCode.D) && transform.position.x <= 10)
            {
                transform.position += new Vector3(MoveSpeed, 0, 0);
            }
            else if (Input.GetKey(KeyCode.A) && transform.position.x >= -10)
            {
                transform.position += new Vector3(-MoveSpeed, 0, 0);
            }
            
        }
        //装甲追加  
        if (gameManagerScript.IsScore()>= 5)
        {
            ShotChenge = 1;
        }
    }
    void FixedUpdate()
    {
        if (gameManagerScript.IsGameOver() == true)
        {
            return;
        }
        ///ゲームスタートしたら
        if (gameManagerScript.IsGameStart() == true)
        {
            //マシンガン
            if (bulletTimer[0] == 0.0f)
            {
                if(ShotChenge >= 0)
                {
                   Vector3 position = transform.position;
                   position.y += 0.3f;
                   position.z += 0.6f;
                   Instantiate(bullet, position, Quaternion.identity);
                   bulletTimer[0] = 1.0f;
                   
                }
            }
            else
            {
                bulletTimer[0]++;
                if (bulletTimer[0] > 15.0f)
                {
                    bulletTimer[0] = 0.0f;
                }
            }
            if (bulletTimer[1] == 0.0f)
            {
                //サブガトリング
                if (ShotChenge >= 1)
                {
                    //弾発射
                    Vector3 positionR = transform.position;
                    Vector3 positionL = transform.position;
                    positionR.y += 0.3f;
                    positionR.x += 2.0f;
                    positionL.y += 0.3f;
                    positionL.x -= 2.0f;
                    Instantiate(Lazer, positionR, Quaternion.identity);
                    Instantiate(Lazer, positionL, Quaternion.identity);
                    bulletTimer[1] = 1.0f;
                }
            }
            else
            {
                bulletTimer[1]++;
                if (bulletTimer[1] > 5.0f)
                {
                    bulletTimer[1] = 0.0f;
                }
            }
        }
        
        if (MaxHp <= 0)
        {
           gameManagerScript.GameOverStart();
           Destroy(gameObject, 1);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            MaxHp -= 15;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            MaxHp -= 5;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
        }
        //ダメージカラー
        if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Enemy")
        {
            Damaged();
        }

    }
    
}
