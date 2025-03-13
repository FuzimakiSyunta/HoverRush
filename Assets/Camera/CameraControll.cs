using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControll : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    //�v���C���[��ϐ��Ɋi�[
    public GameObject Player;
    //��]������X�s�[�h
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
        // ���Ԉˑ��̈ړ�
        float move = rotateSpeed * Time.deltaTime;
        if (gameManagerScript.IsGameStart() == false)
        {
            //��]������p�x
            angle = rotateSpeed;
            //�v���C���[�ʒu���
            Vector3 playerPos = Player.transform.position;
            //�J��������]������
            transform.RotateAround(playerPos, Vector3.up, angle);
        }
        
    }
}
