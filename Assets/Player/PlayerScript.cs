﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using System;


public class PlayerScript : MonoBehaviour
{
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    //boss
    private BossScript bossScript;
    public GameObject boss;

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

    //ステータス
    float[] bulletTimer = new float[3];
    private bool singleShotChenge = false;
    private bool lazerShotChenge = false;
    private bool penetrationShotChenge = false;
    private float moveSpeed = 18.0f;
    private bool isLaserPoweredUp = false;
    private bool isSinglePoweredUp = false;
    private bool isPenetrationPoweredUp = false;

    //上昇
    private float verticalSpeed = 0f; // 上下方向の速度
    private float jumpPower = 50f;    // 上昇の勢い
    private float gravity = 9.8f;     // 重力（手を離したときのゆっくり下降）
    private float maxHeight = 5.6f;    // 上昇できる最大Y座標
    private float minHeight = 0.5f;     // 降下できる最小Y座標

    //HP関連
    public GameObject HPSlider;
    public int maxHp;// プレイヤーの最大HP
    private int currentHp;// プレイヤーの現在のHP
    public Slider hpSlider;//HPバー（スライダー）
    public bool isDamaged = false;
    private bool isHeal=false;
    public float DamegeCoolTime;

    //パーティクル
    public ParticleSystem DamageParticle;
    public ParticleSystem LazerParticle;
    public GameObject HoverParticle;
    public GameObject SmokeParticle;

    public float particleCooldown = 0.2f; // パーティクル再生間隔（秒）
    public float damageCooldown = 0.2f;
    private float particleTimer = 0f;
    private float damageTimer = 0f;

    //オーディオ
    public AudioClip DamegeSound;
    private AudioSource audioSource;

    //アニメーション
    private Animator animator;

    private bool isMoveActive = false;

    //ポジションリング
    public GameObject positionRing;

    //回復
    private const int MaxHealHp = 300;
    private const int HealAmount = 150;

    //シールド
    private bool isShieldActive = false; // シールドがアクティブかどうかのフラグ
    public GameObject Shield; // シールドのGameObject


    // プレイヤーがレーザーに触れているかどうかのフラグ
    private bool isTouchingLaser = false;
    private bool tookDamage = false; // ダメージ判定フラグ（演出用）

