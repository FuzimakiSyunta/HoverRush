using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI�𑀍삷�邽�߂ɕK�v

public class PlayerModels : MonoBehaviour
{
    public GameObject[] cubes; // �؂�ւ���Cube�I�u�W�F�N�g�̔z��
    public Image uiImage; // UI��Image�R���|�[�l���g
    public Sprite[] images; // �؂�ւ���摜�̔z��

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
            float currentTime = Time.time;

            if (canInput && currentTime - lastInputTime > inputCooldown)
            {
                float verticalInput = Input.GetAxis("DPadVertical");

                if (Input.GetKeyDown(KeyCode.UpArrow) || verticalInput > 0)
                {
                    currentIndex = (currentIndex + 1) % cubes.Length;
                    UpdateCubeVisibility();
                    canInput = false;
                    lastInputTime = currentTime;
                }

                if (Input.GetKeyDown(KeyCode.DownArrow) || verticalInput < 0)
                {
                    currentIndex = (currentIndex - 1 + cubes.Length) % cubes.Length;
                    UpdateCubeVisibility();
                    canInput = false;
                    lastInputTime = currentTime;
                }
            }

            if (currentTime - lastInputTime > inputCooldown)
            {
                canInput = true;
            }
        }
    }

    void UpdateCubeVisibility()
    {
        // Cube�̐؂�ւ�
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].SetActive(i == currentIndex);
        }

        // UI�̉摜�𓯊�
        if (uiImage != null && images.Length > currentIndex)
        {
            uiImage.sprite = images[currentIndex];
        }
    }

    // ���݂̃C���f�b�N�X���擾
    public int IsIndex()
    {
        return currentIndex;
    }
}
