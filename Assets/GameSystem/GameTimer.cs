using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gameManagerScript;
    private float gameTime;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        gameTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.IsGameStart())
        {
            gameTime += Time.deltaTime;
        }
    }

    public float IsGameTime()
    {
        return gameTime;
    }
}
