using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationScript : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        float moveSpeed = 27.0f;
        rb.velocity = new Vector3(0, 0, moveSpeed);
        Destroy(gameObject, 1.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Damaged()
    {
        //�p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
        //ParticleSystem newParticle = Instantiate(particle);
        //�p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
        //newParticle.transform.position = this.transform.position;
        //�p�[�e�B�N���𔭐�������B
        //newParticle.Play();
        //�C���X�^���X�������p�[�e�B�N���V�X�e����GameObject��5�b��ɍ폜����B(�C��)
        // ����������newParticle�����ɂ���ƃR���|�[�l���g�����폜����Ȃ��B
        //Destroy(newParticle.gameObject, 5.0f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Damaged();
            
        }
        if (other.gameObject.tag == "Boss")
        {
            Damaged();
            
        }
    }
}
