using UnityEngine;

[RequireComponent(typeof(BossController))]
public class BossManager : MonoBehaviour
{
    private BossController bossController;
    private BossDamageHandler damageHandler;
    private BossLaserController laserController;
    private BossAttackShooter attackShooter;
    private BossUIUpdater uiUpdater;
    private BossAudioManager audioManager;
    private BossParticleManager particleManager;

    private void Awake()
    {
        bossController = GetComponent<BossController>();
        damageHandler = GetComponent<BossDamageHandler>();
        laserController = GetComponent<BossLaserController>();
        attackShooter = GetComponent<BossAttackShooter>();
        uiUpdater = GetComponent<BossUIUpdater>();
        audioManager = GetComponent<BossAudioManager>();
        particleManager = GetComponent<BossParticleManager>();

        InjectDependencies();
    }

    private void InjectDependencies()
    {
        if (damageHandler != null)
        {
            damageHandler.Initialize(bossController, uiUpdater, audioManager, particleManager);
        }

        // ëºÇ…ïKóvÇ»èâä˙âªÇ™Ç†ÇÍÇŒÇ±Ç±Ç…í«â¡
    }
}
