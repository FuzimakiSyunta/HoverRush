using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    private Animator animator;

    private bool isAnimation=false;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        animator.SetBool("isRobotView", false);
        animator.SetBool("isBossBulletView", false);
        animator.SetBool("isLazerBossView", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.IsGameOver()==false)
        {
            //���{�b�g�ɕϐg�����Ƃ��̃A�j���[�V����
            if (gameManagerScript.IsGamePlayCount() >= 77.0f && gameManagerScript.IsGamePlayCount() <= 101.0f)
            {
                animator.SetBool("isRobotView", true);
                isAnimation = true;
            }
            else
            {
                animator.SetBool("isRobotView", false);
                isAnimation = false;
            }

            //���{�X�E�F�[�u�̃A�j���[�V����
            if (gameManagerScript.IsBossWave() && gameManagerScript.IsGamePlayCount() >= 18.0f && gameManagerScript.IsGamePlayCount() <= 58.0f)
            {
                animator.SetBool("isBossBulletView", true);
                isAnimation = true;
            }
            else
            {
                animator.SetBool("isBossBulletView", false);
                isAnimation = false;
            }
            //���{�X�E�F�[�u�̃A�j���[�V����
            if (gameManagerScript.IsBossWave() && gameManagerScript.IsGamePlayCount() >= 58.0f && gameManagerScript.IsGamePlayCount() < 77.0f)
            {
                animator.SetBool("isLazerBossView", true);
                isAnimation = true;
            }
            else
            {
                animator.SetBool("isLazerBossView", false);
                isAnimation = false;
            }
        }
        else
        {
            animator.SetBool("isRobotView", false);
            animator.SetBool("isBossBulletView", false);
            animator.SetBool("isLazerBossView", false);
            animator.SetBool("isGameOver", true);
        }


    }

    public bool IsAnimation()
    {
        return isAnimation;//�A�j���[�V����������
    }
   
}
