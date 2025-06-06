using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageEffect : MonoBehaviour
{
    public GameObject player;
    private PlayerDamage playerScript;

    public GameObject pausesystem;
    private PauseSystem pauseSystemScript;

    public RawImage damageImage;

    public Camera gameCamera;
    private CameraShaker cameraShaker;

    private Coroutine fadeCoroutine = null;
    private Coroutine blinkCoroutine = null;

    private bool isLaserShaking = false;

    void Start()
    {
        playerScript = player.GetComponent<PlayerDamage>();
        pauseSystemScript = pausesystem.GetComponent<PauseSystem>();
        damageImage.color = new Color(1f, 0f, 0f, 0f);

        if (gameCamera != null)
        {
            cameraShaker = gameCamera.GetComponent<CameraShaker>();
        }
    }

    void Update()
    {
        if(!playerScript.IsSheildActive())
        {
            // 通常のダメージ
            if (playerScript.IsTookDamage())
            {
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                    fadeCoroutine = null;
                }

                StartCoroutine(HandleDamageEffect());
                playerScript.ResetDamageFlag();
            }

            // レーザー接触中
            if (playerScript.IsTouchingLaser())
            {
                if (blinkCoroutine == null)
                {
                    blinkCoroutine = StartCoroutine(BlinkDamageImage());
                }

                if (!isLaserShaking && cameraShaker != null)
                {
                    cameraShaker.StartContinuousShake(0.15f);
                    isLaserShaking = true;
                }
            }
            else
            {
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                    damageImage.color = new Color(1f, 0f, 0f, 0f);
                }

                if (isLaserShaking && cameraShaker != null)
                {
                    cameraShaker.StopContinuousShake();
                    isLaserShaking = false;
                }
            }
        }
        
    }

    IEnumerator HandleDamageEffect()
    {
        damageImage.color = new Color(1f, 0f, 0f, 0.5f);

        if (cameraShaker != null)
        {
            cameraShaker.ShakeOnce(0.3f, 0.25f);
        }

        fadeCoroutine = StartCoroutine(FadeOutDamageImage());
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
