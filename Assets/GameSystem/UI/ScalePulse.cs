using System.Collections;
using UnityEngine;

public class ScaleOnce : MonoBehaviour
{
    public float scaleFactor = 1.5f; // �g��{��
    public float duration = 0.3f;   // �g��E�k���ɂ����鎞��
    private Vector3 originalScale;
    private bool hasScaled = false; // ���łɊg��k�������s���ꂽ���ǂ������m�F����t���O
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
        hasScaled = true; // ��x���s�����炱��ȏ�g��k�����s��Ȃ��悤�ɂ���

        // �g��
        yield return StartCoroutine(ScaleTo(originalScale * scaleFactor, duration));
        // �k��
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