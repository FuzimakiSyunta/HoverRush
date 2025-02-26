using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrindScript : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossBullet;
    public GameObject EnergyImage;
    public GameObject StartSelectImage;
    public GameObject LuleSelectImage;
    public GameObject SelectorImage;
    public GameObject StartSelectCoverImage;
    private GameManager gameManagerScript;
    public GameObject gameManager;
    public GameObject LuleBGImage;
    public GameObject LuleUiImage;
    public GameObject Hp;
    public GameObject EnergyEMP;
    public GameObject EnergyMIN;
    public GameObject EnergyMID;
    public GameObject EnergyMAX;
    public GameObject WAVETextFirst;
    public GameObject WAVETextWarnigFarstWave;
    public GameObject WAVETextSecond;
    public GameObject WAVETextWarnigSecondWave;
    public GameObject WAVETextFinalWave;
    public GameObject WAVETextWarnigFinalWave;
    public GameObject ClearText;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        boss.SetActive(false);
        bossBullet.SetActive(false);
        EnergyImage.SetActive(false);
        StartSelectCoverImage.SetActive(true);
        Hp.SetActive(false);
        EnergyEMP.SetActive(false);
        EnergyMIN.SetActive(false);
        EnergyMID.SetActive(false);
        EnergyMAX.SetActive(false);
        LuleBGImage.SetActive(false);
        LuleUiImage.SetActive(false);
        WAVETextWarnigFarstWave.SetActive(false);
        WAVETextSecond.SetActive(false);
        WAVETextWarnigSecondWave.SetActive(false);
        WAVETextFinalWave.SetActive(false);
        WAVETextWarnigFinalWave.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.IsGameClear()==false)
        {
            if (gameManagerScript.IsGameStart() == true && gameManagerScript.IsGameOver() == false)//gamestart
            {
                boss.SetActive(true);
                bossBullet.SetActive(true);
                EnergyImage.SetActive(true);
                StartSelectImage.SetActive(false);
                LuleSelectImage.SetActive(false);
                SelectorImage.SetActive(false);
                StartSelectCoverImage.SetActive(false);
                LuleBGImage.SetActive(false);
                LuleUiImage.SetActive(false);
                Hp.SetActive(true);
                EnergyEMP.SetActive(true);

            }
            if (gameManagerScript.IsGameOver() == true)//gamestart
            {
                boss.SetActive(false);
                EnergyImage.SetActive(false);
                StartSelectImage.SetActive(false);
                LuleSelectImage.SetActive(false);
                SelectorImage.SetActive(false);
                StartSelectCoverImage.SetActive(false);
                LuleBGImage.SetActive(false);
                LuleUiImage.SetActive(false);
                EnergyImage.SetActive(false);
                Hp.SetActive(false);
                WAVETextWarnigFarstWave.SetActive(false);
                WAVETextSecond.SetActive(false);
                WAVETextWarnigSecondWave.SetActive(false);
                WAVETextFinalWave.SetActive(false);
                WAVETextWarnigFinalWave.SetActive(false);
            }


            if (gameManagerScript.IsOpenSelector() == true)//selectormenu
            {
                StartSelectCoverImage.SetActive(false);

            }

            if (gameManagerScript.IsGameStart() == true)
            {
                if (gameManagerScript.IsScore() >= 5)
                {
                    EnergyMIN.SetActive(true);
                }
                if (gameManagerScript.IsScore() >= 10)
                {
                    EnergyMID.SetActive(true);
                }
                if (gameManagerScript.IsScore() >= 15)
                {
                    EnergyMAX.SetActive(true);
                }
            }
        }
        
        
    }
}
