using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerScript : MonoBehaviour
{
    private float MoveSpeed = 0.02f;
    public GameObject bullet;
    public GameObject Lazer;
    float bulletTimer = 0.0f;
    private GameManager gameManagerScript;
    public GameObject gameManager;
    private Animator animator;
    public int playerHP;// 敵の最大HP
    private int MaxHp;  // 敵の現在のHP
    public Slider hpSlider;     //HPバー（スライダー）
    private int ShotChenge;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        hpSlider.value = (float)playerHP;//HPバーの最初の値（最大HP）を設定
        MaxHp = playerHP; // 現在のHPを最大HPに設定
        ShotChenge = 0;
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
        

        //弾切り替え
        if (Input.GetKey(KeyCode.RightArrow))
        {
            ShotChenge = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ShotChenge = 2;
        }
        //if(Input.GetKey(KeyCode.Alpha3))
        //{

        //}
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
            if (bulletTimer == 0.0f)
            {
                if(ShotChenge==1)
                {
                   Vector3 position = transform.position;
                   position.y += 0.3f;
                   
                   Instantiate(bullet, position, Quaternion.identity);
                   bulletTimer = 1.0f;
                   
                }
                
            }
            else
            {
                bulletTimer++;
                if (bulletTimer > 5.0f)
                {
                    bulletTimer = 0.0f;
                }
            }

           //レーザー
           if (ShotChenge == 2)
           {
               //弾発射
               Vector3 position = transform.position;
               position.y += 0.3f;
               position.z += 6.0f;
               Instantiate(Lazer, position, Quaternion.identity);
           } 
        }
        if (MaxHp <= 0)
        {
           gameManagerScript.GameOverStart();
           Destroy(gameObject, 1);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            MaxHp -= 15;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
        }
    }
}
