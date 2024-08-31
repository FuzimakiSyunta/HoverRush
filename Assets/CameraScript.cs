using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera mainCamera;
    public Camera subCamera;
    private GameManager gameManagerScript;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        mainCamera = Camera.main;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            mainCamera.enabled = false;
            subCamera.enabled = true;
        }
        if(gameManagerScript.IsGameOver()==true)
        {
            mainCamera.enabled = true;
            subCamera.enabled = false;
        }
    }
}
