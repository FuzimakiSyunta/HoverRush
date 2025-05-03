using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOVController : MonoBehaviour
{
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    public Camera targetCamera; // 操作したいカメラ
    private float targetFOV = 90.0f; // 設定したい視野角
    private float changeSpeed = 0.5f; // 視野角の変化速度

    private void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        if(gameManagerScript.IsGameStart())
        {
            // 視野角をスムーズに変更
            targetCamera.fieldOfView = Mathf.Lerp(targetCamera.fieldOfView, targetFOV, changeSpeed * Time.deltaTime);
        }
        
    }

    // 視野角を広げるメソッド
    public void IncreaseFOV(float amount)
    {
        targetFOV += amount; // FOVを増加
        targetFOV = Mathf.Clamp(targetFOV, 30.0f, 85.0f); // FOVの範囲を制限
    }

    // 視野角を狭めるメソッド
    public void DecreaseFOV(float amount)
    {
        targetFOV -= amount; // FOVを減少
        targetFOV = Mathf.Clamp(targetFOV, 30.0f, 85.0f); // FOVの範囲を制限
    }
}
