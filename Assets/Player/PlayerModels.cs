using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModels : MonoBehaviour
{
    public GameObject[] cubes; // �؂�ւ���Cube�I�u�W�F�N�g�̔z��
    private int currentIndex = 0; // ���ݕ\������Ă���Cube�̃C���f�b�N�X
    private bool canInput = true;  // ���͉\���ǂ����̃t���O
    private float inputCooldown = 0.3f; // �N�[���_�E�����ԁi�b�j
    private float lastInputTime = 0f; // �Ō�ɓ��͂��󂯕t��������

    //select
    private SelectorMenu selectorMenuScript;
    public GameObject selectMenu;

    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    void Start()
    {
        //selector
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        UpdateCubeVisibility();
    }

    void Update()
    {
        if (selectorMenuScript.IsColorMenuFlag() == true && gameManagerScript.IsGameStart() == false)
        {
            // ���݂̎��Ԃ��擾
            float currentTime = Time.time;

            // �N�[���_�E���m�F
            if (canInput && currentTime - lastInputTime > inputCooldown)
            {
                // �㉺�̓��͂��󂯎��
                float verticalInput = Input.GetAxis("DPadVertical");

                // ��L�[�Ŏ��̌����ڂɐ؂�ւ�
                if (Input.GetKeyDown(KeyCode.UpArrow) || verticalInput > 0)
                {
                    currentIndex = (currentIndex + 1) % cubes.Length; // �C���f�b�N�X��i�߂�
                    UpdateCubeVisibility();
                    canInput = false; // �N�[���_�E���J�n
                    lastInputTime = currentTime; // �Ō�̓��͎��Ԃ��X�V
                }

                // ���L�[�őO�̌����ڂɐ؂�ւ�
                if (Input.GetKeyDown(KeyCode.DownArrow) || verticalInput < 0)
                {
                    currentIndex = (currentIndex - 1 + cubes.Length) % cubes.Length; // �C���f�b�N�X��߂�
                    UpdateCubeVisibility();
                    canInput = false; // �N�[���_�E���J�n
                    lastInputTime = currentTime; // �Ō�̓��͎��Ԃ��X�V
                }
            }

            // �N�[���_�E�����I��������canInput�����Z�b�g
            if (currentTime - lastInputTime > inputCooldown)
            {
                canInput = true;
            }
        }
    }

    void UpdateCubeVisibility()
    {
        // �S�Ă�Cube���\���ɂ��āA���݂�Cube�����\��
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].SetActive(i == currentIndex);
        }
    }

    // �v�f���擾����֐�
    public int IsIndex()
    {
        return currentIndex;
    }
}