    // Start is called before the first frame update
    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //boss 
        bossScript = boss.GetComponent<BossScript>();
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
        hpSlider.value = (float)maxHp;//HPバーの最初の値（最大HP）を設定
        currentHp = maxHp; // 現在のHPを最大HPに設定
        for (int i = 0; i < 3; i++)
        {
            bulletTimer[i] = 0.0f;
        }
        isDamaged = false;
        HPSlider.SetActive(true);
        //回復
        isHeal = false;
        DamegeCoolTime = 0;
        //hover
        HoverParticle.SetActive(false);
        //ポジションリング
        positionRing.SetActive(false);
        //パワーアップ
        isLaserPoweredUp = false;
        isSinglePoweredUp = false;
        isPenetrationPoweredUp = false;
        //シールド
        isShieldActive = false; // シールドは初期状態では非アクティブ
        Shield.SetActive(false); // シールドのGameObjectを非表示にする
    }

    void Damaged()
    {
        isDamaged = true;
        DamegeCoolTime = 0.0f;
        gameManagerScript.BatteryEnargyDown(); // ダメージを受けたらバッテリーエネルギーを減少させる
        audioSource.PlayOneShot(DamegeSound);
        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(DamageParticle);
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
        //ダメージクールタイム
        DamegeCoolTimeActive();
        //プレイヤー関連UI
        ImageActive();


        ///ゲームスタートしたら
        if (gameManagerScript.IsGameStart() == true && gameManagerScript.IsGameClear() == false)
        {
            //プレイヤー移動
            PlayerMove();
            //プレイヤー回復
            PlayerHeal();
            //回避
            //Avoidance();
            // 射撃パターン
            ShotPattern();
            //ホバーモード
            HoverMode();
            //ポジションリング
            positionRing.SetActive(true);
            // シールドのアクティブ状態を更新
            ShieldActive();
            // タイマーを更新
            particleTimer += Time.deltaTime;
            damageTimer += Time.deltaTime;
        }
        else
        {
            HealImage.SetActive(false);
            NoHealImage.SetActive(false);
            positionRing.SetActive(false);
            SmokeParticle.SetActive(false);
        }
        // HPが100以下なら煙を出す、それ以上なら止める
        if (currentHp <= 100)
        {
            if (!SmokeParticle.activeSelf)
            {
                SmokeParticle.SetActive(true);
            }
        }
        else
        {
            if (SmokeParticle.activeSelf)
            {
                SmokeParticle.SetActive(false);
            }
        }

    }

    void ImageActive()
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

        if (gameManagerScript.IsGameStart() == true)
        {
            HPSlider.SetActive(true);

        }
    }

    void PlayerHeal()
    {
        //回復UI
        if (gameManagerScript.GetHealBatteryEnargy() < 9)
        {
            isHeal = false;
        }
        if (gameManagerScript.GetHealBatteryEnargy() < 9 && isHeal == false)
        {
            HealImage.SetActive(false);
            NoHealImage.SetActive(true);
        }

        ///回復
        if (gameManagerScript.GetHealBatteryEnargy() >= 9 && isHeal == false && currentHp < 500)
        {
            HealImage.SetActive(true);
            NoHealImage.SetActive(false);
            if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown("joystick button 2"))
            {
                if (maxHp <= currentHp)
                {
                    currentHp = MaxHealHp; // HPが最大値を超えないように固定
                }
                else
                {
                    currentHp += HealAmount; // 通常の増加処理
                    if (currentHp > MaxHealHp)
                    {
                        currentHp = MaxHealHp; // 300を超えた場合は300にリセット
                    }
                }

                hpSlider.value = (float)currentHp / (float)maxHp;
                isHeal = true;
                HealImage.SetActive(false);
                NoHealImage.SetActive(true);
                //バッテリー関連
                gameManagerScript.HealBatteryEnargyReset();
                gameManagerScript.HealCounter();

            }
        }
    }

    void PlayerMove()
    {
        DamegeCoolTime += Time.deltaTime;
        float move = moveSpeed * Time.deltaTime;

        // 入力取得（コントローラー + キーボードを合成）
        float stick = Input.GetAxis("Horizontal");
        float Vstick = Input.GetAxis("Vertical");
        float horizontalInput = stick + (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);
        float verticalInput = Vstick + (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0);

        // 移動ベクトル
        Vector3 moveDir = Vector3.zero;

        // X方向制限付き移動
        if (horizontalInput > 0 && transform.position.x <= 10)
            moveDir.x = move;
        else if (horizontalInput < 0 && transform.position.x >= -10)
            moveDir.x = -move;

        // Z方向制限付き移動
        if (verticalInput > 0 && transform.position.z <= 15)
            moveDir.z = move;
        else if (verticalInput < 0 && transform.position.z >= -8.5f)
            moveDir.z = -move;

        // 実際に移動
        transform.position += moveDir;

        // 移動フラグ
        isMoveActive = (horizontalInput != 0 || verticalInput != 0);

        // 火のON/OFF（前進中）
        if (verticalInput > 0)
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
    }


    void HoverMode()
    {
        

        float LT = Input.GetAxis("LeftTrigger");
        float RT = Input.GetAxis("RightTrigger");
        // 地面のY座標制限
        Vector3 pos = transform.position;

        // Spaceキーで上昇
        if (RT>0||Input.GetKey(KeyCode.Space) && pos.y < maxHeight)
        {
            verticalSpeed = jumpPower;
            
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime; // 重力による減速（ゆっくり下降）
            
        }

        // 実際にY軸に反映
        pos.y += verticalSpeed * Time.deltaTime;

        // Y座標制限
        if (pos.y < minHeight)
        {
            pos.y = minHeight;
            verticalSpeed = 0f;
            HoverParticle.SetActive(false);
        }
        else if (pos.y > maxHeight)
        {
            pos.y = maxHeight;
            verticalSpeed = 0f;
            HoverParticle.SetActive(true);
        }

        transform.position = pos;

    }

    void DamegeCoolTimeActive()
    {
        // クールダウンタイマーを進行
        if (DamegeCoolTime < 1.0f) // 1秒間のクールダウン
        {
            DamegeCoolTime += Time.deltaTime;
        }
    }

    

    void ShotPattern()
    {
        // 現在のエネルギーを取得しておく（無駄な呼び出しを減らす）
        int energy = gameManagerScript.GetBatteryEnargy();

        // レーザー（20以上）
        if (energy >= 20)
        {
            lazerShotChenge = true;

            if (!isLaserPoweredUp)
            {
                isLaserPoweredUp = true;
            }
        }
        else
        {
            lazerShotChenge = false;
        }

        // 単発（25以上）
        if (energy >= 25)
        {
            singleShotChenge = true;

            if (!isSinglePoweredUp)
            {
                isSinglePoweredUp = true;
            }
        }
        else
        {
            singleShotChenge = false;
        }

        // 貫通（30以上）
        if (energy >= 30)
        {
            penetrationShotChenge = true;

            if (!isPenetrationPoweredUp)
            {
                isPenetrationPoweredUp = true;
            }
        }
        else
        {
            penetrationShotChenge = false;
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
                if (penetrationShotChenge && playerModelsScript.IsIndex() == 2)
                {
                    DamegeRing.SetActive(true);
                    bulletTimer[0]++;
                    if (bulletTimer[0] > 10.0f)
                    {
                        bulletTimer[0] = 0.0f;
                    }
                }
                else
                {
                    DamegeRing.SetActive(false);
                }
            }
            
        }
            
        
        if(gameManagerScript.IsGameClear()==true)
        {
            Destroy(gameObject);
        }
        if (currentHp <= 0)
        {
           gameManagerScript.GameOverStart();
           Destroy(gameObject, 1);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        tookDamage = false; // ここで毎回初期化

        string tag = other.gameObject.tag;

        //雑魚の弾
        if (other.gameObject.tag == "EnemyBullet"&& !isShieldActive)
        {
            currentHp -= 10;
            tookDamage = true;
        }

        //BossBullet
        if (other.gameObject.tag == "BossBullet" && !isShieldActive)
        {
            currentHp -= 20;
          
            tookDamage = true;
        }

        //BossExtraBullet
        if (other.gameObject.tag == "BossExtraBullet" && !isShieldActive)
        {
            currentHp -= 8;
            tookDamage = true;
        }

        //ロボットの弾
        if (other.gameObject.tag == "RobotBullet" && !isShieldActive)
        {
            currentHp -= 20;
            tookDamage = true;
        }

        //必殺技
        if (other.gameObject.tag == "FinalBomm" && !isShieldActive)
        {
            currentHp -= 100;
            tookDamage = true;
            
        }

        // ダメージ受けたらスライダー更新
        if (tookDamage&&!isShieldActive)
        {
            hpSlider.value = (float)currentHp / (float)maxHp;
        }


        if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "BossBullet" || other.gameObject.tag == "BossExtraBullet" || other.gameObject.tag == "Lazer"
            || other.gameObject.tag == "RobotBullet" || other.gameObject.tag == "FinalLazer"|| other.gameObject.tag == "Piller" && cameraMoveScript.IsAnimation() == false||other.gameObject.tag == "FinalBomm")
        {
            Damaged();
        }
        else
        {
            isDamaged = false;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Lazer") || other.gameObject.CompareTag("FinalLazer"))
        {
            // レーザーに触れているフラグを立てる
            SetLaserHit(true);

            // ダメージ処理（0.1秒ごと）
            if (damageTimer >= damageCooldown)
            {
                if (other.gameObject.CompareTag("Lazer")&&!isShieldActive)
                {
                    currentHp -= 4;
                }
                else if (other.gameObject.CompareTag("FinalLazer")&&!isShieldActive)
                {
                    currentHp -= 6;
                }
                

                hpSlider.value = (float)currentHp / (float)maxHp;
                damageTimer = 0f; // タイマーリセット
            }

            //  パーティクル処理
            if (particleTimer >= particleCooldown)
            {
                ParticleSystem newParticle = Instantiate(LazerParticle, transform.position, Quaternion.identity);
                newParticle.Play();
                Destroy(newParticle.gameObject, 5.0f);

                particleTimer = 0f; // タイマーリセット
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lazer") || other.CompareTag("FinalLazer"))
        {
            SetLaserHit(false); // 離れたとき
        }
    }

    void OnCollisionEnter(Collision other)
    {
        tookDamage = false; // ここで毎回初期化

        string tag = other.gameObject.tag;
        if (other.gameObject.tag == "Enemy"&&!isShieldActive)
        {
            currentHp -= 5;
            hpSlider.value = (float)currentHp / (float)maxHp;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
            tookDamage = true;
            Damaged();
        }
    }

    void ShieldActive()
    {
        if (!isShieldActive && // 展開中でない
            gameManagerScript.GetBatteryEnargy() >= 30 &&
            (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown(KeyCode.Q)))
        {
            StartCoroutine(HandleShield());
        }
    }

    IEnumerator HandleShield()
    {
        isShieldActive = true; // 展開中フラグON
        gameManagerScript.ShieldBatteryEnargy();// シールド展開時にバッテリーエネルギーを消費
        Shield.SetActive(true);

        // 3秒間表示
        yield return new WaitForSeconds(3f);

        // 1秒間点滅（0.2秒ごとにON/OFF）
        float blinkDuration = 1f;
        float blinkInterval = 0.2f;
        float elapsed = 0f;

        while (elapsed < blinkDuration)
        {
            Shield.SetActive(false);
            yield return new WaitForSeconds(blinkInterval);
            Shield.SetActive(true);
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval * 2;
        }

        Shield.SetActive(false);
        isShieldActive = false; // 展開終了
    }


    // 他スクリプトから呼べる参照用メソッド
    public bool IsLazerPowerUp()
    {
        return isLaserPoweredUp;
    }
    public bool IsSinglePowerUp()
    {
        return isSinglePoweredUp;
    }
    public bool IsPenetrationPowerUp()
    {
        return isPenetrationPoweredUp;
    }
    public void SetLaserHit(bool v)
    {
        isTouchingLaser = v;
    }
    public bool IsSheildActive()
    {
        return isShieldActive;
    }

    public bool IsTouchingLaser()
    {
        return isTouchingLaser;
    }
    public bool IsDamage()
    {
        return isDamaged;
    }
    public void SetTookDamage(bool val)
    {
        tookDamage = val;
    }

    public bool IsTookDamage()
    {
        return tookDamage;
    }

    public void ResetDamageFlag()
    {
        tookDamage = false;
    }

    public float Speed()
    {
        return moveSpeed * Time.deltaTime;
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
        currentHp -= 20;
        hpSlider.value = (float)currentHp / (float)maxHp;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
    }
    public void UpHp()
    {
        currentHp += 20;
        hpSlider.value = (float)currentHp / (float)maxHp;//スライダは０〜1.0で表現するため最大HPで割って少数点数字に変換
    }

    public bool IsPenetrationShotChenge()
    {
        return penetrationShotChenge;
    }
    public bool IsSingleShotChenge()
    {
        return singleShotChenge;
    }

    public bool IsLazerShotChenge()
    {
        return lazerShotChenge;
    }
    public bool IsMoveActive()
    {
        return isMoveActive;
    }
}
