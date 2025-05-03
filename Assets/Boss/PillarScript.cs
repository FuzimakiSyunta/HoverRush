using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour
{
    // Boss
    public GameObject boss;
    private BossScript bossScript;
    public GameObject pillar;
    // Start is called before the first frame update
    void Start()
    {
        //boss
        bossScript = boss.GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossScript.IsFinalBattle())
        {
            pillar.SetActive(true);
        }
        else
        {
            pillar.SetActive(false);
        }
    }
}
