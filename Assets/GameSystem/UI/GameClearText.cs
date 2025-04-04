using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearText : MonoBehaviour
{
    private Animator animator;

    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;


    // Start is called before the first frame update
    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //animation
        animator = GetComponent<Animator>();
        animator.SetBool("GAMECLEAR", false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameClear())
        {
            animator.SetBool("GAMECLEAR", true);
        }


    }
}
