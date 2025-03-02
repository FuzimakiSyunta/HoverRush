using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;

    private float MoveSpeedY = 0.05f;
    private float MoveSpeedZ = 0.05f;

    private Vector3 pos;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        pos.y = 3.95f;
        pos.z = -1.81f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsBossWaveCount() >= 80 && gameManagerScript.IsBossWaveCount() <= 101)
        {
            transform.position += new Vector3(0, -MoveSpeedY, MoveSpeedZ);
            if (transform.position.y <= 0.1f && transform.position.z >= 2.74f)
            {
                transform.position = new Vector3(0, 0.1f, 2.74f);
            }
        }
        else
        {
            transform.position += new Vector3(0, MoveSpeedY, -MoveSpeedZ);
            if (transform.position.y >= 1.95f&& transform.position.z <= -1.81f)
            {
                transform.position = new Vector3(0, pos.y, pos.z);
            }
        }
    }
   
}
