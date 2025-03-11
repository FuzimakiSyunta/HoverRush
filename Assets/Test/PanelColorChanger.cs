using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelColorChanger : MonoBehaviour
{
    public Image panelImage; // パネルのImageコンポーネント
    public float transitionDuration = 1f; // 色を変えるのにかかる時間
    private bool isTriggered = false; // 発動フラグ

    private GameManager gameManagerScript;
    public GameObject gameManager;


    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    private void Update()
    {
        // Enterキー（またはReturnキー）が押されたときに発動
        if (Input.GetKeyDown(KeyCode.Return) && !isTriggered)
        {
            isTriggered = true; // 発動フラグをセット
            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor()
    {
        // 透明 (アルファ値0)
        Color startColor = new Color(1f, 1f, 1f, 0f);
        // 白 (アルファ値1)
        Color endColor = new Color(1f, 1f, 1f, 1f);

        // 透明から白にフェード
        yield return StartCoroutine(FadeColor(startColor, endColor));

        // 少し待機
        yield return new WaitForSeconds(1f);

        // 白から透明にフェード
        yield return StartCoroutine(FadeColor(endColor, startColor));

        // 発動フラグをリセット
        isTriggered = false;
    }

    private IEnumerator FadeColor(Color fromColor, Color toColor)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            // 色を補間して設定
            panelImage.color = Color.Lerp(fromColor, toColor, elapsedTime / transitionDuration);
            yield return null;
        }

        // 最終的な色を正確に設定
        panelImage.color = toColor;
    }
}
