using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecDemo : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gameManagerScript;

    public GameObject boss;
    private BossController bossController;

    public GameObject player;
    private PlayerStatus playerStatus;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        bossController = boss.GetComponent<BossController>();
        playerStatus = player.GetComponent<PlayerStatus>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) gameManagerScript.BatteryEnargyUp();
        if (Input.GetKey(KeyCode.N)) bossController.BossWaveTimeAdd(1f);
        if (Input.GetKeyDown(KeyCode.K)) gameManagerScript.BossWaveCountStart();

        if (Input.GetKeyDown(KeyCode.Alpha0)) playerStatus.DecreaseHp(10);
        if (Input.GetKey(KeyCode.Alpha1)) playerStatus.IncreaseHp(10);
        if (Input.GetKey(KeyCode.Alpha2)) bossController.ReduceHp(100);
        if (Input.GetKey(KeyCode.Alpha3)) bossController.ReduceHp(-100);
        if (Input.GetKeyDown(KeyCode.Alpha4)) gameManagerScript.HealCounter();

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale += 0.5f;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 10f);
            Debug.Log("TimeScale: " + Time.timeScale);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Time.timeScale -= 0.5f;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 5f);
            Debug.Log("TimeScale: " + Time.timeScale);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = 1.0f;
            Debug.Log("TimeScale Reset: " + Time.timeScale);
        }
    }
}
