using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

using TMPro;
//using TMPro;


public class GameManager : MonoBehaviour
{
    //��v�I�u�W�F�N�g
    public GameObject enemy;
    public GameObject mineEnemy;
    public GameObject planeEnemy;
    public GameObject hevyplaneEnemy;
    public GameObject blueplaneEnemy;
    public GameObject player;
    public GameObject UI;
    //text&Image
    public GameObject gameOverText;
    public GameObject gameClearText;
    public GameObject titleText;
    public GameObject StartButtonImage;

    //Select
    private bool OpenSelector = false;
    
    //�Q�[���V�X�e��
    private float[] CoolTime = new float[5];
    private bool GameOverFlag = false;
    private bool GameClearFlag = false;
    private bool GameStartFlag = false;
    private int score = 0;

    //WAVE
    public int Wave;
    public float BossWaveCount;
    private bool BossWaveFlag;
    private int waveModifier;
    

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i<5;i++)
        {
            CoolTime[i] = 0;
        }
        titleText.SetActive(true);
        StartButtonImage.SetActive(true);
        Wave = 0;
        BossWaveCount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //�Z���N�g
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
        if(GameClearFlag==true)
        {
            UI.SetActive(false);
        }else
        {
            UI.SetActive(true);
        }

            
        if (GameStartFlag == true)
        {
            BossWaveCount += Time.deltaTime;
            
            if (BossWaveCount>=18&&BossWaveCount <= 40)
            {
                BossWaveFlag = true;
                
            }
            if (BossWaveCount >= 40 && BossWaveCount < 60)
            {
                BossWaveFlag = false;
                Wave = 1;
               
            }
            if (BossWaveCount >= 58 && BossWaveCount < 80)
            {
                BossWaveFlag = true;
               
            }
            if (BossWaveCount >= 80 && BossWaveCount < 125)
            {
                BossWaveFlag = false;
                Wave = 2;
               
            }
            
        }
        
    }
    private void FixedUpdate()//�G�o��
    {
        if (GameOverFlag == true) return;
        if (GameClearFlag == true) return;

        if (GameStartFlag == true&&BossWaveFlag==false) // �Q�[�����J�n���Ă���{�X�E�F�[�u����Ȃ��Ƃ�
        {
            int RandomEnemy = Random.Range(0, 15000); // �G�̏o���p�x�������_���Ɍ���
            int Style = Random.Range(0, 5); // �G�̎�ނ������_���Ɍ���

            // ���݂�WAVE�ɂ���Ċm���͈͂�ύX
            if (Wave == 0)
            {
                waveModifier = 1;
            }
            if (Wave == 1)
            {
                waveModifier = 2; 
            }
            else if (Wave == 2)
            {
                waveModifier = 3; 
            }

            // �o�������i�͈́j��ݒ�
            int[][] Enemyranges = {
        new int[]{0, 300 * waveModifier},       // WAVE�ɉ������o���͈�
        new int[]{100, 150 * waveModifier},   // �͈�2
        new int[]{150, 300 * waveModifier},   // �͈�3
        //new int[]{6000, 6300 * waveModifier},   // �͈�4
        //new int[]{7000, 7100 * waveModifier}    // �͈�5

    };

            // �eCoolTime�i�G���ēx�o������܂ł̑ҋ@���ԁj���J�E���g�A�b�v
            for (int i = 0; i < CoolTime.Length; i++)
            {
                CoolTime[i]+=Time.deltaTime;
            }

            // �@�̂̓G�̏����ʒu�i�ʏ���W�j
            Vector3[] positions = {
        new Vector3(-8.0f, 1.5f, 45.0f), // ���[
        new Vector3(0.0f, 1.5f, 45.0f),  // ����
        new Vector3(8.0f, 1.5f, 45.0f),  // �E�[
        new Vector3(4.0f, 1.5f, 45.0f),  // �����E
        new Vector3(-4.0f, 1.5f, 45.0f)  // ������
    };

            // 覐Ό^�̓G��p�̏����ʒu�i�㕔�j
            Vector3[] MeteoPositions = {
        new Vector3(-8.0f, 8.0f, 45.0f), // ���[��
        new Vector3(0.0f, 8.0f, 45.0f),  // ������
        new Vector3(8.0f, 8.0f, 45.0f),  // �E�[��
        new Vector3(4.0f, 8.0f, 45.0f),  // �����E��
        new Vector3(-4.0f, 8.0f, 45.0f)  // ��������
    };

            // �o���͈͂ƓG�̏������ƂɓG�𐶐�
            for (int i = 0; i < Enemyranges.Length; i++)
            {
                // �����_���l�����݂͈̔͂Ɋ܂܂�邩�`�F�b�N
                if (RandomEnemy >= Enemyranges[i][0] && RandomEnemy <= Enemyranges[i][1])
                {
                    // CoolTime�����l�ȏ�Ȃ�G�𐶐�
                    if (CoolTime[i] >= 0.5f)
                    {
                        // �U���\�ȓG�𐶐�
                        if (Style == 0 && Wave >= 1)
                        {
                            Instantiate(enemy, positions[i], Quaternion.identity); // �G�𐶐�
                        }
                        // 覐Ό^�̓G�𐶐�
                        else if (Style == 1)
                        {
                            Instantiate(mineEnemy, MeteoPositions[i], Quaternion.identity); // �n���G�𐶐�
                        }
                        // �@�̂̓G�𐶐�
                        else if (Style == 2)
                        {
                            Instantiate(planeEnemy, positions[i], Quaternion.identity); // ���F�G�𐶐�
                        }
                        // �@�̂̓G�𐶐�
                        else if (Style == 3 && Wave >= 1)
                        {
                            Instantiate(hevyplaneEnemy, positions[i], Quaternion.identity); // �d���G�𐶐�
                        }
                        // �@�̂̓G�𐶐�
                        else if (Style == 4 && Wave >= 2)
                        {
                            Instantiate(blueplaneEnemy, positions[i], Quaternion.identity); // ���G�𐶐�
                        }

                        // CoolTime�����Z�b�g
                        CoolTime[i] = 0;
                    }
                }
            }
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
    public void Score()//�X�R�A
    {
        score += 1;
    }
    public int IsScore()
    {
        return score;
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
    public bool IsBossWaveStart()
    {
        return BossWaveFlag;
    }
    public float IsBossWaveCount()
    {
        return BossWaveCount;
    }
}
