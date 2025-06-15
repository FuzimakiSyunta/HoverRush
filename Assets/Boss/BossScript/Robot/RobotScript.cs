using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;

    public Animator animator;

    public GameObject projectilePrefab; // �e�̃v���n�u
    public Transform target; // �^�[�Q�b�g
    public float projectileSpeed = 10f; // �e�̏����x
    public float fireRate = 0.5f; // �e�̔��ˊԊu�i�b�j

    private bool isFirstShot = true; // �ŏ��̑ҋ@���Ǘ�����t���O
    private float bulletTimer = 0; // �^�C�}�[���Ǘ�

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
                bulletTimer += Time.deltaTime; // �ŏ��̑ҋ@���Ԃ��v��
                if (bulletTimer >= 2.5f) // �ҋ@��ɒe�𔭎�
                {
                    FireProjectile(); // �ŏ��̒e�𔭎�
                    bulletTimer = 0; // �^�C�}�[�����Z�b�g
                    isFirstShot = false; // �ҋ@������
                }
            }
            else
            {
                bulletTimer += Time.deltaTime; // �p�����˗p�̃^�C�}�[���v��
                if (bulletTimer >= fireRate) // ���ˊԊu�ɏ]���Ēe�𔭎�
                {
                    FireProjectile(); // �e�𔭎�
                    bulletTimer = 0; // �^�C�}�[�����Z�b�g
                }
            }
        }
    }

    void FireProjectile()
    {
        // �e�𐶐�
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        // �e�Ƀ^�[�Q�b�g��ݒ�
        RobotBullet robotbulletScript = projectile.GetComponent<RobotBullet>();
        if (robotbulletScript != null)
        {
            robotbulletScript.SetTarget(target);
        }
    }
}