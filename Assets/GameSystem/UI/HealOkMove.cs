using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealOkMove : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gameManagerScript;

    public GameObject pauseSystem;
    private PauseSystem pauseSystemScript;

    public GameObject Stunby; // �_�ŗp�I�u�W�F�N�g1
    public GameObject HealOk; // �_�ŗp�I�u�W�F�N�g2

    public float switchInterval = 0.5f;

    private float timer = 0f;
    private bool showingStunby = true;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        pauseSystemScript = pauseSystem.GetComponent<PauseSystem>();

        // �����͔�\��
        Stunby.SetActive(false);
        HealOk.SetActive(false);
    }

    void Update()
    {
        int healBatteryEnergy = gameManagerScript.GetHealBatteryEnargy();

        // �񕜕s�� or �|�[�Y�� �� ������\��
        if (pauseSystemScript.IsPaused() || healBatteryEnergy < 9)
        {
            Stunby.SetActive(false);
            HealOk.SetActive(false);
            return;
        }

        // �񕜉\ �� 0.5�b���ƂɌ��݂ɓ_��
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
