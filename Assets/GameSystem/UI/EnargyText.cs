using TMPro;
using UnityEngine;

public class EnargyText : MonoBehaviour
{
    public TextMeshProUGUI enargyText; // TextMeshProテキスト
    private GameManager gameManagerScript;
    public GameObject gameManager;

    private int previousEnergy = 0;
    private Vector3 originalScale;
    private float scaleDuration = 0.2f;
    private float scaleTimer = 0f;
    private bool isScaling = false;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        originalScale = enargyText.transform.localScale;
        previousEnergy = gameManagerScript.GetBatteryEnargy();
    }

    void Update()
    {
        int currentEnergy = gameManagerScript.GetBatteryEnargy();
        enargyText.text = currentEnergy.ToString();

        // 数値が増えた時だけ拡大演出
        if (currentEnergy > previousEnergy)
        {
            isScaling = true;
            scaleTimer = 0f;
        }

        // 拡大縮小のアニメーション
        if (isScaling)
        {
            scaleTimer += Time.deltaTime;
            float t = scaleTimer / scaleDuration;

            if (t < 0.5f)
            {
                // 拡大
                enargyText.transform.localScale = Vector3.Lerp(originalScale, originalScale * 1.4f, t * 2f);
            }
            else if (t < 1f)
            {
                // 縮小
                enargyText.transform.localScale = Vector3.Lerp(originalScale * 1.4f, originalScale, (t - 0.5f) * 2f);
            }
            else
            {
                // 終了
                enargyText.transform.localScale = originalScale;
                isScaling = false;
            }
        }

        previousEnergy = currentEnergy;
    }
}
