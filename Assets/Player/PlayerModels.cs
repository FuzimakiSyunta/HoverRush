using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModels : MonoBehaviour
{
    
    public GameObject[] cubes; // �؂�ւ���Cube�I�u�W�F�N�g�̔z��
    private int currentIndex = 0; // ���ݕ\������Ă���Cube�̃C���f�b�N�X

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
            // ��L�[�Ŏ��̌����ڂɐ؂�ւ�
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentIndex = (currentIndex + 1) % cubes.Length; // �C���f�b�N�X��i�߂�
                UpdateCubeVisibility();
            }

            // ���L�[�őO�̌����ڂɐ؂�ւ�
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentIndex = (currentIndex - 1 + cubes.Length) % cubes.Length; // �C���f�b�N�X��߂�
                UpdateCubeVisibility();
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

    //�v�f
    public int IsIndex()
    {
        return currentIndex;
    }
}
