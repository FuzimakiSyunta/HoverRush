using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;

    // Tutorial
    private TutorialManager tutorialManagerScript;
    public GameObject tutorialManager;
    private Animator animator;

    private bool isAnimation=false;
    private bool wasAnimation = false; // �O��̏��

    private void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        tutorialManagerScript = tutorialManager.GetComponent<TutorialManager>();
        animator = GetComponent<Animator>();
        // ������Ԃ̃A�j���[�V������ݒ�
        animator.SetBool("isRobotView", false);
        animator.SetBool("isBossBulletView", false);
        animator.SetBool("isLazerBossView", false);
        animator.SetBool("isFinalBattle", false);
        animator.SetBool("isTutorial", false);
    }

    void Update()
    {
        if (!gameManagerScript.IsGameOver()&&!gameManagerScript.IsGameClear())
        {
            bool newIsAnimation = false;

            // �e�A�j���[�V�����`�F�b�N�itrue �ɂ���^�C�~���O�j
            if (gameManagerScript.IsGamePlayCount() >= 77.0f && gameManagerScript.IsGamePlayCount() <= 101.0f)
            {
                animator.SetBool("isRobotView", true);
                newIsAnimation = true;
            }
            else
            {
                animator.SetBool("isRobotView", false);
            }

            if (gameManagerScript.IsBossWave() && gameManagerScript.IsGamePlayCount() >= 18.0f && gameManagerScript.IsGamePlayCount() <= 58.0f)
            {
                animator.SetBool("isBossBulletView", true);
                newIsAnimation = true;
            }
            else
            {
                animator.SetBool("isBossBulletView", false);
            }

            if (gameManagerScript.IsBossWave() && gameManagerScript.IsGamePlayCount() >= 58.0f && gameManagerScript.IsGamePlayCount() < 77.0f)
            {
                animator.SetBool("isLazerBossView", true);
                newIsAnimation = true;
            }
            else
            {
                animator.SetBool("isLazerBossView", false);
            }

            if (gameManagerScript.IsBossWave() && gameManagerScript.IsGamePlayCount() >= 124.0f)
            {
                animator.SetBool("isFinalBattle", true);
                newIsAnimation = true;
            }
            else
            {
                animator.SetBool("isFinalBattle", false);
            }

            if (tutorialManagerScript.IsTutorialOpen())
            {
                animator.SetBool("isTutorial", true);
            }
            else
            {
                animator.SetBool("isTutorial", false);
            }

            //// �A�j���[�V������Ԃ� false��true �ɂȂ����u�Ԃ����m
            //if (!wasAnimation && newIsAnimation)
            //{
            //    StartCoroutine(PauseGameForSeconds(2f));
            //}

            wasAnimation = newIsAnimation;
            isAnimation = newIsAnimation;
        }
        else
        {
            animator.SetBool("isRobotView", false);
            animator.SetBool("isBossBulletView", false);
            animator.SetBool("isLazerBossView", false);
            animator.SetBool("isGameOver", true);
            animator.SetBool("isTutorial", false);
        }
    }


    IEnumerator PauseGameForSeconds(float seconds)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(seconds); // Realtime�őҋ@
        Time.timeScale = 1f;
    }

    public bool IsAnimation()
    {
        return isAnimation;
    }

}
