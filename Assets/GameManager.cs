using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    int[] CoolTime = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i<5;i++)
        {
            CoolTime[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームオーバーなら
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("TitleScene");
        }

    }

    private void FixedUpdate()
    {
        int r = Random.Range(0, 15000);
        CoolTime[0]++;
        CoolTime[1]++;
        CoolTime[2]++;
        CoolTime[3]++;
        CoolTime[4]++;

        if (r <= 300)
        {
            if(CoolTime[0]>=30)
            {
                Instantiate(enemy, new Vector3(-8.0f, 1.5f, 45.0f), Quaternion.identity);
                CoolTime[0] = 0;
            }
        }
        if (r >= 2000&&r <= 2300)
        {
            if (CoolTime[1] >= 30)
            {
                Instantiate(enemy, new Vector3(0.0f, 1.5f, 45.0f), Quaternion.identity);
                CoolTime[1] = 0;
            }
        }
        if (r >= 4000 && r <= 4300)
        {
            if (CoolTime[2] >= 30)
            {
                Instantiate(enemy, new Vector3(8.0f, 1.5f, 45.0f), Quaternion.identity);
                CoolTime[2] = 0;
            }
        }
        if (r >= 6000 && r <= 6300)
        {
            if (CoolTime[3] >= 30)
            {
                Instantiate(enemy, new Vector3(4.0f, 1.5f, 45.0f), Quaternion.identity);
                CoolTime[3] = 0;
            }
        }
        if (r >= 8000 && r <= 8300)
        {
            if (CoolTime[4] >= 30)
            {
                Instantiate(enemy, new Vector3(-4.0f, 1.5f, 45.0f), Quaternion.identity);
                CoolTime[4] = 0;
            }
        }
    }

}
