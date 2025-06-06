using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class FloorMove : MonoBehaviour
{
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    public float startSpeed = 0f;   // 初期速度
    public float targetSpeed = 80f; // 最終速度
    public float accelerationTime = 4f; // 加速にかける時間（秒）
    private float currentSpeed = 0f; // 現在の速度
    private float timeElapsed = 0f; // 経過時間


    // Start is called before the first frame update
    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()//精密な処理を行うためFixedUpdateを使用
    {
        if (gameManagerScript.IsOpenSelector()) // セレクター画面出現
        {
            // 時間を更新
            timeElapsed = Mathf.Min(timeElapsed + Time.deltaTime, accelerationTime); // accelerationTimeを超えないように制限

            // スピードを徐々に変化させる
            if (timeElapsed <= accelerationTime)
            {
                currentSpeed = Mathf.Lerp(startSpeed, targetSpeed, timeElapsed / accelerationTime); // 加速
            }
            else if (timeElapsed <= 125f)
            {
                currentSpeed = targetSpeed; // 等速維持（125秒まで）
            }
            else if (timeElapsed <= 125f + 3f) // 3秒かけて減速
            {
                float decelT = (timeElapsed - 125f) / 3f;
                currentSpeed = Mathf.Lerp(targetSpeed, 0f, decelT); // 減速
            }
            else
            {
                currentSpeed = 0f; // 停止
            }


            // オブジェクトを移動
            transform.Translate(-Vector3.forward * currentSpeed * Time.deltaTime);

            // 位置が条件に達した場合、座標をリセット
            if (transform.position.z <= -165f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 165f);
            }
        }
    }



}
