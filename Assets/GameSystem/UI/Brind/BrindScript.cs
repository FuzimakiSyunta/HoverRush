using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class BrindScript : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    //models
    private PlayerModels playerModelsScript;
    public GameObject playerModels;

    // Boss
    public GameObject boss;
    public GameObject bossBullet;
    //public GameObject RobotBullet;
    //�J�n�OUI
    public GameObject TitleUi;
    //�J�n��UI
    public GameObject StartUi;
    //Energy
    public GameObject AllEnergy;
    public GameObject EnergyMIN;
    public GameObject EnergyMID;
    public GameObject EnergyMAX;
    //Selector
    public GameObject Selector;
    
    //�Q�[���I�[�o�[
    public GameObject PushAGameOver;
    //�ݒ�{�^��
    public GameObject OptionButton;
    //�p���[�A�b�v
    public GameObject PowerUpImage;
    private bool hasPowerUpImageBeenHidden = false;



    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //playerModels
        playerModelsScript = playerModels.GetComponent<PlayerModels>();

        // Initialize Elements
        SetActiveForObjects(false, boss, bossBullet, StartUi, PushAGameOver, OptionButton, AllEnergy);
        PowerUpImage.SetActive(false); // �ʂɐ���
        SetActiveForObjects(true, TitleUi);
    }

    void Update()
    {
        // �Q�[���J�n��Ԃ̊m�F
        if (gameManagerScript != null && gameManagerScript.IsGameStart())
        {
            if (!gameManagerScript.IsGameClear() && !gameManagerScript.IsGameOver())
            {
                SetActiveForObjects(false, TitleUi, PushAGameOver, PowerUpImage);
                SetActiveForObjects(true, boss, bossBullet, StartUi, OptionButton, AllEnergy);
               
                HandleEnergyLevels();
            }
            else
            {
                Destroy(AllEnergy);
           
                AllEnergy = null;
            }

            if (gameManagerScript.GetHealBatteryEnargy() <= 2 && !gameManagerScript.IsGameClear() && !gameManagerScript.IsGameOver())
            {
                EnergyMIN.SetActive(false);
                EnergyMID.SetActive(false);
                EnergyMAX.SetActive(false);
            }
        }

        // �Z���N�^�̏�Ԋm�F
        if (gameManagerScript.IsOpenSelector())
        {
            SetActiveForObjects(true, Selector); // Selector�摜��L����
        }
        else
        {
            SetActiveForObjects(false, Selector); // �Z���N�^�������ꍇ�͔�\��
        }

        //��UI���W�b�N/////////////////////////////////////////////////////////////////////////////////////////////////
        if (playerModelsScript.IsIndex() == 0 || playerModelsScript.IsIndex() == 1 || playerModelsScript.IsIndex() == 2)
        {
            int healBatteryEnergy = gameManagerScript.GetHealBatteryEnargy();

            // �G�l���M�[��Ԃ�؂�ւ�
            UpdateEnergyState(healBatteryEnergy);


        }
        // �Q�[���̏I��/�N���A����
        HandleGameOver(); // GameOver���W�b�N
        HandleGameClear(); // GameClear���W�b�N
    }

    void UpdateEnergyState(int healBatteryEnergy)
    {
        if (!gameManagerScript.IsGameClear() && !gameManagerScript.IsGameOver())
        {
            // �G�l���M�[UI�̏�Ԃ��Ǘ�
            EnergyMIN.SetActive(healBatteryEnergy >= 3);
            EnergyMID.SetActive(healBatteryEnergy >= 6);
            EnergyMAX.SetActive(healBatteryEnergy >= 9);
        }

    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    private void HandleGameOver()
    {
        if (gameManagerScript.IsGameOver())
        {
            // GameOver��ʂ�\��
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX, PowerUpImage);
            SetActiveForObjects(true, PushAGameOver);


        }
    }

    private void HandleGameClear()
    {
        if (gameManagerScript.IsGameClear())
        {
            // GameClear��ʂ�\��
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX, PowerUpImage);

        }
    }

    private IEnumerator HidePowerUpImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PowerUpImage.SetActive(false);
        hasPowerUpImageBeenHidden = true; // ��\���ɂ��ꂽ���Ƃ��L�^
    }


    private void HandleEnergyLevels() // �G�l���M�[�Ǘ�
    {
        if (gameManagerScript.IsGameStart())
        {

            //���@�^�C�v�P����
            if (playerModelsScript.IsIndex() == 0)
            {

                if (gameManagerScript.GetBatteryEnargy() >= 25 && !hasPowerUpImageBeenHidden)
                {

                    PowerUpImage.SetActive(true);
                    // PowerUpImage��2.0�b��ɔ�\���ɂ���R���[�`�����J�n
                    StartCoroutine(HidePowerUpImageAfterDelay(2.0f));

                }
            }

            //���@�^�C�v���[�U�[��
            if (playerModelsScript.IsIndex() == 1)
            {

                if (gameManagerScript.GetBatteryEnargy() >= 20 && !hasPowerUpImageBeenHidden)
                {

                    PowerUpImage.SetActive(true);
                    // PowerUpImage��2.0�b��ɔ�\���ɂ���R���[�`�����J�n
                    StartCoroutine(HidePowerUpImageAfterDelay(2.0f));

                }
            }

            //���@�^�C�v�ђʒe��
            if (playerModelsScript.IsIndex() == 2)
            {

                if (gameManagerScript.GetBatteryEnargy() >= 30 && !hasPowerUpImageBeenHidden)
                {

                    PowerUpImage.SetActive(true);
                    // PowerUpImage��2.0�b��ɔ�\���ɂ���R���[�`�����J�n
                    StartCoroutine(HidePowerUpImageAfterDelay(2.0f));

                }
            }

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
