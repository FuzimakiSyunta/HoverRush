using System.Collections;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour
{
    public Transform target; // ���b�N�I������^�[�Q�b�g
    public GameObject bulletPrefab; // �e�̃v���n�u
    public Transform firePoint; // �e�����˂����ʒu
    public float fireInterval = 0.5f; // ���ˊԊu�i�b�j

    // Boss
    public GameObject boss;
    private BossScript bossScript;

    void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
        
    }
    void Update()
    {
        if (bossScript != null && bossScript.IsFinalBattle() && !IsInvoking(nameof(FireOnce)))
        {
            Debug.Log("Final Battle�J�n�I�e�𔭎˂��܂�");
            InvokeRepeating(nameof(FireOnce), 0f, fireInterval);
        }
    }


    void FireOnce()
    {
        if (target != null && bulletPrefab != null)
        {
            // �^�[�Q�b�g�̕���������
            transform.LookAt(target);
            

            // �e�𐶐����Ĕ���
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // �e�̓����𐧌䂷��ꍇ�A�Ⴆ��Rigidbody�ŗ͂�������
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * 200f; // �e��O���ɔ�΂��i���x20�j
            }
        }
    }

}