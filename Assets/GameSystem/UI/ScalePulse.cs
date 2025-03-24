using System.Collections;
using UnityEngine;

public class ScaleOnce : MonoBehaviour
{
    public float scaleFactor = 1.5f; // Šg‘å”{—¦
    public float duration = 0.3f;   // Šg‘åEk¬‚É‚©‚©‚éŠÔ
    private Vector3 originalScale;
    private bool hasScaled = false; // ‚·‚Å‚ÉŠg‘åk¬‚ªÀs‚³‚ê‚½‚©‚Ç‚¤‚©‚ğŠm”F‚·‚éƒtƒ‰ƒO
    private PlayerScript playerScriptScript;
    public GameObject playerScript;

    void Start()
    {
        originalScale = transform.localScale;
        playerScriptScript = playerScript.GetComponent<PlayerScript>();
    }

    void Update()
    {
        if (playerScriptScript.IsHeal() == true && !hasScaled)
        {
            StartCoroutine(ScaleObject());
        }
    }

    IEnumerator ScaleObject()
    {
        hasScaled = true; // ˆê“xÀs‚µ‚½‚ç‚±‚êˆÈãŠg‘åk¬‚ğs‚í‚È‚¢‚æ‚¤‚É‚·‚é

        // Šg‘å
        yield return StartCoroutine(ScaleTo(originalScale * scaleFactor, duration));
        // k¬
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