using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;

    private float bulletTimer = 0;
    private float BulletCoolTime = 0;
    private Animator animator;

    public GameObject RobotBullet;
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
        if (animator.GetBool("FinalWave") == true)
        {
            //ŽU’e
            if (bulletTimer == 0.0f)
            {
                Vector3 position = transform.position;
                BulletCoolTime++;

                if (BulletCoolTime >= 120)
                {
                    position.y += 0.3f;
                    position.z = 35.0f;
                    Instantiate(RobotBullet, position, Quaternion.identity);
                    bulletTimer = 1.0f;

                }


            }
            else
            {
                bulletTimer++;
                if (bulletTimer > 60.0f)
                {
                    bulletTimer = 0.0f;
                }
                BulletCoolTime = 0;
            }
        }
    }

}
