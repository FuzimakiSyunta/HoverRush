using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BossScript : MonoBehaviour
{
    public GameObject gameManager;
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
    private float Lazer_RdamegeCoolTime = 0;
    private float Lazer_LdamegeCoolTime = 0;

    //Bossのステータス
    public int bossHP;// ボスの最大HP
    private int NowHP;  // ボスの現在のHP
    public UnityEngine.UI.Slider hpSlider; //HPバー（スライダー）
    public ParticleSystem particle;
    public bool sliderBool;
    private Animator animator;
    private bool StartTime=false;
    //damegeパーティクル
    public ParticleSystem damegeParticle;
    //Lazerdamegeパーティクル
    public ParticleSystem lazerDamegeParticle;

    //ダメージ表記
    private float imageDisplayTime = 1.0f; // 画像を表示する時間
    public GameObject bulletdamageImage; // ダメージを受けた際に表示する画像
    public GameObject MachinegunDamegeImage;
    public GameObject PenetrationBulletDamegeImage;
    public GameObject LazerDamegeImage;
    public GameObject Lazer_LDamegeImage;
    public GameObject Lazer_RDamegeImage;

    public GameObject DamegeCanvas;

    public GameObject EnemyPositionObject;
    //
    public bool isDameged = false;


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
        NowHP = bossHP; // 現在のHPを最大HPに設定
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
        isDameged = false;
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

            //posObject
            if (gameManagerScript.IsBossWave())
            {
                EnemyPositionObject.SetActive(true);
            }else
            {
                EnemyPositionObject.SetActive(false);
            }
            

        }
        // HPが0以下になった場合、自らを消す
        if (NowHP <= 0)
        {
            ParticleSystem newParticle = Instantiate(particle);
            //場所固定
            newParticle.transform.position = this.gameObject.transform.position;
            //発生
            newParticle.Play();
            //エフェクト消える
            Destroy(newParticle.gameObject, 2.5f);
            EnemyPositionObject.SetActive(false);

            //ボス消える
            gameManagerScript.GameClearStart();//ゲームクリア
            Destroy(gameObject, 0f);

            DamegeCanvas.SetActive(false);
            gameManagerScript.BatteryEnargyUp();

        }
        
        LazerdamegeCoolTime += Time.deltaTime; // クールタイムを進める
        Lazer_RdamegeCoolTime += Time.deltaTime;
        Lazer_LdamegeCoolTime += Time.deltaTime;
    }

    void Damaged()
    {
        isDameged = true;
        audioSource.PlayOneShot(DamegeSound);
        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(damegeParticle);
        // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
        newParticle.transform.position = this.transform.position;
        // パーティクルを発生させる。
        newParticle.Play();
        // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
        // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
        Destroy(newParticle.gameObject, 5.0f);

    }

    void LazerDamaged()
    {
        isDameged = true;
        audioSource.PlayOneShot(DamegeSound);
        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(lazerDamegeParticle);
        // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
        newParticle.transform.position = this.transform.position;
        // パーティクルを発生させる。
        newParticle.Play();
        // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
        // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
        Destroy(newParticle.gameObject, 5.0f);

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject damageImage = null;
        int damage = 0;
        if (gameManagerScript.IsBossWave())
        {
            // 各タグに応じたダメージ値と画像を設定
            switch (other.gameObject.tag)
            {
                case "Bullet":
                    damage = 300;
                    damageImage = bulletdamageImage;
                    Damaged();
                    break;
                case "Machinegun":
                    damage = 100;
                    damageImage = MachinegunDamegeImage;
                    Damaged();
                    break;
                case "PenetrationBullet":
                    damage = 400;
                    damageImage = PenetrationBulletDamegeImage;
                    Damaged();
                    break;

            }
            // ダメージを受けた際の処理
            audioSource.PlayOneShot(DamegeSound); // ダメージ音を再生

            if (damageImage != null)
            {
                audioSource.PlayOneShot(DamegeSound);
                NowHP -= damage;
                hpSlider.value = (float)NowHP / (float)bossHP; // HPスライダーを更新
                sliderBool = true;

                // 画像を当たった位置に移動して表示
                Vector3 hitPosition = other.transform.position;
                ShowDamageImageAtPosition(damageImage, hitPosition);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        // PlayerLazer専用の処理
        if (other.gameObject.tag == "PlayerLazer"&&gameManagerScript.IsBossWave())
        {
            if (LazerdamegeCoolTime >= 0.1f) // クールタイム判定
            {
                int damage = 150; // ダメージ値
                GameObject damageImage = LazerDamegeImage;

                audioSource.PlayOneShot(DamegeSound);
                NowHP -= damage; // HPを減少
                hpSlider.value = (float)NowHP / (float)bossHP; // HPスライダーを更新
                sliderBool = true;

                // 画像を当たった位置に移動して表示
                Vector3 hitPosition = other.transform.position;
                ShowDamageImageAtPosition(damageImage, hitPosition);

                LazerDamaged();//damege

                // クールタイムをリセット
                LazerdamegeCoolTime = 0;
            }
        }
        // PlayerLazer専用の処理
        if (other.gameObject.tag == "PlayerLazer_L" && gameManagerScript.IsBossWave())
        {
            if (Lazer_LdamegeCoolTime >= 0.1f) // クールタイム判定
            {
                int damage = 80; // ダメージ値
                GameObject damageImage = Lazer_LDamegeImage;

                audioSource.PlayOneShot(DamegeSound);
                NowHP -= damage; // HPを減少
                hpSlider.value = (float)NowHP / (float)bossHP; // HPスライダーを更新
                sliderBool = true;

                // 画像を当たった位置に移動して表示
                Vector3 hitPosition = other.transform.position;
                ShowDamageImageAtPosition(damageImage, hitPosition);

                LazerDamaged();//damege

                // クールタイムをリセット
                Lazer_LdamegeCoolTime = 0;
            }
        }
        // PlayerLazer専用の処理
        if (other.gameObject.tag == "PlayerLazer_R" && gameManagerScript.IsBossWave())
        {
            if (Lazer_RdamegeCoolTime >= 0.1f) // クールタイム判定
            {
                int damage = 80; // ダメージ値
                GameObject damageImage = Lazer_RDamegeImage;

                audioSource.PlayOneShot(DamegeSound);
                NowHP -= damage; // HPを減少
                hpSlider.value = (float)NowHP / (float)bossHP; // HPスライダーを更新
                sliderBool = true;

                // 画像を当たった位置に移動して表示
                Vector3 hitPosition = other.transform.position;
                ShowDamageImageAtPosition(damageImage, hitPosition);

                LazerDamaged();//damege

                // クールタイムをリセット
                Lazer_RdamegeCoolTime = 0;
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
        Lazer_RDamegeImage.SetActive(false);
        Lazer_LDamegeImage.SetActive(false);
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
        // 散弾
        if (MultibulletTimer == 0.0f)
        {
            Vector3 position = transform.position;
            MultiBulletCoolTime += Time.deltaTime; // 時間で加算
            if (animator.GetBool("isMove") == false && animator.GetBool("isLazer") == false
                && animator.GetBool("isRobotStay") == false && animator.GetBool("FinalWave") == false)
            {
                if (MultiBulletCoolTime >= 4.0f) // 240フレームの代わりに秒数で設定
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
            MultibulletTimer += Time.deltaTime; // 時間で加算
            if (MultibulletTimer > 5.0f) // 120フレームの代わりに秒数で設定
            {
                MultibulletTimer = 0.0f;
            }
            BulletCoolTime = 0;
        }

        // 爆弾ウェーブ
        if (bulletTimer == 0.0f)
        {
            Vector3 positionR = transform.position;
            Vector3 positionL = transform.position;
            BulletCoolTime += Time.deltaTime; // 時間で加算
            if (animator.GetBool("isMove") == true && animator.GetBool("isFinalBullet") == false)
            {
                if (BulletCoolTime >= 2f) // 60フレームの代わりに秒数で設定
                {
                    Instantiate(BossBarstbullet_R, positionR, Quaternion.identity);
                    Instantiate(BossBarstbullet_L, positionL, Quaternion.identity);
                    bulletTimer = 1.0f;
                }
            }
        }
        else
        {
            bulletTimer += Time.deltaTime; // 時間で加算
            if (bulletTimer > 1.5f) // 30フレームの代わりに秒数で設定
            {
                bulletTimer = 0.0f;
            }
            MultiBulletCoolTime = 0;
        }

        // レーザーウェーブ
        if (animator.GetBool("isLazer") == true)
        {
            LazerTime += Time.deltaTime; // 時間で加算
            LazerBulletCoolTime += Time.deltaTime; // 時間で加算
            if (LazerBulletCoolTime >= 1.0f) // 60フレームの代わりに秒数で設定
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
            LazerBulletCoolTime = 0.0f;
        }

        // 最終レーザー
        if (animator.GetBool("FinalWave") == true)
        {
            BIGLAZER.SetActive(true);
        }
        else
        {
            BIGLAZER.SetActive(false);
        }

        // 最終爆弾ウェーブ
        if (FinalbulletTimer == 0.0f)
        {
            Vector3 positionR = transform.position;
            Vector3 positionL = transform.position;
            BulletCoolTime += Time.deltaTime; // 時間で加算
            if (animator.GetBool("isMove") == true && animator.GetBool("isFinalBullet") == true)
            {
                if (BulletCoolTime >= 2f) // 60フレームの代わりに秒数で設定
                {
                    Instantiate(FinalBossBarstbullet_R, positionR, Quaternion.identity);
                    Instantiate(FinalBossBarstbullet_L, positionL, Quaternion.identity);
                    FinalbulletTimer = 1.0f;
                }
            }
        }
        else
        {
            FinalbulletTimer += Time.deltaTime; // 時間で加算
            if (FinalbulletTimer > 1.5f) // 30フレームの代わりに秒数で設定
            {
                FinalbulletTimer = 0.0f;
            }
            MultiBulletCoolTime = 0.0f;
        }

        // ロボット状態の切り替え
        if (animator.GetBool("isRobotStay") == true)
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
            animator.SetBool("isTransform", true);
            isLazerWave = false;
        }
        if (BossBattleTime >= 85 && BossBattleTime < 100)
        {
            animator.SetBool("isRobotStay", true);
        }
        if (BossBattleTime >= 100 && BossBattleTime < 103)
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

    public void BossWaveTime()
    {
       BossBattleTime++;
    }

    public void DownHp()
    {
        NowHP -= 800;
        hpSlider.value = (float)NowHP / (float)bossHP; // HPスライダーを更新
    }
    public void UpHp()
    {
        NowHP += 800;
        hpSlider.value = (float)NowHP / (float)bossHP; // HPスライダーを更新
    }
}
