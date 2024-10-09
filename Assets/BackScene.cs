using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Timer++;
        if (Timer>=120)
        {
            SceneManager.LoadScene("PlayerMove");
            Timer = 0;
        }
    }
}
