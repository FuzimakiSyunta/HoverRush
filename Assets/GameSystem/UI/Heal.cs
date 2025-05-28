using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gameManagerScript;

    public GameObject pauseSystem;
    private PauseSystem pauseSystemScript;

    public GameObject Stunby; // 点滅用オブジェクト1
    public GameObject HealOk; // 点滅用オブジェクト2

    public float switchInterval = 0.5f;

    private float timer = 0f;
    private bool showingStunby = true;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        pauseSystemScript = pauseSystem.GetComponent<PauseSystem>();

        // 初期は非表示
        Stunby.SetActive(false);
        HealOk.SetActive(false);
    }

    void Update()
    {
        int healBatteryEnergy = gameManagerScript.GetHealBatteryEnargy();

        // 回復不可 or ポーズ中 → 両方非表示
        if (pauseSystemScript.IsPaused() || healBatteryEnergy < 9||gameManagerScript.IsGameClear()||gameManagerScript.IsGameOver())
        {
            Stunby.SetActive(false);
            HealOk.SetActive(false);
            return;
        }

        // 回復可能 → 0.5秒ごとに交互に点滅
        timer += Time.deltaTime;
        if (timer >= switchInterval)
        {
            timer = 0f;
            showingStunby = !showingStunby;
        }

        Stunby.SetActive(showingStunby);
        HealOk.SetActive(!showingStunby);
    }
}
