using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private float LazerdamegeCoolTime = 0;

    //Bossのステータス
    public int bossHP;// ボスの最大HP
    private int wkHP;  // ボスの現在のHP
    public UnityEngine.UI.Slider hpSlider; //HPバー（スライダー）
    public ParticleSystem particle;
    public bool sliderBool;
    private Animator animator;
    private bool StartTime=false;

    //ダメージ表記
    private float imageDisplayTime = 1.0f; // 画像を表示する時間
    public GameObject bulletdamageImage; // ダメージを受けた際に表示する画像
    public GameObject MachinegunDamegeImage;
    public GameObject PenetrationBulletDamegeImage;
    public GameObject LazerDamegeImage;

    public void Damage(Collider col)
    {
        //　DamageUIをインスタンス化。登場位置は接触したコライダの中心からカメラの方向に少し寄せた位置
        var obj = Instantiate(bulletdamageImage, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
    }

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

            //Debag
            if (Input.GetKeyDown(KeyCode.X))
            {
                BossBattleTime++;
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
            Destroy(newParticle.gameObject, 2.5f);

            //ボス消える
            gameManagerScript.GameClearStart();//ゲームクリア
            Destroy(gameObject, 0f);

        }
        ///ダメージ非表示
        if(gameManagerScript.IsGameOver()||gameManagerScript.IsGameClear())
        {
            bulletdamageImage.SetActive(false);
            MachinegunDamegeImage.SetActive(false);
            PenetrationBulletDamegeImage.SetActive(false);
            LazerDamegeImage.SetActive(false);
        }
        if (LazerdamegeCoolTime < 0.5f)
        {
            LazerdamegeCoolTime += Time.deltaTime; // クールタイムを進める
        }

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject damageImage = null;
        int damage = 0;

        // 各タグに応じたダメージ値と画像を設定
        switch (other.gameObject.tag)
        {
            case "Bullet":
                damage = 300;
                damageImage = bulletdamageImage;
                break;
            case "Machinegun":
                damage = 100;
                damageImage = MachinegunDamegeImage;
                break;
            case "PenetrationBullet":
                damage = 400;
                damageImage = PenetrationBulletDamegeImage;
                break;
            
        }
        

        if (damageImage != null)
        {
            audioSource.PlayOneShot(DamegeSound);
            wkHP -= damage;
            hpSlider.value = (float)wkHP / (float)bossHP; // HPスライダーを更新
            sliderBool = true;

            // 画像を当たった位置に移動して表示
            Vector3 hitPosition = other.transform.position;
            ShowDamageImageAtPosition(damageImage, hitPosition);
        }
    }

    void OnTriggerStay(Collider other)
    {
        // PlayerLazer専用の処理
        if (other.gameObject.tag == "PlayerLazer")
        {
            if (LazerdamegeCoolTime >= 0.1f) // クールタイム判定
            {
                int damage = 30; // ダメージ値
                GameObject damageImage = LazerDamegeImage;

                audioSource.PlayOneShot(DamegeSound);
                wkHP -= damage; // HPを減少
                hpSlider.value = (float)wkHP / (float)bossHP; // HPスライダーを更新
                sliderBool = true;

                // 画像を当たった位置に移動して表示
                Vector3 hitPosition = other.transform.position;
                ShowDamageImageAtPosition(damageImage, hitPosition);

                // クールタイムをリセット
                LazerdamegeCoolTime = 0;
            }
        }
    }
    private void ShowDamageImageAtPosition(GameObject damageImage, Vector3 position)
    {
        // 表示位置を少し上に調整
        Vector3 adjustedPosition = position + new Vector3(0, 6.5f, 0);
        damageImage.transform.position = adjustedPosition; // 調整後の位置に設定

        // 画像を表示する前に透明度をリセット
        damageImage.SetActive(true);
        UnityEngine.UI.Image imageComponent = damageImage.GetComponent<UnityEngine.UI.Image>();
        if (imageComponent != null)
        {
            // 現在のフェードアウトを中断し、透明度をリセット
            StopAllCoroutines(); // 現在のCoroutineを停止
            Color tempColor = imageComponent.color;
            tempColor.a = 1.0f; // アルファを最大値（完全表示）に設定
            imageComponent.color = tempColor;

            MoveImageUpward(damageImage); // 画像を上方向に移動
            StartCoroutine(ForceHideImageAfterDelay(damageImage, 0.5f)); // 0.5秒後に強制非表示
            StartCoroutine(FadeOutImage(imageComponent)); // フェードアウトを開始
        }
    }
    private IEnumerator ForceHideImageAfterDelay(GameObject damageImage, float delay)//強制非表示
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        bulletdamageImage.SetActive(false);
        MachinegunDamegeImage.SetActive(false);
        PenetrationBulletDamegeImage.SetActive(false);
        LazerDamegeImage.SetActive(false);
    }
    

    private IEnumerator FadeOutImage(UnityEngine.UI.Image imageComponent)
    {
        float fadeDuration = imageDisplayTime; // フェードアウトの時間
        float elapsedTime = 0f;

        Color originalColor = imageComponent.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeDuration); // アルファ値を徐々に減少
            originalColor.a = alpha;
            imageComponent.color = originalColor; // 画像の色を更新
            yield return null; // 次のフレームまで待機
        }

        // フェードアウト終了後に画像を非表示
        imageComponent.gameObject.SetActive(false);
    }

    private void MoveImageUpward(GameObject damageImage)//上に移動
    {
        StartCoroutine(UpwardMovement(damageImage));
    }

    private IEnumerator UpwardMovement(GameObject damageImage)
    {
        float moveDuration = 1.0f; // 移動する時間（秒）
        float elapsedTime = 0f;
        Vector3 originalPosition = damageImage.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, 5.0f, 0); // 目標位置

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            // 緩やかに位置を補間
            damageImage.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / moveDuration);
            yield return null; // 次のフレームまで待機
        }

        // 最後に位置を目標位置に設定
        damageImage.transform.position = targetPosition;
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
        if (BossBattleTime > 20 && BossBattleTime <= 131) // 共通範囲
        {
            animator.SetBool("isMove", BossBattleTime <= 40 || BossBattleTime >= 131);
            animator.SetBool("isLazer", BossBattleTime >= 60 && BossBattleTime < 80);
            animator.SetBool("isTransform", BossBattleTime >= 80 && BossBattleTime < 100);
            animator.SetBool("isRobotStay", BossBattleTime >= 85 && BossBattleTime < 100);
            animator.SetBool("isAirTransform", BossBattleTime >= 100 && BossBattleTime < 103);
            animator.SetBool("FinalWave", BossBattleTime >= 101 && BossBattleTime < 131);
            animator.SetBool("isFinalBullet", BossBattleTime >= 131);
        }

        if (BossBattleTime >= 60 && BossBattleTime < 80)
        {
            isLazerWave = true;
        }
        if (BossBattleTime >= 80 && BossBattleTime < 100)
        {
            isLazerWave = false;
        }
    }

    public bool IsFadeStart()
    {
        return isfadeLazer;
    }

   

}
