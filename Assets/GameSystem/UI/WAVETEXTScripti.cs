using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WAVETEXTScripti : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;
    //IMAGE
    public GameObject WAVEText0;
    public GameObject WAVEText1;
    public GameObject WAVEText2;
    public GameObject WAVEText3;
    public GameObject WAVEText4;
    public GameObject WAVEText5;
    //WAVE
    private float WaveImageActiveTime;
   
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        WaveImageActiveTime = 0;
        WAVEText0.SetActive(false);
        WAVEText1.SetActive(false);
        WAVEText2.SetActive(false);
        WAVEText3.SetActive(false);
        WAVEText4.SetActive(false);
        WAVEText5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�e�L�X�g
        if (gameManagerScript.IsGameStart() == true)
        {
            WaveImageActiveTime += Time.deltaTime;
            if (gameManagerScript.IsBossWaveCount() < 20)
            {
                if(WaveImageActiveTime <= 3)
                {
                    WAVEText0.SetActive(true);
                    WAVEText1.SetActive(false);
                    WAVEText2.SetActive(false);
                    WAVEText3.SetActive(false);
                    WAVEText4.SetActive(false);
                    WAVEText5.SetActive(false);
                }
                
            }
            if (gameManagerScript.IsBossWaveCount() > 20 && gameManagerScript.IsBossWaveCount() <= 40)
            {
                if (WaveImageActiveTime <= 23)
                {
                    WAVEText0.SetActive(false);
                    WAVEText1.SetActive(true);
                    WAVEText2.SetActive(false);
                    WAVEText3.SetActive(false);
                    WAVEText4.SetActive(false);
                    WAVEText5.SetActive(false);
                }
            }
            if (gameManagerScript.IsBossWaveCount() >= 40 && gameManagerScript.IsBossWaveCount() < 60)
            {
                if (WaveImageActiveTime <= 43)
                {
                    WAVEText0.SetActive(false);
                    WAVEText1.SetActive(false);
                    WAVEText2.SetActive(true);
                    WAVEText3.SetActive(false);
                    WAVEText4.SetActive(false);
                    WAVEText5.SetActive(false);
                }
            }
            if (gameManagerScript.IsBossWaveCount() >= 60 && gameManagerScript.IsBossWaveCount() < 80)
            {
                if (WaveImageActiveTime <= 63)
                {
                    WAVEText0.SetActive(false);
                    WAVEText1.SetActive(false);
                    WAVEText2.SetActive(false);
                    WAVEText3.SetActive(true);
                    WAVEText4.SetActive(false);
                    WAVEText5.SetActive(false);
                }
            }
            if (gameManagerScript.IsBossWaveCount() >= 80 && gameManagerScript.IsBossWaveCount() < 100)
            {
                if (WaveImageActiveTime <= 83)
                {
                    WAVEText0.SetActive(false);
                    WAVEText1.SetActive(false);
                    WAVEText2.SetActive(false);
                    WAVEText3.SetActive(false);
                    WAVEText4.SetActive(true);
                    WAVEText5.SetActive(false);
                }
            }
            if (gameManagerScript.IsBossWaveCount() >= 100)
            {
                if (WaveImageActiveTime <= 103)
                {
                    WAVEText0.SetActive(false);
                    WAVEText1.SetActive(false);
                    WAVEText2.SetActive(false);
                    WAVEText3.SetActive(false);
                    WAVEText4.SetActive(false);
                    WAVEText5.SetActive(true);
                }
            }
        }
        else
        {
            //WAVEText
            WAVEText0.SetActive(false);
            WAVEText1.SetActive(false);
            WAVEText2.SetActive(false);
            WAVEText3.SetActive(false);
            WAVEText4.SetActive(false);
            WAVEText5.SetActive(false);
        }
    }
}
