using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerScript : MonoBehaviour
{
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    //select
    private SelectorMenu selectorMenuScript;
    public GameObject selectMenu;

    //オブジェクト挿入
    public GameObject bullet;
    public GameObject machineGun;
    public EnemyScript enemy;
    public GameObject Fire;
    public GameObject HealImage;
    public GameObject NoHealImage;

    public bool position_R;

    //ステータス
    float[] bulletTimer = new float[3];
    private int ShotChenge = 0;//射撃パターン追加
    private Animator animator;
    //private float MoveSpeed = 0.06f;

    private float MoveSpeed = 0.15f;

    //HP関連
    public GameObject HPSlider;
    public int playerHP;// プレイヤーの最大HP
    private int MaxHp;// プレイヤーの現在のHP
    public Slider hpSlider;//HPバー（スライダー）
    public bool isDameged = false;
    private bool isHeal=false;

    //パーティクル
    public ParticleSystem particle;

    //オーディオ
    public AudioClip DamegeSound;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //selector
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
        animator = GetComponent<Animator>();

        //Hp関連
        audioSource = GetComponent<AudioSource>();
        hpSlider.value = (float)playerHP;//HPバーの最初の値（最大HP）を設定
        MaxHp = playerHP; // 現在のHPを最大HPに設定
        for (int i = 0; i < 3; i++)
        {
            bulletTimer[i] = 0.0f;
        }
        isDameged = false;
        HPSlider.SetActive(true);
        //回復
        isHeal = false;
        
    }

    void Damaged()
    {
        isDameged = true;
        audioSource.PlayOneShot(DamegeSound);
        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(particle);
        // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
        newParticle.transform.position = this.transform.position;
        // パーティクルを発生させる。
        newParticle.Play();
        // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
        // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
        Destroy(newParticle.gameObject, 5.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectorMenuScript.IsColorMenuFlag() == true)
        {
            HealImage.SetActive(false);
            NoHealImage.SetActive(false);
        }
        if (gameManagerScript.IsGameOver() == true)
        {
            animator.SetBool("GameOver", true);
            HPSlider.SetActive(false);
            HealImage.SetActive(false);
            NoHealImage.SetActive(false);
            return;
        }
        else
        {
            animator.SetBool("GameOver", false);
        }

        if(gameManagerScript.IsGameStart() == true)
        {
            HPSlider.SetActive(true);
        }

        //L Stick
        float stick = Input.GetAxis("Horizontal");
        float Vstick = Input.GetAxis("Vertical");

        ///ゲームスタートしたら
        if (gameManagerScript.IsGameStart() == true&&gameManagerScript.IsGameClear()==false)
        {
            //回復UI
            if (gameManagerScript.IsScore() < 15)
            {
                isHeal = false;
            }
            if(gameManagerScript.IsScore() < 15&&isHeal ==false)
            {
                HealImage.SetActive(false);
                NoHealImage.SetActive(true);
            }
            
            ///回復
            if (gameManagerScript.IsScore() >= 15 && isHeal == false)
            {
                HealImage.SetActive(true);
                NoHealImage.SetActive(false);
                if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown("joystick button 2"))
                {
                    MaxHp = 200;
                    hpSlider.value = (float)MaxHp / (float)playerHP;
                    isHeal = true;
                    HealImage.SetActive(false);
                    NoHealImage.SetActive(true);
                }
            }

            ///コントローラー対応///////////////////////
            if (stick > 0 && transform.position.x <= 10)
            {
                transform.position += new Vector3(MoveSpeed, 0, 0);
            }
            else if (stick < 0 && transform.position.x >= -10)
            {
                transform.position += new Vector3(-MoveSpeed, 0, 0);
            }
            if (Vstick > 0 && transform.position.z <= 20)
            {
                transform.position += new Vector3(0, 0, MoveSpeed);
            }
            else if (Vstick < 0 && transform.position.z >= -6.5f)
            {
                transform.position += new Vector3(0, 0, -MoveSpeed);
            }

            if (Vstick > 0 || Input.GetKey(KeyCode.W))
            {
                Fire.SetActive(true);
            }
            else
            {
                Fire.SetActive(false);
            }

            //キーボード/////////////////////////////////
            if (Input.GetKey(KeyCode.D)&& transform.position.x <= 10)
            {
                transform.position += new Vector3(MoveSpeed, 0, 0);
            }
            else if (Input.GetKey(KeyCode.A) && transform.position.x >= -10)
            {
                transform.position += new Vector3(-MoveSpeed, 0, 0);
            }
            if (Input.GetKey(KeyCode.W) && transform.position.z <= 20)
            {
                transform.position += new Vector3(0, 0, MoveSpeed);
            }
            else if (Input.GetKey(KeyCode.S) && transform.position.z >= -6.5f)
            {
                transform.position += new Vector3(0, 0, -MoveSpeed);
            }
            //////////////////////////////////////////////

            if(transform.position.x > 0)
            {
                position_R = true;
            }else
            {
                position_R = false;
            }

            

        }
        else
        {
            HealImage.SetActive(false);
            NoHealImage.SetActive(false);
        }
        //射撃パターン追加  
        if (gameManagerScript.IsScore()>= 15)
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
            //遅い弾
            if (bulletTimer[0] == 0.0f)
            {
                if(ShotChenge >= 0)
                {
                    Vector3 position = transform.position;
                    position.y += 0.3f;
                    position.z += 1.6f;
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
                //マシンガン
                if (ShotChenge >= 1)
                {
                    //弾発射
                    Vector3 positionR = transform.position;
                    Vector3 positionL = transform.position;
                    positionR.y += 0.3f;
                    positionR.x += 2.0f;
                    positionL.y += 0.3f;
                    positionL.x -= 2.0f;
                    Instantiate(machineGun, positionR, Quaternion.identity);
                    Instantiate(machineGun, positionL, Quaternion.identity);
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
        
        if(gameManagerScript.IsGameClear()==true)
        {
            Destroy(gameObject);
        }
        if (MaxHp <= 0)
        {
           gameManagerScript.GameOverStart();
           Destroy(gameObject, 1);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //雑魚の弾
        if (other.gameObject.tag == "EnemyBullet")
        {
            MaxHp -= 10;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            
        }

        //BossBullet
        if (other.gameObject.tag == "BossBullet")
        {
            MaxHp -= 20;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
        }
        
        //BossExtraBullet
        if (other.gameObject.tag == "BossExtraBullet")
        {
            MaxHp -= 20;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
        }

        //ボスのレーザー
        if (other.gameObject.tag == "Lazer")
        {
            MaxHp -= 15;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
        }

        //ダメージ
        if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Enemy"|| other.gameObject.tag == "BossBullet"||other.gameObject.tag == "BossExtraBullet"|| other.gameObject.tag == "Lazer")
        {
            Damaged();
            
        }
        else
        {
            isDameged = false;
        }

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            MaxHp -= 5;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            Damaged();
            
        }
        
    }

    public bool IsDamege()
    {
        return isDameged;
    }

    public bool IsPotion()
    {
        return position_R;
    }
    public float Speed()
    {
        return MoveSpeed;
    }
}
