using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBlinking : MonoBehaviour
{
    public Image[] img; // Image �z��
    float duration = 1.0f;
    //�J�n���̐F
    Color32 startColor = new Color32(255, 255, 255, 255);

    //�I�����̐F
    Color32 endColor = new Color32(255, 255, 255, 64);

    void Awake()
    {
        // img �� null �`�F�b�N�ƃf�t�H���g�l�ݒ�́A�ȉ��̂悤�ɏC��
        if (img == null || img.Length == 0)
        {
            img = GetComponentsInChildren<Image>(); // �q�I�u�W�F�N�g���� Image �R���|�[�l���g���擾
        }
    }

    void Update()
    {
        // for ���[�v�C��: �z��̃C���f�b�N�X�͈͂�K�؂ɐ���
        for (int i = 0; i < img.Length; i++)
        {
            img[i].color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time / duration, 1.0f));
        }
    }
}