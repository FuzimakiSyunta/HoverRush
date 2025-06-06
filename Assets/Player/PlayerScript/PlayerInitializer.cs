using UnityEngine;
using UnityEngine.UI;

public class PlayerInitializer : MonoBehaviour
{
    public GameManager gameManagerScript;
    public GameObject gameManager;
    public GameObject boss;
    public SelectorMenu selectorMenuScript;
    public GameObject selectMenu;
    public PlayerModels playerModelsScript;
    public GameObject playerModels;
    public TutorialCameraMove cameraMoveScript;
    public GameObject cameraMove;
    public Animator animator;
    public AudioSource audioSource;
    public GameObject HoverParticle;
    public GameObject positionRing;
    public GameObject Shield;

    public float[] bulletTimer = new float[3];
    public PlayerStatus playerStatus;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
        playerModelsScript = playerModels.GetComponent<PlayerModels>();
        cameraMoveScript = cameraMove.GetComponent<TutorialCameraMove>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerStatus = GetComponent<PlayerStatus>();

        playerStatus.SetHp(playerStatus.maxHp);

        for (int i = 0; i < 3; i++) bulletTimer[i] = 0.0f;

        playerStatus.isDamaged = false;
        playerStatus.isHeal = false;
        playerStatus.DamegeCoolTime = 0;
        playerStatus.isLaserPoweredUp = false;
        playerStatus.isSinglePoweredUp = false;
        playerStatus.isPenetrationPoweredUp = false;
        playerStatus.isShieldActive = false;

        HoverParticle.SetActive(false);
        positionRing.SetActive(false);
        Shield.SetActive(false);
    }
}