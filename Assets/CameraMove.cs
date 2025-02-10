using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    private float MoveSpeed = 0.08f;
    private bool Moving;
    private GameManager gameManagerScript;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        Moving = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float stick = Input.GetAxis("Horizontal");
        float Vstick = Input.GetAxis("Vertical");
        if (gameManagerScript.IsLuleStart()==true&&Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown("joystick button 0"))
        {
            Moving = true;
        }
        if(Moving==true)
        {
            if (gameManagerScript.IsGameClear() == false&&gameManagerScript.IsGameOver()==false)
            {
                if (stick > 0 && transform.position.x <= 10)
                {
                    transform.position += new Vector3(MoveSpeed, 0, 0);
                }
                else if (stick < 0 && transform.position.x >= -10)
                {
                    transform.position += new Vector3(-MoveSpeed, 0, 0);
                }
                //キーボード
                if (Input.GetKey(KeyCode.D) && transform.position.x <= 10)
                {
                    transform.position += new Vector3(MoveSpeed, 0, 0);
                }
                else if (Input.GetKey(KeyCode.A) && transform.position.x >= -10)
                {
                    transform.position += new Vector3(-MoveSpeed, 0, 0);
                }
            }
        }
    }
   
}
