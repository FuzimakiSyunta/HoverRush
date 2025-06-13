using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

using TMPro;


public class GameManager : MonoBehaviour
{
    //��v�I�u�W�F�N�g
    public GameObject UI;
    //text&Image
    public GameObject gameOverText;
    public GameObject gameClearText;
    public GameObject titleText;
    public GameObject StartButtonImage;

    //Select
    private bool isSelectorOpened = false;
    
    //�Q�[���V�X�e��
    private bool GameOverFlag = false;
    private bool GameClearFlag = false;
    private bool GameStartFlag = false;
    public int batteryEnergy = 0;//�����p
    public int healBatteryEnergy = 0;//�񕜗p
    private int healcount = 5;//�񕜉�

    //WAVE
    public int Wave;
    public float GamePlayCount;
    private bool BossWaveFlag;

    // WAVE��
    private const int MaxWave = 4;

    // �eWAVE���n�܂�����
    private bool[] waveStarted = new bool[MaxWave];
    // �eWAVE�̃{�X�t�F�[�Y���n�܂�����
    private bool[] bossStarted = new bool[MaxWave];
    // �ʏ�WAVE�̊J�n����
    private float[] waveStartTimes = { 0f, 40f, 80f, 125f };

    // �{�XWAVE�̊J�n����
    private float[] bossStartTimes = { 18f, 58f, -1f, 125f }; // -1 �� Wave2 �Ƀ{�X���Ȃ��Ɖ���



    //�Q�[���J�n���o��
    public GameObject SpeedParticle;

    // Start is called before the first frame update
    void Start()
    {
        titleText.SetActive(true);
        StartButtonImage.SetActive(true);
        Wave = 0;
        GamePlayCount = 0;
        SpeedParticle.SetActive(false);
        healcount = 5;
    }

    // Update is called once per frame
    void Update()
    {

        //�Z���N�g
        OffSelect();


        //UI�S��
        Allui();

        //�񕜊Ǘ�
        HwalManager();


        //�E�F�[�u�Ǘ�
        WaveManager();

        if (batteryEnergy <= 0)
        {
            batteryEnergy = 0; // �G�l���M�[��0�ȉ��ɂȂ�Ȃ��悤�ɂ���
        }
    }

    void WaveManager()
    {
        if (GameStartFlag)
        {
            GamePlayCount += Time.deltaTime;
            SpeedParticle.SetActive(true);

            for (int i = 0; i < MaxWave; i++)
            {
                // �ʏ�WAVE�J�n����
                if (!waveStarted[i] && GamePlayCount >= waveStartTimes[i])
                {
                    Wave = i;
                    BossWaveFlag = false;
                    waveStarted[i] = true;
                    Debug.Log($"Wave {i} Started");
                }

                // �{�XWAVE�J�n����i�L���Ȏ��Ԃ̂݁j
                if (!bossStarted[i] && bossStartTimes[i] > 0 && GamePlayCount >= bossStartTimes[i])
                {
                    BossWaveFlag = true;
                    bossStarted[i] = true;
                    Debug.Log($"Wave {i} Boss Started");
                }
            }
        }
        else
        {
            SpeedParticle.SetActive(false);
        }
    }

    void OffSelect()
    {
        if (isSelectorOpened == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                isSelectorOpened = true;
                titleText.SetActive(false);
                StartButtonImage.SetActive(false);
                GameStartFlag = false;

            }
        }
    }

    void Allui()
    {
        //UI�S��///////////////
        if (GameClearFlag == true)
        {
            UI.SetActive(false);
        }
        else
        {
            UI.SetActive(true);
        }
    }

    void HwalManager()
    {
        if (healcount <= 0)
        {
            HealBatteryEnargyReset();
        }
    }
   
    public void GameOverStart()//�Q�[���I�[�o�[
    {
        GameOverFlag = true;
        gameOverText.SetActive(true);
    }
    public bool IsGameOver()
    {
        return GameOverFlag;
    }
    public void GameClearStart()//�Q�[���N���A
    {
        GameClearFlag = true;
        gameClearText.SetActive(true);
    }
    public bool IsGameClear()
    {
        return GameClearFlag;//�N���A�̃t���O
    }
    public void BatteryEnargyUp()
    {
        batteryEnergy += 1; // �G�l���M�[�𑝉�
    }
    public void BatteryEnargyDown()
    {
        batteryEnergy -= 2; // �G�l���M�[������
    }

    public int GetBatteryEnargy()
    {
        return batteryEnergy; // ���݂̃G�l���M�[�l��Ԃ�
    }
    public void HealBatteryEnargyReset()
    {
        healBatteryEnergy = 0; // �o�b�e���[�����Z�b�g
    }
    public void HealBatteryEnargyUp()
    {
        healBatteryEnergy += 1; // �o�b�e���[�𑝉�
    }
    public void ShieldBatteryEnargy()
    {
        batteryEnergy -= 30; // �o�b�e���[��-30
    }
    public int GetHealBatteryEnargy()
    {
        return healBatteryEnergy; // ���݂̃G�l���M�[�l��Ԃ�
    }
    public void HealCounter()
    {
        healcount -= 1;//�񕜃J�E���g
    }
    public int HealCount()
    {
        return healcount;
    }
    public void GameStart()//�Q�[���X�^�[�g
    {
        GameStartFlag = true;
    }
    public bool IsGameStart()
    {
        return GameStartFlag;
    }
    public bool IsOpenSelector()
    {
        return isSelectorOpened;
    }
    public bool IsBossWave()
    {
        return BossWaveFlag;
    }
    public float IsGamePlayCount()
    {
        return GamePlayCount;
    }

    public void BossWaveCountStart()
    {
        GamePlayCount++;
    }

    public int IsWave()
    {
        return Wave;
    }
    public float[] GetBossStartTimes()
    {
        return bossStartTimes;
    }

}
