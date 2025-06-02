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
    private PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        bossScript = boss.GetComponent<BossScript>();
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            gameManagerScript.BatteryEnargyUp();//�X�R�A�㏸
        }

        if (Input.GetKey(KeyCode.N))
        {
            bossScript.BossWaveTime();//�{�X�펞�ԑ���
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            gameManagerScript.BossWaveCountStart();//�E�F�[�u����
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            playerScript.DownHp();//PlayerHp����
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            playerScript.UpHp();//PlayerHp����
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            bossScript.DownHp();//BossHp����
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            bossScript.UpHp();//BossHp����
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gameManagerScript.HealCounter();//�񕜉񐔑���
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale += 0.5f;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 10f); 
            Debug.Log("TimeScale���㏸: " + Time.timeScale);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Time.timeScale -= 0.5f;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 5f); // 0�ȉ��ɂȂ�Ȃ��悤��
            Debug.Log("TimeScale������: " + Time.timeScale);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = 1.0f; // �ʏ푬�x�ɖ߂�
            Debug.Log("TimeScale���Z�b�g: " + Time.timeScale);
        }
    }
}
