using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerScript : MonoBehaviour
{
    private float MoveSpeed = 0.02f;
    public GameObject bullet;
    float bulletTimer = 0.0f;
    private GameManager gameManagerScript;
    public GameObject gameManager;
    private Animator animator;
    private int MaxHp;
    public bool Damege = false;
    public int DamegePoint;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        MaxHp = 5;
        DamegePoint = 1;
        Damege = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameOver() == true)
        {
            transform.position += new Vector3(0, 0, 0);
            animator.SetBool("GameOver", true);
            return;
        }
        else
        {
            animator.SetBool("GameOver", false);
        }

        ///ゲームスタートしたら
        if(gameManagerScript.IsGameStart() == true)
        {
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
        }

    }
    void FixedUpdate()
    {
        if (gameManagerScript.IsGameOver() == true)
        {
            return;
        }
        ///ゲームスタートしたら
        if (gameManagerScript.IsGameStart() == true)
        {
            if (bulletTimer == 0.0f)
            {
                //弾発射
                if (Input.GetKey(KeyCode.Space))
                {
                    Vector3 position = transform.position;
                    position.y += 0.3f;
                    position.z += 1.0f;
                    Instantiate(bullet, position, Quaternion.identity);
                    bulletTimer = 1.0f;
                }
            }
            else
            {
                bulletTimer++;
                if (bulletTimer > 5.0f)
                {
                    bulletTimer = 0.0f;
                }
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Damege = true;
            if(Damege == true)
            {
                MaxHp = MaxHp - DamegePoint;
                Damege =false;
            }
            
            if(MaxHp <= 0)
            {
                Destroy(gameObject, 1);
                gameManagerScript.GameOverStart();
            }
        }
    }

    
   
}
