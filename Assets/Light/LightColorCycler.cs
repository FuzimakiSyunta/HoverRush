using UnityEngine;

public class LightColorCycler : MonoBehaviour
{
    private Light directionalLight;

    public float duration = 3f; // 各色の遷移にかける時間

    private Color[] colors = new Color[]
    {
        Color.white,
        new Color(0.5f, 0.7f, 1f), // 青っぽい色（空色）
        new Color(1f, 0.6f, 0.2f), // オレンジ
    };

    private int currentIndex = 0;
    private float timer = 0f;

    void Start()
    {
        directionalLight = GetComponent<Light>();
        if (directionalLight == null)
        {
            enabled = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / duration;

        // 色の補間
        Color fromColor = colors[currentIndex];
        Color toColor = colors[(currentIndex + 1) % colors.Length];

        directionalLight.color = Color.Lerp(fromColor, toColor, t);

        // 次の色へ
        if (timer >= duration)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % colors.Length;
        }
    }
}
