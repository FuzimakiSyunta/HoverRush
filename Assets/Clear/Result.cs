using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject gameTimer;
    private GameTimer gameTimerScript;

    public GameObject gameManager;
    private GameManager gameManagerScript;

    public GameObject isSrank;
    public GameObject isArank;
    public GameObject isBrank;

    // Start is called before the first frame update
    void Start()
    {
        gameTimerScript = gameTimer.GetComponent<GameTimer>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        isArank.SetActive(false);
        isBrank.SetActive(false);
        isSrank.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameClear())
        {
            gameTimerScript.IsStopTime();
            if (gameTimerScript.IsGameTime() <= 3.0f)
            {
                isSrank.SetActive(true);
                isArank.SetActive(false);
                isBrank.SetActive(false);
            }
            else if (gameTimerScript.IsGameTime() <= 5.0f)
            {
                isSrank.SetActive(false);
                isArank.SetActive(true);
                isBrank.SetActive(false);
            }
            else if (gameTimerScript.IsGameTime() <= 7.0f)
            {
                isSrank.SetActive(false);
                isArank.SetActive(false);
                isBrank.SetActive(true);
            }
        }
        else
        {
            gameTimerScript.IsStartTime();
        }
        
    }
}
