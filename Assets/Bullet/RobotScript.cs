using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;

    

    private float bulletTimer = 0;
    //private float BulletCoolTime = 0;
    public Animator animator;

    public GameObject RobotBullet;
    public GameObject RobotBullet_L;
    public GameObject RobotBullet_R;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        
        animator = GetComponent<Animator>();
        bulletTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (animator.GetBool("isRobotStay") == true)
        {
            if (gameManagerScript.IsGameOver() == false)
            {
                if (bulletTimer == 0.0f)
                {
                    Vector3 position = transform.position;
                    position.y += 0.3f;
                    position.z -= 3.0f;
                    Instantiate(RobotBullet_R, position, Quaternion.identity);
                    bulletTimer = 1.0f;
                }
                else
                {
                    bulletTimer++;
                    if (bulletTimer > 60.0f)
                    {
                        bulletTimer = 0.0f;
                    }
                }
                
                if (bulletTimer == 0.0f)
                {
                    Vector3 position = transform.position;
                    position.y += 0.3f;
                    position.z -= 3.0f;
                    Instantiate(RobotBullet_L, position, Quaternion.identity);
                    bulletTimer = 1.0f;
                }
                else
                {
                    bulletTimer++;
                    if (bulletTimer > 120.0f)
                    {
                        bulletTimer = 0.0f;
                    }
                }

                if (bulletTimer == 0.0f)
                {
                    Vector3 position = transform.position;
                    position.y += 0.3f;
                    position.z -= 3.0f;
                    Instantiate(RobotBullet_L, position, Quaternion.identity);
                    bulletTimer = 1.0f;
                }
                else
                {
                    bulletTimer++;
                    if (bulletTimer > 60.0f)
                    {
                        bulletTimer = 0.0f;
                    }
                }



            }
        }
    }


}
