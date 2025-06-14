using System.Collections;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject boss;
    public GameObject shieldPrefab;
    public GameObject chargeEffectPrefab; // �`���[�W�G�t�F�N�g�̃v���n�u

    private BossScript bossScript;
    private bool isCharging = false;

    // �Q�[���}�l�[�W���[�̎Q��
    private GameManager gameManagerScript;
    public GameObject gameManager;

    void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {

        // �ȉ��̓Q�[�����̂ݎ��s�����
        if (bossScript != null && bossScript.IsFinalBattle() && !isCharging)
        {
            Debug.Log("�K�E�Z�`���[�W�J�n�I");
            StartCoroutine(ChargeAndFire());
            
        }

        if (isCharging)
        {
            // �`���[�W���̃G�t�F�N�g��\��
            if (chargeEffectPrefab != null)
            {
                Instantiate(chargeEffectPrefab, firePoint.position, firePoint.rotation);
            }
        }
        else
        {
            // �`���[�W���łȂ��ꍇ�̓G�t�F�N�g���\���ɂ���
            if (chargeEffectPrefab != null)
            {
                foreach (var effect in GameObject.FindGameObjectsWithTag("ChargeEffect"))
                {
                    Destroy(effect);
                }
            }

        }


        IEnumerator ChargeAndFire()
        {
            isCharging = true;

            // �`���[�W���G�t�F�N�g�� Update() ���ŕ\�������

            yield return new WaitForSeconds(10f); // �`���[�W

            FireSpecialBullet(); // ����

            yield return new WaitForSeconds(20f); // �N�[���_�E��

            isCharging = false;
        }

        void FireSpecialBullet()
        {
            if (target != null && bulletPrefab != null)
            {
                transform.LookAt(target);

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = transform.forward * 10f;
                }
            }
        }

    }

    
}
