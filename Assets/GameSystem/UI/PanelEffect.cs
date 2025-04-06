using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PanelEffect : MonoBehaviour
{
    public Image panelImage; // パネルのImageコンポーネント
    public float transitionDuration = 2f; // 色を変えるのにかかる時間

    private bool isChangingColor = false; // 色変更中のフラグ
    private bool hasTriggeredEffect = false; // エフェクトが発動したかどうかのフラグ

    private SelectorMenu selectorMenuScript;
    public GameObject selectorMenu;
    
    public GameObject Allui;
    private bool isWhite;
    private bool isAlpha;

    // Tutorial
    private TutorialManager tutorialManagerScript;
    public GameObject tutorialManager;


    void Start()
    {
        selectorMenuScript = selectorMenu.GetComponent<SelectorMenu>();
        
        //tutorial
        tutorialManagerScript = tutorialManager.GetComponent<TutorialManager>();
    }

    private void Update()
    {
        // フェード中でなく、かつエフェクトがまだ発動していない場合に開始
        if (selectorMenuScript.IsSeaneEffectFlag() == true && !isChangingColor && !hasTriggeredEffect)
        {
            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor()
    {
        isChangingColor = true; // フラグをセットして多重実行を防止
        hasTriggeredEffect = true; // エフェクト発動フラグをセット

        // 透明 (アルファ値0) から 白 (アルファ値1)
        Color startColor = new Color(1f, 1f, 1f, 0f);
        Color endColor = new Color(1f, 1f, 1f, 1f);

        // 透明から白にフェード
        yield return StartCoroutine(FadeColor(startColor, endColor));
        isWhite = true;
        isAlpha = false;
        Allui.SetActive(false);

        // 少し待機（白の状態を維持する時間）
        yield return new WaitForSeconds(2f);
        

        // 白から透明にフェード
        yield return StartCoroutine(FadeColor(endColor, startColor));
        isWhite = false;
        isAlpha = true;
        Allui.SetActive(true);
        tutorialManagerScript.TutorialStart();//チュートリアル開始


        isChangingColor = false; // 色変更フラグをリセット
    }

    private IEnumerator FadeColor(Color fromColor, Color toColor)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            panelImage.color = Color.Lerp(fromColor, toColor, elapsedTime / transitionDuration);
            yield return null;
        }

        // 最終的な色を正確に設定
        panelImage.color = toColor;
    }

    // 必要に応じて外部からエフェクト状態をリセット可能
    public void ResetEffect()
    {
        hasTriggeredEffect = false;
    }
    public bool IsWhite()
    {
        return isWhite;
    }
    public bool IsAlpha()
    {
        return isAlpha;
    }
}
