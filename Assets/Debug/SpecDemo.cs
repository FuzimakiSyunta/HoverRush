using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecDemo : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gameManagerScript;

    public GameObject boss;
    private BossScript bossScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        bossScript = boss.GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            gameManagerScript.Score();//�X�R�A�㏸
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            bossScript.BossWaveTime();//�{�X�펞�ԑ���
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            gameManagerScript.BossWaveCountStart();//�E�F�[�u����
        }

    }
}
