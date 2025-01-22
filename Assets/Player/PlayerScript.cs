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
    public GameObject HPSlider;
    private Animator animator;
    public int playerHP;// プレイヤーの最大HP
    private int MaxHp;// プレイヤーの現在のHP
    public Slider hpSlider;//HPバー（スライダー）
    private int ShotChenge = 0;//射撃パターン追加
    public bool isDameged = false;
    public ParticleSystem particle;
    public AudioClip DamegeSound;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        hpSlider.value = (float)playerHP;//HPバーの最初の値（最大HP）を設定
        MaxHp = playerHP; // 現在のHPを最大HPに設定
        for (int i = 0; i < 3; i++)
        {
            bulletTimer[i] = 0.0f;
        }
        isDameged = false;
        
        HPSlider.SetActive(true);
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


        if (gameManagerScript.IsGameOver() == true)
        {
            animator.SetBool("GameOver", true);
            HPSlider.SetActive(false);
            return;
        }
        else
        {
            animator.SetBool("GameOver", false);
        }

        float stick = Input.GetAxis("Horizontal");
        float Vstick = Input.GetAxis("Vertical");
        ///ゲームスタートしたら
        if (gameManagerScript.IsGameStart() == true&&gameManagerScript.IsGameClear()==false)
        {
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

            //キーボード
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

        }
        //装甲追加  
        if (gameManagerScript.IsScore()>= 10)
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
       
        if (other.gameObject.tag == "EnemyBullet")
        {
            MaxHp -= 3;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
        }

        //BossBullet
        if (other.gameObject.tag == "BossBullet")
        {
            MaxHp -= 8;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
        }
        //BossExtraBullet
        if (other.gameObject.tag == "BossExtraBullet")
        {
            MaxHp -= 10;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
        }

        //ダメージ
        if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Enemy"|| other.gameObject.tag == "BossBullet"||other.gameObject.tag == "BossExtraBullet")
        {
            Damaged();
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

}
