using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public GameManager gameManagerScript;
    public PlayerModels playerModelsScript;
    public PlayerStatus playerStatus;

    public GameObject bullet;
    public GameObject machineGun;
    public GameObject Lazer;
    public GameObject Lazer_R;
    public GameObject Lazer_L;
    public GameObject PenetrationBullet;
    public GameObject DamegeRing;

    public float[] bulletTimer;
    public bool singleShotChenge;
    public bool lazerShotChenge;
    public bool penetrationShotChenge;
    public bool isLaserPoweredUp;
    public bool isSinglePoweredUp;
    public bool isPenetrationPoweredUp;

    void Start()
    {
        bulletTimer = new float[3];
    }

    public void UpdateShotPattern()
    {
        int energy = gameManagerScript.GetBatteryEnargy();

        lazerShotChenge = energy >= 20;
        if (lazerShotChenge && !isLaserPoweredUp)
        {
            isLaserPoweredUp = true;
            playerStatus.isLaserPoweredUp = true;
        }

        singleShotChenge = energy >= 25;
        if (singleShotChenge && !isSinglePoweredUp)
        {
            isSinglePoweredUp = true;
            playerStatus.isSinglePoweredUp = true;
        }

        penetrationShotChenge = energy >= 30;
        if (penetrationShotChenge && !isPenetrationPoweredUp)
        {
            isPenetrationPoweredUp = true;
            playerStatus.isPenetrationPoweredUp = true;
        }
    }

    public void HandleFixedUpdate()
    {
        if (!gameManagerScript.IsGameStart()) return;

        int index = playerModelsScript.IsIndex();

        if (index == 0)
        {
            HandleSingleShot();
        }
        else if (index == 1)
        {
            Lazer.SetActive(true);
            Lazer_R.SetActive(playerStatus.IsLaserPoweredUp());
            Lazer_L.SetActive(playerStatus.IsLaserPoweredUp());
        }
        else
        {
            Lazer.SetActive(false);
            Lazer_R.SetActive(false);
            Lazer_L.SetActive(false);
        }

        if (index == 2)
        {
            HandlePenetrationShot();
        }
    }

    private void HandleSingleShot()
    {
        if (bulletTimer[0] == 0.0f)
        {
            Vector3 position = transform.position + new Vector3(0, 0.3f, 1.6f);
            Instantiate(bullet, position, Quaternion.identity);
            bulletTimer[0] = 1.0f;
        }
        else if (++bulletTimer[0] > 15.0f)
        {
            bulletTimer[0] = 0.0f;
        }

        if (bulletTimer[1] == 0.0f && singleShotChenge)
        {
            Vector3 positionR = transform.position + new Vector3(2.0f, 0.3f, 0f);
            Vector3 positionL = transform.position + new Vector3(-2.0f, 0.3f, 0f);
            Instantiate(machineGun, positionR, Quaternion.identity);
            Instantiate(machineGun, positionL, Quaternion.identity);
            bulletTimer[1] = 1.0f;
        }
        else if (++bulletTimer[1] > 5.0f)
        {
            bulletTimer[1] = 0.0f;
        }
    }

    private void HandlePenetrationShot()
    {
        if (bulletTimer[0] == 0.0f)
        {
            Vector3 position = transform.position + new Vector3(0, 0.3f, 1.6f);
            Instantiate(PenetrationBullet, position, Quaternion.identity);
            if (playerStatus.IsPenetrationPoweredUp())
            {
                // ¶‰E‚©‚ç‚à”­ŽË
                Vector3 positionR = transform.position + new Vector3(2.0f, 0.3f, 1.6f);
                Vector3 positionL = transform.position + new Vector3(-2.0f, 0.3f, 1.6f);
                Instantiate(PenetrationBullet, positionR, Quaternion.identity);
                Instantiate(PenetrationBullet, positionL, Quaternion.identity);
            }
            bulletTimer[0] = 1.0f;
        }
        else if (++bulletTimer[0] > 30.0f)
        {
            bulletTimer[0] = 0.0f;
        }

        DamegeRing.SetActive(playerStatus.IsPenetrationPoweredUp());
    }
}