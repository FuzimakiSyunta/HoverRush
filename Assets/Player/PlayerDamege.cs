using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public GameObject player; // �v���C���[�I�u�W�F�N�g
    private PlayerScript playerScript; // �v���C���[�X�N���v�g�Q��
    public GameObject pausesystem; // PauseSystem�I�u�W�F�N�g
    private PauseSystem pauseSystemScript; // PauseSystem�̃X�N���v�g

    public RawImage damageImage; // �_���[�W�G�t�F�N�g�p�̉摜�i�ԐF�j

    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        pauseSystemScript = pausesystem.GetComponent<PauseSystem>(); // PauseSystem�̃X�N���v�g���擾
        damageImage.color = new Color(1f, 0f, 0f, 0f); // �ŏ��͓���
    }

    void Update()
    {
        if (playerScript.IsDamage()) // �_���[�W������
        {
            StartCoroutine(FadeOutDamageImage()); // �t�F�[�h�A�E�g�������J�n
            StartCoroutine(HitStopEffect()); // �q�b�g�X�g�b�v�������J�n
        }
        

    }

    // �_���[�W�摜���t�F�[�h�A�E�g�����鏈��
    IEnumerator FadeOutDamageImage()
    {
        damageImage.color = new Color(1f, 0f, 0f, 0.5f); // �Ԃ��摜��\��

        float duration = 0.5f; // �t�F�[�h�A�E�g�ɂ����鎞��
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0.5f, 0f, elapsed / duration); // Alpha�l�����X�Ɍ���
            damageImage.color = new Color(1f, 0f, 0f, alpha); // �ԐF���ێ����Ȃ��瓧���x��ύX
            yield return null; // �t���[�����Ƃɑҋ@
        }

        // ���S�ɓ����ɐݒ�
        damageImage.color = new Color(1f, 0f, 0f, 0f);
    }

    // �q�b�g�X�g�b�v����
    IEnumerator HitStopEffect()
    {
        if (!pauseSystemScript.IsPaused())
        {
            Time.timeScale = 0.1f; // ��u�����X���[
            yield return new WaitForSecondsRealtime(0.25f); // 0.25�b���A���^�C���őҋ@
            // �Q�[���X�s�[�h�����ɖ߂�
            Time.timeScale = 1f;
        }
        else
        {
            // �|�[�Y���͎��Ԃ����ɖ߂��Ȃ�
            Time.timeScale = 0f;

        }
    }
}