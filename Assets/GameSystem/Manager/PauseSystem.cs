using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    private bool isPaused = false;
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)|| Input.GetKeyDown("joystick button 7") && gameManagerScript.IsGameStart()==true)
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        // �ꎞ��~���ɃQ�[���I�u�W�F�N�g���~�����
        foreach (var obj in GameObject.FindGameObjectsWithTag("Pauseable"))
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = isPaused;
            }
        }
    }
}
