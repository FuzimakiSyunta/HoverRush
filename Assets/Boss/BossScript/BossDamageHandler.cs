using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossDamageHandler : MonoBehaviour
{
    [Header("éQè∆")]
    private BossController bossController;
    private BossUIUpdater uiUpdater;
    private BossAudioManager audioManager;
    private BossParticleManager particleManager;
    [SerializeField] private Slider hpSlider;

    private float lazerCoolTime;
    private float lazerLCoolTime;
    private float lazerRCoolTime;

    public void Initialize(BossController ctrl, BossUIUpdater ui, BossAudioManager audio, BossParticleManager particle)
    {
        bossController = ctrl;
        uiUpdater = ui;
        audioManager = audio;
        particleManager = particle;
    }

    private void Update()
    {
        lazerCoolTime += Time.deltaTime;
        lazerLCoolTime += Time.deltaTime;
        lazerRCoolTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!bossController.IsInBossWave()) return;

        string tag = other.tag;
        int damage = tag switch
        {
            "Bullet" => 300,
            "Machinegun" => 100,
            "PenetrationBullet" => 400,
            _ => 0
        };

        if (damage > 0)
        {
            ApplyDamage(damage, tag, other.transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!bossController.IsInBossWave()) return;

        string tag = other.tag;
        bool canDamage = tag switch
        {
            "PlayerLazer" => lazerCoolTime >= 0.1f,
            "PlayerLazer_L" => lazerLCoolTime >= 0.1f,
            "PlayerLazer_R" => lazerRCoolTime >= 0.1f,
            _ => false
        };

        if (canDamage)
        {
            int damage = tag == "PlayerLazer" ? 150 : 80;
            ApplyDamage(damage, tag, other.transform.position);

            switch (tag)
            {
                case "PlayerLazer": lazerCoolTime = 0f; break;
                case "PlayerLazer_L": lazerLCoolTime = 0f; break;
                case "PlayerLazer_R": lazerRCoolTime = 0f; break;
            }
        }
    }

    private void ApplyDamage(int amount, string tag, Vector3 hitPosition)
    {
        bossController.ReduceHp(amount);
        hpSlider.value = bossController.GetHpRate();
        audioManager.PlayDamageSound();
        uiUpdater.ShowDamageImage(tag, hitPosition); // Å© âÊëúï\é¶Ç…ñﬂÇ∑

        if (tag.Contains("Lazer"))
            particleManager.PlayLazerDamageEffect(transform.position);
        else
            particleManager.PlayDamageEffect(transform.position);
    }
}
