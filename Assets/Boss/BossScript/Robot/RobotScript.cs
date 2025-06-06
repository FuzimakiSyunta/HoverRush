using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;

    public Animator animator;

    public GameObject projectilePrefab; // 弾のプレハブ
    public Transform target; // ターゲット
    public float projectileSpeed = 10f; // 弾の初速度
    public float fireRate = 0.5f; // 弾の発射間隔（秒）

    private bool isFirstShot = true; // 最初の待機を管理するフラグ
    private float bulletTimer = 0; // タイマーを管理

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (animator.GetBool("isRobotStay") == true && !gameManagerScript.IsGameOver())
        {
            if (isFirstShot)
            {
                bulletTimer += Time.deltaTime; // 最初の待機時間を計測
                if (bulletTimer >= 2.5f) // 待機後に弾を発射
                {
                    FireProjectile(); // 最初の弾を発射
                    bulletTimer = 0; // タイマーをリセット
                    isFirstShot = false; // 待機を解除
                }
            }
            else
            {
                bulletTimer += Time.deltaTime; // 継続発射用のタイマーを計測
                if (bulletTimer >= fireRate) // 発射間隔に従って弾を発射
                {
                    FireProjectile(); // 弾を発射
                    bulletTimer = 0; // タイマーをリセット
                }
            }
        }
    }

    void FireProjectile()
    {
        // 弾を生成
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        // 弾にターゲットを設定
        RobotBullet robotbulletScript = projectile.GetComponent<RobotBullet>();
        if (robotbulletScript != null)
        {
            robotbulletScript.SetTarget(target);
        }
    }
}