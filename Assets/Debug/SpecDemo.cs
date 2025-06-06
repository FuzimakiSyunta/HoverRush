using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecDemo : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gameManagerScript;

    public GameObject boss;
    private BossScript bossScript;

    public GameObject player;
    private PlayerStatus playerStatus;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        bossScript = boss.GetComponent<BossScript>();
        playerStatus = player.GetComponent<PlayerStatus>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) gameManagerScript.BatteryEnargyUp();
        if (Input.GetKey(KeyCode.N)) bossScript.BossWaveTime();
        if (Input.GetKeyDown(KeyCode.K)) gameManagerScript.BossWaveCountStart();

        if (Input.GetKeyDown(KeyCode.Alpha0)) playerStatus.DecreaseHp(10);
        if (Input.GetKey(KeyCode.Alpha1)) playerStatus.IncreaseHp(10);
        if (Input.GetKey(KeyCode.Alpha2)) bossScript.DownHp();
        if (Input.GetKey(KeyCode.Alpha3)) bossScript.UpHp();
        if (Input.GetKeyDown(KeyCode.Alpha4)) gameManagerScript.HealCounter();

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale += 0.5f;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 10f);
            Debug.Log("TimeScaleÇè„è∏: " + Time.timeScale);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Time.timeScale -= 0.5f;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 5f);
            Debug.Log("TimeScaleÇå∏è≠: " + Time.timeScale);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = 1.0f;
            Debug.Log("TimeScaleÉäÉZÉbÉg: " + Time.timeScale);
        }
    }
}