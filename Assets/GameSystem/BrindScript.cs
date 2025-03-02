using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrindScript : MonoBehaviour
{
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    //Boss
    public GameObject boss;
    public GameObject bossBullet;
    //Enemy
    public GameObject EnergyImage;
    public GameObject StartSelectImage;
    //SelectUI
    public GameObject LuleSelectImage;
    public GameObject SelectorImage;
    public GameObject StartSelectCoverImage;
    public GameObject LuleBGImage;
    public GameObject LuleUiImage;

    //ColorChengeUI
    public GameObject colorMenuWHITE;
    public GameObject colorMenuRED;
    public GameObject colorMenuYELLOW;

    //hp
    public GameObject Hp;
    //Energy
    public GameObject EnergyEMP;
    public GameObject EnergyMIN;
    public GameObject EnergyMID;
    public GameObject EnergyMAX;
    //Wavetext
    public GameObject WAVETextFirst;
    public GameObject WAVETextWarnigFarstWave;
    public GameObject WAVETextSecond;
    public GameObject WAVETextWarnigSecondWave;
    public GameObject WAVETextFinalWave;
    public GameObject WAVETextWarnigFinalWave;

    //LTRT
    public GameObject LTRT;
    


    // Start is called before the first frame update
    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //boss
        boss.SetActive(false);
        bossBullet.SetActive(false);
        //enemy
        EnergyImage.SetActive(false);
        Hp.SetActive(false);
        //Energy
        EnergyEMP.SetActive(false);
        EnergyMIN.SetActive(false);
        EnergyMID.SetActive(false);
        EnergyMAX.SetActive(false);
        //Lule
        StartSelectCoverImage.SetActive(true);
        LuleBGImage.SetActive(false);
        LuleUiImage.SetActive(false);
        LTRT.SetActive(false);
        ////color
        //colorMenuWHITE.SetActive(false);
        //colorMenuRED.SetActive(false);
        //colorMenuYELLOW.SetActive(false);
        //Wave
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
                //Boss
                boss.SetActive(true);
                bossBullet.SetActive(true);
                //enemy
                EnergyImage.SetActive(true);
                //SelectUI
                StartSelectImage.SetActive(false);
                LuleSelectImage.SetActive(false);
                SelectorImage.SetActive(false);
                StartSelectCoverImage.SetActive(false);
                LuleBGImage.SetActive(false);
                LuleUiImage.SetActive(false);
                LTRT.SetActive(false);
                ////color
                //colorMenuWHITE.SetActive(false);
                //colorMenuRED.SetActive(false);
                //colorMenuYELLOW.SetActive(false);
                //Hp
                Hp.SetActive(true);
                //Energy
                EnergyEMP.SetActive(true);

            }
            if (gameManagerScript.IsGameOver() == true)//gamestart
            {
                boss.SetActive(false);
                EnergyImage.SetActive(false);
                StartSelectImage.SetActive(false);
                //selectorUI
                LuleSelectImage.SetActive(false);
                SelectorImage.SetActive(false);
                StartSelectCoverImage.SetActive(false);
                LuleBGImage.SetActive(false);
                LuleUiImage.SetActive(false);
                EnergyImage.SetActive(false);
                LTRT.SetActive(false);
                ////color
                //colorMenuWHITE.SetActive(false);
                //colorMenuRED.SetActive(false);
                //colorMenuYELLOW.SetActive(false);
                //Hp
                Hp.SetActive(false);
                //Wave
                WAVETextWarnigFarstWave.SetActive(false);
                WAVETextSecond.SetActive(false);
                WAVETextWarnigSecondWave.SetActive(false);
                WAVETextFinalWave.SetActive(false);
                WAVETextWarnigFinalWave.SetActive(false);
            }


            if (gameManagerScript.IsOpenSelector() == true)//selectormenu
            {
                StartSelectCoverImage.SetActive(false);
                LTRT.SetActive(true);
            }
            

            if (gameManagerScript.IsGameStart() == true)
            {
                LTRT.SetActive(false);
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
