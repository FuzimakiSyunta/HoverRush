using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    private PlayerShake playerShakeScript;
    public GameObject playerShake;

    private float MoveSpeed = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        playerShakeScript = playerShake.GetComponent < PlayerShake>();
    }

    // Update is called once per frame
    void Update()
    {
        //L Stick
        float stick = Input.GetAxis("Horizontal");
        float Vstick = Input.GetAxis("Vertical");
        if (gameManagerScript.IsGameStart() == true && gameManagerScript.IsGameClear() == false&& playerShakeScript.IsShaking() ==false)
        {
            // 時間依存の移動
            float move = MoveSpeed * Time.deltaTime;
            //キーボード/////////////////////////////////
            if (Input.GetKey(KeyCode.D) && transform.position.x <= 10)
            {
                transform.position += new Vector3(move, 0, 0);
            }
            else if (Input.GetKey(KeyCode.A) && transform.position.x >= -10)
            {
                transform.position += new Vector3(-move, 0, 0);
            }
            if (Input.GetKey(KeyCode.W) && transform.position.z <= 20)
            {
                transform.position += new Vector3(0, 0, move);
            }
            else if (Input.GetKey(KeyCode.S) && transform.position.z >= -6.5f)
            {
                transform.position += new Vector3(0, 0, -move);
            }
            //////////////////////////////////////////////
            
            ///コントローラー対応///////////////////////
            if (stick > 0 && transform.position.x <= 10)
            {
                transform.position += new Vector3(move, 0, 0);
            }
            else if (stick < 0 && transform.position.x >= -10)
            {
                transform.position += new Vector3(-move, 0, 0);
            }
            if (Vstick > 0 && transform.position.z <= 20)
            {
                transform.position += new Vector3(0, 0, move);
            }
            else if (Vstick < 0 && transform.position.z >= -6.5f)
            {
                transform.position += new Vector3(0, 0, -move);
            }
        }

    }
}
