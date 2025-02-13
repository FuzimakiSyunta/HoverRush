using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControll : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    //プレイヤーを変数に格納
    public GameObject Player;
    //回転させるスピード
    public float rotateSpeed = 3.0f;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameStart() == false)
        {
            //回転させる角度
            angle = rotateSpeed;
            //プレイヤー位置情報
            Vector3 playerPos = Player.transform.position;
            //カメラを回転させる
            transform.RotateAround(playerPos, Vector3.up, angle);
        }
        
    }
}
