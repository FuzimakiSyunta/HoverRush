using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public GameObject player;
    private PlayerScript playerScript;

    public GameObject pausesystem;
    private PauseSystem pauseSystemScript;

    public RawImage damageImage;

    private Coroutine fadeCoroutine = null;
    private Coroutine blinkCoroutine = null;

    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        pauseSystemScript = pausesystem.GetComponent<PauseSystem>();
        damageImage.color = new Color(1f, 0f, 0f, 0f);
    }

    void Update()
    {
        // tookDamage�t���O�������Ă����疈�񉉏o���s��
        if (playerScript.IsTookDamage())
        {
            // �O�̃t�F�[�h���o������Ύ~�߂�
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
                fadeCoroutine = null;
            }

            StartCoroutine(HandleDamageEffect());

            // �t���O���Z�b�g�iPlayerScript���Ɏ������K�v�j
            playerScript.ResetDamageFlag();
        }

        // ���[�U�[�ڐG���̓_�ŉ��o
        if (playerScript.IsTouchingLaser())
        {
            if (blinkCoroutine == null)
            {
                blinkCoroutine = StartCoroutine(BlinkDamageImage());
            }
        }
        else
        {
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
                damageImage.color = new Color(1f, 0f, 0f, 0f); // ���ɖ߂�
            }
        }
    }

    IEnumerator HandleDamageEffect()
    {
        // ���Ԃ�����
        damageImage.color = new Color(1f, 0f, 0f, 0.5f);

        fadeCoroutine = StartCoroutine(FadeOutDamageImage());
        //StartCoroutine(HitStopEffect());
        yield return null;
    }

    IEnumerator FadeOutDamageImage()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0.5f, 0f, elapsed / duration);
            damageImage.color = new Color(1f, 0f, 0f, alpha);
            yield return null;
        }

        damageImage.color = new Color(1f, 0f, 0f, 0f);
        fadeCoroutine = null;
    }

    //IEnumerator HitStopEffect()
    //{
    //    if (!pauseSystemScript.IsPaused())
    //    {
    //        Time.timeScale = 0.1f;
    //        yield return new WaitForSecondsRealtime(0.25f);
    //        Time.timeScale = 1f;
    //    }
    //    else
    //    {
    //        Time.timeScale = 0f;
    //    }
    //}

    IEnumerator BlinkDamageImage()
    {
        while (true)
        {
            damageImage.color = new Color(1f, 0f, 0f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            damageImage.color = new Color(1f, 0f, 0f, 0f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
