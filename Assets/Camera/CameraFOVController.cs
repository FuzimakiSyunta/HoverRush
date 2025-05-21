using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOVController : MonoBehaviour
{
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    public Camera targetCamera; // ���삵�����J����
    private float targetFOV = 90.0f; // �ݒ肵��������p
    private float changeSpeed = 0.5f; // ����p�̕ω����x

    private void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        if(gameManagerScript.IsGameStart())
        {
            // ����p���X���[�Y�ɕύX
            targetCamera.fieldOfView = Mathf.Lerp(targetCamera.fieldOfView, targetFOV, changeSpeed * Time.deltaTime);
        }
        
    }

    // ����p���L���郁�\�b�h
    public void IncreaseFOV(float amount)
    {
        targetFOV += amount; // FOV�𑝉�
        targetFOV = Mathf.Clamp(targetFOV, 30.0f, 85.0f); // FOV�͈̔͂𐧌�
    }

    // ����p�����߂郁�\�b�h
    public void DecreaseFOV(float amount)
    {
        targetFOV -= amount; // FOV������
        targetFOV = Mathf.Clamp(targetFOV, 30.0f, 85.0f); // FOV�͈̔͂𐧌�
    }
}
