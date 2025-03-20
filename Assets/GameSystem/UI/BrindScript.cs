using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrindScript : MonoBehaviour
{
    // GameManager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    // Boss and Enemy
    public GameObject boss;
    public GameObject bossBullet;
    public GameObject EnergyImage;

    // SelectUI
    public GameObject StartSelectImage;
    public GameObject LuleSelectImage;
    public GameObject StartSelectCoverImage;
    public GameObject LuleBGImage;
    public GameObject LuleUiImage;

    // UI Elements
    public GameObject Hp;
    public GameObject A_Select;
    public GameObject OptionImage;

    // Energy Levels
    public GameObject EnergyEMP;
    public GameObject EnergyMIN;
    public GameObject EnergyMID;
    public GameObject EnergyMAX;

    // Wave Texts
    public GameObject WAVETextFirst;
    public GameObject WAVETextWarnigFarstWave;
    public GameObject WAVETextSecond;
    public GameObject WAVETextWarnigSecondWave;
    public GameObject WAVETextFinalWave;
    public GameObject WAVETextWarnigFinalWave;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();

        // Initialize Elements
        SetActiveForObjects(false, boss, bossBullet, EnergyImage, Hp, EnergyEMP, EnergyMIN, EnergyMID, EnergyMAX,
                            WAVETextWarnigFarstWave, WAVETextSecond, WAVETextWarnigSecondWave, WAVETextFinalWave, WAVETextWarnigFinalWave,
                            A_Select, OptionImage, LuleBGImage, LuleUiImage);

        StartSelectCoverImage.SetActive(true);
    }

    void Update()
    {
        if (!gameManagerScript.IsGameClear())
        {
            HandleGameStart();
            HandleGameOver();
            HandleSelectorMenu();
            HandleEnergyLevels();
        }
    }

    private void HandleGameStart()
    {
        if (gameManagerScript.IsGameStart() && !gameManagerScript.IsGameOver())
        {
            SetActiveForObjects(true, boss, bossBullet, EnergyImage, Hp, EnergyEMP, OptionImage);
            SetActiveForObjects(false, StartSelectImage, LuleSelectImage, StartSelectCoverImage, A_Select, LuleBGImage, LuleUiImage);
        }
    }

    private void HandleGameOver()
    {
        if (gameManagerScript.IsGameOver())
        {
            SetActiveForObjects(false, boss, bossBullet, EnergyImage, StartSelectImage, LuleSelectImage, StartSelectCoverImage,
                                A_Select, OptionImage, Hp, WAVETextWarnigFarstWave, WAVETextSecond, WAVETextWarnigSecondWave,
                                WAVETextFinalWave, WAVETextWarnigFinalWave);
        }
    }

    private void HandleSelectorMenu()
    {
        if (gameManagerScript.IsOpenSelector())
        {
            StartSelectCoverImage.SetActive(false);
            SetActiveForObjects(true, A_Select);
            SetActiveForObjects(false, OptionImage);
        }
    }

    private void HandleEnergyLevels()//energy
    {
        if (gameManagerScript.IsGameStart())
        {
            SetActiveForObjects(false, A_Select);
            SetActiveForObjects(true, OptionImage);

            if (gameManagerScript.IsScore() >= 5) EnergyMIN.SetActive(true);
            if (gameManagerScript.IsScore() >= 10) EnergyMID.SetActive(true);
            if (gameManagerScript.IsScore() >= 15) EnergyMAX.SetActive(true);
        }
    }

    private void SetActiveForObjects(bool state, params GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null) obj.SetActive(state);
        }
    }
}
