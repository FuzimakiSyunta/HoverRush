using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        animator.SetBool("isRobotView", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.IsGamePlayCount() >= 77.0f&& gameManagerScript.IsGamePlayCount() <= 101.0f)
        {
            animator.SetBool("isRobotView", true);
        }else
        {
            animator.SetBool("isRobotView", false);
        }


        
    }
   
}
