using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOkMove : MonoBehaviour
{
    public GameObject gameManager; // GameManager�I�u�W�F�N�g
    private GameManager gameManagerScript; // GameManager�̃X�N���v�g
    private Animator healOkAnimation; // HealOk�̃A�j���[�V�����R���|�[�l���g

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        healOkAnimation = GetComponent<Animator>(); // Animator
    }

    // Update is called once per frame
    void Update()
    {
        PlayHealOkAnimation();
    }

    // HealOk�̃A�j���[�V�������Đ����郁�\�b�h
    private void PlayHealOkAnimation()
    {
        int healBatteryEnergy = gameManagerScript.GetHealBatteryEnargy();
        if (healBatteryEnergy >= 9)
        {
            healOkAnimation.SetBool("isHealOk", true);  // HealOk�̃A�j���[�V�������Đ�
        }
        else
        {
            healOkAnimation.SetBool("isHealOk",false); 
        }
    }
}
