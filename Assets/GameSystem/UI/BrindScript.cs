using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrindScript : MonoBehaviour
{
    // GameManager
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
    public GameObject StartTitleUi;
    //�J�n��UI
    public GameObject StartUi;
    //Energy
    public GameObject AllEnergy;
    public GameObject EnergyMIN;
    public GameObject EnergyMID;
    public GameObject EnergyMAX;
    //Selector
    public GameObject Selector;
    //�ŏ��̃^�C�g��
    public GameObject TitleUi;
    //�Q�[���I�[�o�[
    public GameObject GameOver;
    //�ݒ�{�^��
    public GameObject OptionButton;
    //�p���[�A�b�v
    public GameObject PowerUpImage;
    private bool hasPowerUpImageBeenHidden = false;
    //�񕜉\
    public GameObject HealOkImage;
    private bool hasHealOkImageBeenHidden = false;


    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //playerModels
        playerModelsScript = playerModels.GetComponent<PlayerModels>();

        // Initialize Elements
        SetActiveForObjects(false, boss, bossBullet, StartUi, GameOver, OptionButton, AllEnergy);
        PowerUpImage.SetActive(false); // �ʂɐ���
        HealOkImage.SetActive(false); // �ʂɐ���
        SetActiveForObjects(true, StartTitleUi);
    }

    void Update()
    {
        // �Q�[���J�n��Ԃ̊m�F
        if (gameManagerScript.IsGameStart())
        {
            if (!gameManagerScript.IsGameClear() && !gameManagerScript.IsGameOver())
            {
                SetActiveForObjects(false, StartTitleUi,GameOver,PowerUpImage,HealOkImage);
                SetActiveForObjects(true, boss, bossBullet, StartUi, OptionButton, AllEnergy);
                HandleEnergyLevels(); // �G�l���M�[�Ǘ�
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


        // �Q�[���̏I��/�N���A����
        HandleGameOver(); // GameOver���W�b�N
        HandleGameClear(); // GameClear���W�b�N
    }


    private void HandleGameOver()
    {
        if (gameManagerScript.IsGameOver())
        {
            // GameOver��ʂ�\��
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX,PowerUpImage,HealOkImage);
            SetActiveForObjects(true, GameOver);

            
        }
    }

    private void HandleGameClear()
    {
        if (gameManagerScript.IsGameClear())
        {
            // GameClear��ʂ�\��
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX,PowerUpImage, HealOkImage);

        }
    }

    private IEnumerator HidePowerUpImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PowerUpImage.SetActive(false);
        hasPowerUpImageBeenHidden = true; // ��\���ɂ��ꂽ���Ƃ��L�^
    }

    private IEnumerator HideHealOkImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HealOkImage.SetActive(false);
        hasHealOkImageBeenHidden = true; // ��\���ɂ��ꂽ���Ƃ��L�^
    }

    private void HandleEnergyLevels() // �G�l���M�[�Ǘ�
    {
        if (gameManagerScript.IsGameStart())
        {
            //���@�^�C�v�P����
            if (playerModelsScript.IsIndex() == 0)
            {
                if (gameManagerScript.IsBatteryEnargy() >= 5) EnergyMIN.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 10) EnergyMID.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 15 && !hasPowerUpImageBeenHidden&&!hasHealOkImageBeenHidden)
                {
                    EnergyMAX.SetActive(true);
                    PowerUpImage.SetActive(true);
                    HealOkImage.SetActive(true);
                    // PowerUpImage��2.0�b��ɔ�\���ɂ���R���[�`�����J�n
                    StartCoroutine(HidePowerUpImageAfterDelay(2.0f));
                    // HealOkImage��2.0�b��ɔ�\���ɂ���R���[�`�����J�n
                    StartCoroutine(HideHealOkImageAfterDelay(2.0f));
                }
            }
            
            //���@�^�C�v���[�U�[��
            if (playerModelsScript.IsIndex() == 1)
            {
                if (gameManagerScript.IsBatteryEnargy() >= 5) EnergyMIN.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 10) EnergyMID.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 15&&!hasHealOkImageBeenHidden)
                {
                    EnergyMAX.SetActive(true);
                    HealOkImage.SetActive(true);
                    // HealOkImage��2.0�b��ɔ�\���ɂ���R���[�`�����J�n
                    StartCoroutine(HideHealOkImageAfterDelay(2.0f));
                }
                if (gameManagerScript.IsBatteryEnargy() >= 20 && !hasPowerUpImageBeenHidden)
                {

                    PowerUpImage.SetActive(true);
                    // PowerUpImage��2.0�b��ɔ�\���ɂ���R���[�`�����J�n
                    StartCoroutine(HidePowerUpImageAfterDelay(2.0f));

                }
            }

            //���@�^�C�v�ђʒe��
            if (playerModelsScript.IsIndex() == 2)
            {
                if (gameManagerScript.IsBatteryEnargy() >= 5) EnergyMIN.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 10) EnergyMID.SetActive(true);
                if (gameManagerScript.IsBatteryEnargy() >= 15 && !hasHealOkImageBeenHidden)
                {
                    EnergyMAX.SetActive(true);
                    HealOkImage.SetActive(true);
                    // HealOkImage��2.0�b��ɔ�\���ɂ���R���[�`�����J�n
                    StartCoroutine(HideHealOkImageAfterDelay(2.0f));
                }
                if (gameManagerScript.IsBatteryEnargy() >= 30 && !hasPowerUpImageBeenHidden)
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
