using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{
    private bool isSceneMove;
    // Start is called before the first frame update
    void Start()
    {
        isSceneMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
        {
            isSceneMove = true;
        }
        if(isSceneMove==true)
        {
            SceneManager.LoadScene("PlayerMove");
        }
    }
}
