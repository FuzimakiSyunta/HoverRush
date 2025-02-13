using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    public GameObject Score;
    public GameObject HP;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameStart() == false)
        {
            //UIÇè¡Ç∑
            Score.SetActive(false);
            HP.SetActive(false);
        }else { 
            Score.SetActive(true);
            HP.SetActive(true);
        }
    }
}
