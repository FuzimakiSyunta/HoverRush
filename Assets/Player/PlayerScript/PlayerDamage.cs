using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public GameManager gameManagerScript;
    public CameraMove cameraMoveScript;
    public PlayerStatus playerStatus;

    public ParticleSystem DamageParticle;
    public ParticleSystem ShieldParticle;
    public AudioClip DamegeSound;
    public AudioClip ShieldHitSound;

    private AudioSource audioSource;

    public float particleCooldown = 0.2f;
    public float damageCooldown = 0.2f;
    private float particleTimer = 0f;
    private float damageTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DamageUpdate()
    {
        particleTimer += Time.deltaTime;
        damageTimer += Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;

        if (tag == "EnemyBullet" || tag == "BossBullet" || tag == "BossExtraBullet" ||
            tag == "RobotBullet" || tag == "FinalBomm")
        {
            if (!playerStatus.isShieldActive)
            {
                switch (tag)
                {
                    case "EnemyBullet": playerStatus.DecreaseHp(10); break;
                    case "BossBullet": playerStatus.DecreaseHp(20); break;
                    case "BossExtraBullet": playerStatus.DecreaseHp(8); break;
                    case "RobotBullet": playerStatus.DecreaseHp(20); break;
                    case "FinalBomm": playerStatus.DecreaseHp(100); break;
                }

                playerStatus.tookDamage = true;
            }

            DamagedEffect(); // 演出は常に出す
        }

        if ((tag == "Enemy" || tag == "Lazer" || tag == "RobotBullet" ||
             tag == "FinalLazer" || tag == "Piller") &&
             !cameraMoveScript.IsAnimation() && !playerStatus.isShieldActive)
        {
            DamagedEffect();
        }
        else
        {
            playerStatus.isDamaged = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (gameManagerScript.IsGameOver() || gameManagerScript.IsGameClear())
            return;

        if ((other.CompareTag("Lazer") || other.CompareTag("FinalLazer")))
        {
            playerStatus.isTouchingLaser = true;

            if (!playerStatus.isShieldActive)
            {
                if (damageTimer >= damageCooldown)
                {
                    if (other.CompareTag("Lazer")) playerStatus.DecreaseHp(4);
                    else if (other.CompareTag("FinalLazer")) playerStatus.DecreaseHp(6);
                    damageTimer = 0f;
                }

                if (particleTimer >= particleCooldown)
                {
                    ParticleSystem newParticle = Instantiate(DamageParticle, transform.position, Quaternion.identity);
                    newParticle.Play();
                    Destroy(newParticle.gameObject, 5.0f);
                    particleTimer = 0f;
                }
            }
            else
            {
                // シールド中はダメージなし・シールド演出のみ
                if (particleTimer >= particleCooldown)
                {
                    ParticleSystem shieldFx = Instantiate(ShieldParticle, transform.position, Quaternion.identity);
                    shieldFx.Play();
                    Destroy(shieldFx.gameObject, 5.0f);
                    particleTimer = 0f;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lazer") || other.CompareTag("FinalLazer"))
        {
            playerStatus.isTouchingLaser = false;
        }
    }

    void DamagedEffect()
    {
        if (playerStatus.isShieldActive)
        {
            audioSource.PlayOneShot(ShieldHitSound);
            ParticleSystem shieldFx = Instantiate(ShieldParticle, transform.position, Quaternion.identity);
            shieldFx.Play();
            Destroy(shieldFx.gameObject, 5f);
        }
        else
        {
            audioSource.PlayOneShot(DamegeSound);
            ParticleSystem dmgFx = Instantiate(DamageParticle, transform.position, Quaternion.identity);
            dmgFx.Play();
            Destroy(dmgFx.gameObject, 5f);
            gameManagerScript.BatteryEnargyDown();
        }

        playerStatus.isDamaged = true;
    }

    public bool IsSheildActive() => playerStatus.isShieldActive;
    public bool IsTookDamage() => playerStatus.tookDamage;
    public bool IsTouchingLaser() => playerStatus.isTouchingLaser;
    public void ResetDamageFlag() => playerStatus.tookDamage = false;
}