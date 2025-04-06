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

    //models
    private PlayerModels playerModelsScript;
    public GameObject playerModels;

    //カメラ
    private CameraMove cameraMoveScript;
    public GameObject cameraMove;

    //オブジェクト挿入
    public GameObject MachingunFire;
    public GameObject LazerFire;
    public GameObject PanetrationFire;
    public GameObject HealImage;
    public GameObject NoHealImage;
    // 攻撃手段///
    public GameObject bullet;
    public GameObject machineGun;
    public GameObject Lazer;
    public GameObject Lazer_R;
    public GameObject Lazer_L;
    public GameObject PenetrationBullet;
    public GameObject DamegeRing;


    public bool position_R;

    //ステータス
    float[] bulletTimer = new float[3];
    private bool singleShotChenge = false;
    private bool lazerShotChenge = false;
    private bool penetrationShotChenge = false;
    private Animator animator;
    private float MoveSpeed = 18.0f;
    private float BoostMoveSpeed = 80.0f;

    //HP関連
    public GameObject HPSlider;
    public int playerHP;// プレイヤーの最大HP
    private int MaxHp;// プレイヤーの現在のHP
    public Slider hpSlider;//HPバー（スライダー）
    public bool isDameged = false;
    private bool isHeal=false;
    public float DamegeCoolTime;

    //パーティクル
    public ParticleSystem particle;

    //オーディオ
    public AudioClip DamegeSound;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //selector
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
        //playerModels
        playerModelsScript = playerModels.GetComponent<PlayerModels>();
        //animation
        animator = GetComponent<Animator>();
        //camera
        cameraMoveScript = cameraMove.GetComponent<CameraMove>();

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
        DamegeCoolTime = 0.0f;
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
        // 時間依存の移動
        float move = MoveSpeed * Time.deltaTime;
        float boostmove = BoostMoveSpeed * Time.deltaTime;

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

        if (gameManagerScript.IsGameStart() == true)
        {
            HPSlider.SetActive(true);

        }

        //L Stick
        float stick = Input.GetAxis("Horizontal");
        float Vstick = Input.GetAxis("Vertical");

        float LT = Input.GetAxis("LeftTrigger");
        float RT = Input.GetAxis("RightTrigger");

        ///ゲームスタートしたら
        if (gameManagerScript.IsGameStart() == true&&gameManagerScript.IsGameClear()==false)
        {
            DamegeCoolTime += Time.deltaTime;
            //回復UI
            if (gameManagerScript.GetHealBatteryEnargy() < 9)
            {
                isHeal = false;
            }   
            if(gameManagerScript.GetHealBatteryEnargy() < 9&&isHeal ==false)
            {
                HealImage.SetActive(false);
                NoHealImage.SetActive(true);
            }

            ///回復
            if (gameManagerScript.GetHealBatteryEnargy() >= 9 && isHeal == false && MaxHp < 400)
            {
                HealImage.SetActive(true);
                NoHealImage.SetActive(false);
                if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown("joystick button 2"))
                {
                    if (playerHP <= MaxHp)
                    {
                        MaxHp = 150; // HPが最大値を超えないように固定
                    }
                    else
                    {
                        MaxHp += 100; // 通常の増加処理
                        if (MaxHp > 150)
                        {
                            MaxHp = 150; // 150を超えた場合は150にリセット
                        }
                    }

                    hpSlider.value = (float)MaxHp / (float)playerHP;
                    isHeal = true;
                    HealImage.SetActive(false);
                    NoHealImage.SetActive(true);
                    //バッテリー関連
                    gameManagerScript.HealBatteryEnargyReset();
                    gameManagerScript.HealCounter();

                }
            }
            
            ///コントローラー対応///////////////////////
            if (stick > 0 && transform.position.x <= 10)
            {
                transform.position += new Vector3(move, 0, 0);
            }
            else if (stick < 0 && transform.position.x >= -10)
            {
                transform.position += new Vector3(-move, 0, 0);
            }
            if (Vstick > 0 && transform.position.z <= 15)
            {
                transform.position += new Vector3(0, 0, move);
            }
            else if (Vstick < 0 && transform.position.z >= -8.5f)
            {
                transform.position += new Vector3(0, 0, -move);
            }

            //緊急回避
            if (RT > 0 && transform.position.x <= 10)
            {
                transform.position += new Vector3(boostmove, 0, 0);
            }
            else if (LT > 0 && transform.position.x >= -10)
            {
                transform.position += new Vector3(-boostmove, 0, 0);
            }

            //火
            if (Vstick > 0 || Input.GetKey(KeyCode.W))
            {
                MachingunFire.SetActive(true);
                LazerFire.SetActive(true);
                PanetrationFire.SetActive(true);
            }
            else
            {
                MachingunFire.SetActive(false);
                LazerFire.SetActive(false);
                PanetrationFire.SetActive(false);
            }

            //キーボード/////////////////////////////////
            if (Input.GetKey(KeyCode.D)&& transform.position.x <= 8)
            {
                transform.position += new Vector3(move, 0, 0);
            }
            else if (Input.GetKey(KeyCode.A) && transform.position.x >= -8)
            {
                transform.position += new Vector3(-move, 0, 0);
            }
            if (Input.GetKey(KeyCode.W) && transform.position.z <= 15)
            {
                transform.position += new Vector3(0, 0, move);
            }
            else if (Input.GetKey(KeyCode.S) && transform.position.z >= -8.5f)
            {
                transform.position += new Vector3(0, 0, -move);
            }

            //緊急回避
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Space)&& transform.position.x <= 8)
            {
                transform.position += new Vector3(boostmove, 0, 0);
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.Space) && transform.position.x >= -8)
            {
                transform.position += new Vector3(-boostmove, 0, 0);
            }
            //////////////////////////////////////////////

            if (transform.position.x > 0)
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
        // 射撃パターン追加
        if (gameManagerScript.GetBatteryEnargy() >= 30)
        {
            penetrationShotChenge = true; // 自機タイプ貫通弾
        }
        else if (gameManagerScript.GetBatteryEnargy() >= 25)
        {
            singleShotChenge = true; // 自機タイプ単発
        }
        else if (gameManagerScript.GetBatteryEnargy() >= 20)
        {
            lazerShotChenge = true; // 自機タイプレーザー
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
            //自機タイプ単発時
            if (playerModelsScript.IsIndex() == 0)
            {
                //遅い弾
                if (bulletTimer[0] == 0.0f)
                {
                    Vector3 position = transform.position;
                    position.y += 0.3f;
                    position.z += 1.6f;
                    Instantiate(bullet, position, Quaternion.identity);
                    bulletTimer[0] = 1.0f;
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
                    if (singleShotChenge)
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

            //自機タイプレーザー時
            if (playerModelsScript.IsIndex() == 1)
            {
                Lazer.SetActive(true);
                if (lazerShotChenge)
                {
                    Lazer_R.SetActive(true);
                    Lazer_L.SetActive(true);
                }
            }
            else
            {
                Lazer.SetActive(false);
                Lazer_R.SetActive(false);
                Lazer_L.SetActive(false);
            }

            //自機タイプ貫通時
            if (playerModelsScript.IsIndex() == 2)
            {
                //貫通弾
                if (bulletTimer[0] == 0.0f)
                {
                    Vector3 position = transform.position;
                    position.y += 0.3f;
                    position.z += 1.6f;
                    Instantiate(PenetrationBullet, position, Quaternion.identity);
                    bulletTimer[0] = 1.0f;
                }
                else
                {
                    bulletTimer[0]++;
                    if (bulletTimer[0] > 30.0f)
                    {
                        bulletTimer[0] = 0.0f;
                    }
                }
                //DamegeRing
                if (penetrationShotChenge)
                {
                    DamegeRing.SetActive(true);
                    bulletTimer[0]++;
                    if (bulletTimer[0] > 10.0f)
                    {
                        bulletTimer[0] = 0.0f;
                    }
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
        if (DamegeCoolTime >= 1f)
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

            //ロボットの弾
            if (other.gameObject.tag == "RobotBullet")
            {
                MaxHp -= 20;
                hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            }


            if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "BossBullet" || other.gameObject.tag == "BossExtraBullet" || other.gameObject.tag == "Lazer"
                || other.gameObject.tag == "RobotBullet"|| other.gameObject.tag == "FinalLazer"&&cameraMoveScript.IsAnimation()==false)
            {
                Damaged();
            }
            else
            {
                isDameged = false;

            }
        }
            

    }
    void OnTriggerStay(Collider other)
    {
        //ボスのレーザー
        if (other.gameObject.tag == "Lazer")
        {
            MaxHp -= 3;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
        }
        //ボスのレーザー
        if (other.gameObject.tag == "FinalLazer")
        {
            MaxHp -= 4;
            hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (DamegeCoolTime >= 1f)
        {
            if (other.gameObject.tag == "Enemy")
            {
                MaxHp -= 5;
                hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
                Damaged();

            }
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
        return MoveSpeed * Time.deltaTime;
    }
    public float DamegeCoolTimer()
    {
        return DamegeCoolTime;
    }
    public bool IsHeal()
    {
        return isHeal;
    }
    public void DownHp()
    {
        MaxHp -= 20;
        hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
    }
    public void UpHp()
    {
        MaxHp += 20;
        hpSlider.value = (float)MaxHp / (float)playerHP;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
    }
}
