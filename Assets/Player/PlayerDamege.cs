using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public GameObject player; // �v���C���[�I�u�W�F�N�g
    private PlayerScript playerScript; // �v���C���[�X�N���v�g�Q��

    public RawImage sandstormImage; // �����G�t�F�N�g�p�� RawImage
    private Color sandstormColor = new Color(1f, 1f, 1f, 0f); // ��������J�n

    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        sandstormImage.color = new Color(sandstormColor.r, sandstormColor.g, sandstormColor.b, 0f); // �ŏ��͓���
    }

    void Update()
    {
        if (playerScript.IsDamage()) // �_���[�W����
        {
            //StartCoroutine(ShowSandstormEffect());  // ��������
            StartCoroutine(HitStopEffect()); // �q�b�g�X�g�b�v����
        }
    }

    // ��������
    IEnumerator ShowSandstormEffect()
    {
        sandstormImage.color = new Color(1f, 1f, 1f, 0.5f); // �m�C�Y��\��

        float duration = 2f; // �����̎�������
        yield return new WaitForSeconds(duration);

        sandstormImage.color = new Color(1f, 1f, 1f, 0f); // ���X�ɓ�����
    }

    // �q�b�g�X�g�b�v����
    IEnumerator HitStopEffect()
    {
        Time.timeScale = 0.1f; // ��u�����X���[
        yield return new WaitForSecondsRealtime(0.25f); // 0.1�b�ҋ@�i���A���^�C���j

        // �m���Ƀ^�C���X�P�[�������ɖ߂�
        Time.timeScale = 1f;
        
    }


}
