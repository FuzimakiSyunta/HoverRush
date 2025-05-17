using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class FloorMove : MonoBehaviour
{
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    public float startSpeed = 0f;   // �������x
    public float targetSpeed = 80f; // �ŏI���x
    public float accelerationTime = 4f; // �����ɂ����鎞�ԁi�b�j
    private float currentSpeed = 0f; // ���݂̑��x
    private float timeElapsed = 0f; // �o�ߎ���


    // Start is called before the first frame update
    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsOpenSelector()) // �Z���N�^�[��ʏo��
        {
            // ���Ԃ��X�V
            timeElapsed = Mathf.Min(timeElapsed + Time.deltaTime, accelerationTime); // accelerationTime�𒴂��Ȃ��悤�ɐ���

            // �X�s�[�h�����X�ɕω�������
            if (timeElapsed <= accelerationTime)
            {
                currentSpeed = Mathf.Lerp(startSpeed, targetSpeed, timeElapsed / accelerationTime); // ����
            }
            else if (timeElapsed <= 125f)
            {
                currentSpeed = targetSpeed; // �����ێ��i125�b�܂Łj
            }
            else if (timeElapsed <= 125f + 3f) // 3�b�����Č���
            {
                float decelT = (timeElapsed - 125f) / 3f;
                currentSpeed = Mathf.Lerp(targetSpeed, 0f, decelT); // ����
            }
            else
            {
                currentSpeed = 0f; // ��~
            }


            // �I�u�W�F�N�g���ړ�
            transform.Translate(-Vector3.forward * currentSpeed * Time.deltaTime);

            // �ʒu�������ɒB�����ꍇ�A���W�����Z�b�g
            if (transform.position.z <= -165f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 165f);
            }
        }
    }

    

}
