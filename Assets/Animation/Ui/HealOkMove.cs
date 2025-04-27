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
        if (gameManagerScript.IsGameStart() && !gameManagerScript.IsGameOver() && !gameManagerScript.IsGameClear())
        {
            healOkAnimation.SetBool("isHealOk", true);  // HealOk�̃A�j���[�V�������Đ�
        }
        else
        {
            healOkAnimation.SetBool("isHealOk",false); // �Q�[�����X�^�[�g���Ă��Ȃ��A�܂��̓Q�[���I�[�o�[�A�܂��̓Q�[���N���A�̏ꍇ�̓A�j���[�V�������~
        }
    }
}
