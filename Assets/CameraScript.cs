using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float MoveSpeed = 0.02f;
    private GameManager gameManagerScript;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.IsGameStart()==true)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= 10)
            {
                transform.position += new Vector3(MoveSpeed, 0, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -10)
            {
                transform.position += new Vector3(-MoveSpeed, 0, 0);
            }
            else
            {
                transform.position += new Vector3(0, 0, 0);
            }
        }else
        {
            transform.rotation = Quaternion.Euler(10f, 0f, 0f);
        }
        
    }
}
