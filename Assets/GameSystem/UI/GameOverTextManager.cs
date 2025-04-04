using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTextManager : MonoBehaviour
{
    private Animator animator;
    public GameObject PushAImage;

    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    //CoolTimer
    private float PushAActiveTime;

    // Start is called before the first frame update
    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //animation
        animator = GetComponent<Animator>();
        animator.SetBool("GameOverTextActive", false);
        PushAImage.SetActive(false);
        PushAActiveTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.IsGameOver())
        {
            PushAActiveTime += Time.deltaTime;
            animator.SetBool("GameOverTextActive", true);
            if(PushAActiveTime >= 2.2f)
            {
                PushAImage.SetActive(true);
                if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
                {
                    SceneManager.LoadScene("Load");
                }
            }
            else
            {
                PushAImage.SetActive(false);
            }
            
        }
        

    }
}
