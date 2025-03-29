using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrindScript : MonoBehaviour
{
    // GameManager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    // Boss
    public GameObject boss;
    public GameObject bossBullet;
    //public GameObject RobotBullet;
    //�J�n�OUI
    public GameObject StartTitleUi;
    //�J�n��UI
    public GameObject StartUi;
    //GameOver&GameClear
    public GameObject ClearGameOverUI;
    //Energy
    public GameObject AllEnergy;
    public GameObject EnergyMIN;
    public GameObject EnergyMID;
    public GameObject EnergyMAX;
    //Selector
    public GameObject Selector;
    //�ŏ��̃^�C�g��
    public GameObject TitleUi;
    //
    public GameObject GameClear;
    public GameObject GameOver;
    //�ݒ�{�^��
    public GameObject OptionButton;
    //�p���[�A�b�v
    public GameObject PowerUpImage;
    private bool hasPowerUpImageBeenHidden = false;



    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();

        // Initialize Elements
        SetActiveForObjects(false, boss, bossBullet, StartUi, GameOver, GameClear, ClearGameOverUI, OptionButton, AllEnergy);
        PowerUpImage.SetActive(false); // �ʂɐ���
        SetActiveForObjects(true, StartTitleUi);
    }

    void Update()
    {
        // �Q�[���J�n��Ԃ̊m�F
        if (gameManagerScript.IsGameStart())
        {
            if (!gameManagerScript.IsGameClear() && !gameManagerScript.IsGameOver())
            {
                SetActiveForObjects(false, StartTitleUi, ClearGameOverUI,PowerUpImage);
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
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX,PowerUpImage);
            SetActiveForObjects(true, GameOver);

            
        }
    }

    private void HandleGameClear()
    {
        if (gameManagerScript.IsGameClear())
        {
            // GameClear��ʂ�\��
            SetActiveForObjects(false, boss, bossBullet, StartUi, EnergyMIN, EnergyMID, EnergyMAX,PowerUpImage);
            SetActiveForObjects(true, GameClear);

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
            if (gameManagerScript.IsScore() >= 5) EnergyMIN.SetActive(true);
            if (gameManagerScript.IsScore() >= 10) EnergyMID.SetActive(true);
            if (gameManagerScript.IsScore() >= 15 && !hasPowerUpImageBeenHidden)
            {
                EnergyMAX.SetActive(true);
                PowerUpImage.SetActive(true);
                // PowerUpImage��2.0�b��ɔ�\���ɂ���R���[�`�����J�n
                StartCoroutine(HidePowerUpImageAfterDelay(2.0f));

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
