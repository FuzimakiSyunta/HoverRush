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
        isCameraSwitch = false; // 初期状態ではカメラスイッチを有効にする
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
                isCameraSwitch = true; // チュートリアル中はカメラスイッチを無効にする
            }
            else
            {
                isCameraSwitch = false; // チュートリアルが終了したらカメラスイッチを有効にする
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
