using System.Collections;
using UnityEngine;

public class ScalePulse : MonoBehaviour
{
    public float scaleFactor = 1.5f; // ägëÂî{ó¶
    public float duration = 0.3f;   // ägëÂÅEèkè¨Ç…Ç©Ç©ÇÈéûä‘
    private Vector3 originalScale;
    private PlayerStatus playerStatus;
    public GameObject player;

    void Start()
    {
        originalScale = transform.localScale;
        playerStatus = player.GetComponent<PlayerStatus>();
    }

    void Update()
    {
        if (playerStatus.IsHeal())
        {
            StartCoroutine(ScaleObject());
        }
    }

    IEnumerator ScaleObject()
    {
        // ägëÂ
        yield return StartCoroutine(ScaleTo(originalScale * scaleFactor, duration));
        // èkè¨
        yield return StartCoroutine(ScaleTo(originalScale, duration));
    }

    IEnumerator ScaleTo(Vector3 targetScale, float time)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}