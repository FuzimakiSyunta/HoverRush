using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModels : MonoBehaviour
{
    
    public GameObject[] cubes; // �؂�ւ���Cube�I�u�W�F�N�g�̔z��
    private int currentIndex = 0; // ���ݕ\������Ă���Cube�̃C���f�b�N�X

    void Start()
    {
        UpdateCubeVisibility();
    }

    void Update()
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

    void UpdateCubeVisibility()
    {
        // �S�Ă�Cube���\���ɂ��āA���݂�Cube�����\��
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].SetActive(i == currentIndex);
        }
    }
}
