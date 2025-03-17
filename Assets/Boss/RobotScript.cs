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

    //public GameObject RobotBullet;

    public GameObject projectilePrefab; // オブジェクト3 (弾) のプレハブ
    public Transform target; // オブジェクト2 (ターゲット)
    public float projectileSpeed = 10f; // 弾の初速度
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
        if (animator.GetBool("isRobotStay") == true /*|| animator.GetBool("isRobotStay") == false*/)
        {
            if (gameManagerScript.IsGameOver() == false)
            {
                if (bulletTimer == 0.0f)
                {
                    FireProjectile();
                    bulletTimer = 1.0f;
                }
                else
                {
                    bulletTimer++;
                    if (bulletTimer > 30.0f)
                    {
                        bulletTimer = 0.0f;
                    }
                }

            }

        }
    }

    void FireProjectile()
    {
        // オブジェクト3を生成
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        // 弾に初速度を付与
        RobotBullet robotbulletScript = projectile.GetComponent<RobotBullet>();
        if (robotbulletScript != null)
        {
            robotbulletScript.SetTarget(target);
        }
    }


}
