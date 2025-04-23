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
    private bool OpenSelector = false;
    
    //�Q�[���V�X�e��
    private bool GameOverFlag = false;
    private bool GameClearFlag = false;
    private bool GameStartFlag = false;
    public int batteryEnargy = 0;//�����p
    public int healBatteryEnargy = 0;//�񕜗p
    private int healcount = 5;//�񕜉�

    //WAVE
    public int Wave;
    public float GamePlayCount;
    private bool BossWaveFlag;

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
        
        //�Z���N�g///////////////////////////////////////////////////////////////////////////////
        if (OpenSelector == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                OpenSelector = true;
                titleText.SetActive(false);
                StartButtonImage.SetActive(false);
                GameStartFlag = false;
                
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////



        //UI�S��///////////////
        if(GameClearFlag==true)
        {
            UI.SetActive(false);
        }else
        {
            UI.SetActive(true);
        }
        ///////////////////////
        


        ///�񕜊Ǘ�/////////////////////////////////////
        if(healcount<=0)
        {
            HealBatteryEnargyReset();
        }
        /////////////////////////////////////////////////////

        //�E�F�[�u�Ǘ�/////////////////////////
        if (GameStartFlag == true)
        {
            GamePlayCount += Time.deltaTime;
            SpeedParticle.SetActive(true);

            if (GamePlayCount>=18&&GamePlayCount <= 40)
            {
                BossWaveFlag = true;
            }
            if (GamePlayCount >= 40 && GamePlayCount < 60)
            {
                BossWaveFlag = false;
                Wave = 1;
            }
            if (GamePlayCount >= 58 && GamePlayCount < 80)
            {
                BossWaveFlag = true;
               
            }
            if (GamePlayCount >= 80 && GamePlayCount < 125)
            {
                BossWaveFlag = false;
                Wave = 2;
               
            }
            if (GamePlayCount >= 125)
            {
                BossWaveFlag = true;
                Wave = 3;
            }
        }else
        {
            SpeedParticle.SetActive(false);
        }
        ///////////////////////////////////////
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
        batteryEnargy += 1; // �G�l���M�[�𑝉�
    }

    public int GetBatteryEnargy()
    {
        return batteryEnargy; // ���݂̃G�l���M�[�l��Ԃ�
    }
    public void HealBatteryEnargyReset()
    {
        healBatteryEnargy = 0; // �o�b�e���[�����Z�b�g
    }
    public void HealBatteryEnargyUp()
    {
        healBatteryEnargy += 1; // �o�b�e���[�𑝉�
    }
    public int GetHealBatteryEnargy()
    {
        return healBatteryEnargy; // ���݂̃G�l���M�[�l��Ԃ�
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
        return OpenSelector;
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
}
