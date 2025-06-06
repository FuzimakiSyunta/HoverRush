using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    public GameManager gameManagerScript;
    public PlayerImageUI uiController;
    public PlayerStatus playerStatus;

    private const int MaxHealHp = 300;
    private const int HealAmount = 150;

    public AudioClip healSound;
    public ParticleSystem healEffect;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Heal()
    {
        if (gameManagerScript.GetHealBatteryEnargy() < 9)
            playerStatus.isHeal = false;

        if (gameManagerScript.GetHealBatteryEnargy() < 9 && playerStatus.isHeal == false)
        {
            uiController.SetHealImage(false);
        }

        if (gameManagerScript.GetHealBatteryEnargy() >= 9 && playerStatus.isHeal == false && playerStatus.GetHp() < MaxHealHp)
        {
            uiController.SetHealImage(true);

            if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown("joystick button 2"))
            {
                playerStatus.IncreaseHp(HealAmount);
                playerStatus.isHeal = true;
                uiController.SetHealImage(false);
                gameManagerScript.HealBatteryEnargyReset();
                gameManagerScript.HealCounter();
            }
        }
    }
}