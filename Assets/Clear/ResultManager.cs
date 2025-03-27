using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public GameObject gameTimer;
    private GameTimer gameTimerrScript;

    // Start is called before the first frame update
    void Start()
    {
        gameTimerrScript = gameTimer.GetComponent<GameTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
