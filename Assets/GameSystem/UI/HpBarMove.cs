using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarMove : MonoBehaviour
{
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    public GameObject hpBar;
    // Start is called before the first frame update
    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float[] bossTimes = gameManagerScript.GetBossStartTimes();

        // ボス開始時間に関する処理
        if (bossTimes.Length > 3 && gameManagerScript.IsGamePlayCount()+3 >= bossTimes[3])
        {
            Vector3 currentPosition = hpBar.transform.position;
            hpBar.transform.position = new Vector3(40.0f, 20.0f, currentPosition.z);
        }
    }


}
