using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialCameraMove : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;

    // Tutorial
    private TutorialManager tutorialManagerScript;
    public GameObject tutorialManager;
    private Animator animator;

    private bool isAnimation=false;

    //Swich
    private bool isCameraSwitch = false;

    private void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        tutorialManagerScript = tutorialManager.GetComponent<TutorialManager>();
        animator = GetComponent<Animator>();
        animator.SetBool("isTutorial", false);
        isCameraSwitch = false; // ������Ԃł̓J�����X�C�b�`��L���ɂ���
    }

    void Update()
    {
        if (!gameManagerScript.IsGameOver()&&!gameManagerScript.IsGameClear())
        {
            bool newIsAnimation = false;

            

            if (tutorialManagerScript.IsTutorialOpen())
            {
                animator.SetBool("isTutorial", true);

            }
            else
            {
                animator.SetBool("isTutorial", false);
            }

            if (!tutorialManagerScript.IsTutorialCheckOpen())
            {
                isCameraSwitch = true; // �`���[�g���A�����̓J�����X�C�b�`�𖳌��ɂ���
            }
            else
            {
                isCameraSwitch = false; // �`���[�g���A�����I��������J�����X�C�b�`��L���ɂ���
            }
            isAnimation = newIsAnimation;
        }
        else
        {
            
            animator.SetBool("isTutorial", false);
        }
    }


    public bool IsAnimation()
    {
        return isAnimation;
    }

    public bool IsCameraSwitch()
    {
        return isCameraSwitch;
    }
}
