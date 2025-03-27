using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gameManagerScript;
    private float gameTime;
    private float customDeltaTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        gameTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        customDeltaTime = Time.deltaTime;

        if (gameManagerScript.IsGameStart()&&gameManagerScript.IsGameClear()==false&&gameManagerScript.IsGameOver()==false)
        {
            gameTime += customDeltaTime;
        }
        
    }

    public float IsGameTime()
    {
        return gameTime;
    }

    public void IsStopTime()
    {
        customDeltaTime = 0f;
    }

    public void IsStartTime()
    {
        customDeltaTime = 1f;
    }
}
