using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem particle;
    private float moveSpeedZ;
    private float moveSpeedY;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeedZ = 23.0f;
        moveSpeedY = -35.0f;
        rb.velocity = new Vector3(0, moveSpeedY, moveSpeedZ);
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // �o�ߎ��ԂɊ�Â��ăX�s�[�h��ω�������
        float timeFactor = Time.time; // Unity�̃��[���h�J�n����̎��Ԃ��擾
        float newSpeedZ = moveSpeedZ + (timeFactor * 1.1f); // Z�X�s�[�h�����X�ɑ���
        float newSpeedY = moveSpeedY + (timeFactor * 2.0f); // Y�X�s�[�h�����X�ɑ���
        rb.velocity = new Vector3(0, newSpeedY, newSpeedZ);
    }
    void Damaged()
    {
        // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
        ParticleSystem newParticle = Instantiate(particle);
        // �p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
        newParticle.transform.position = this.transform.position;
        // �p�[�e�B�N���𔭐�������B
        newParticle.Play();
        // �C���X�^���X�������p�[�e�B�N���V�X�e����GameObject��5�b��ɍ폜����B(�C��)
        // ����������newParticle�����ɂ���ƃR���|�[�l���g�����폜����Ȃ��B
        Destroy(newParticle.gameObject, 5.0f);
    }
    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "Enemy")
        {
            Damaged();
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Boss")
        {
            Damaged();
            Destroy(this.gameObject);
        }
    }

}
