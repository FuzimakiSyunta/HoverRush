using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    private float MoveSpeed = 0.02f;
    private bool Moving;
    private GameManager gameManagerScript;
    public GameObject gameManager;

    //�v���C���[��ϐ��Ɋi�[
    public GameObject Player;
    private float angle;

    //select
    private SelectorMenu selectorMenuScript;
    public GameObject selectMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
        Moving = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float stick = Input.GetAxis("Horizontal");
        float Vstick = Input.GetAxis("Vertical");

        if (selectorMenuScript.IsStartFlag() == true && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
        {
            Moving = true;
        }
        if(Moving==true)
        {
            if (gameManagerScript.IsGameClear() == false&&gameManagerScript.IsGameOver()==false)
            {
                //�v���C���[�ʒu���
                Vector3 playerPos = Player.transform.position;
                //�J��������]������
                transform.RotateAround(playerPos, Vector3.up, angle);

                if (stick > 0 && transform.position.x <= 10)
                {
                    transform.position += new Vector3(MoveSpeed, 0, 0);
                }
                else if (stick < 0 && transform.position.x >= -10)
                {
                    transform.position += new Vector3(-MoveSpeed, 0, 0);
                }
                //�L�[�{�[�h
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
