using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public GameObject player; // プレイヤーオブジェクト
    private PlayerScript playerScript; // プレイヤースクリプト参照

    public RawImage sandstormImage; // 砂嵐エフェクト用の RawImage
    private Color sandstormColor = new Color(1f, 1f, 1f, 0f); // 透明から開始

    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        sandstormImage.color = new Color(sandstormColor.r, sandstormColor.g, sandstormColor.b, 0f); // 最初は透明
    }

    void Update()
    {
        if (playerScript.IsDamage()) // ダメージ判定
        {
            //StartCoroutine(ShowSandstormEffect());  // 砂嵐処理
            StartCoroutine(HitStopEffect()); // ヒットストップ処理
        }
    }

    // 砂嵐処理
    IEnumerator ShowSandstormEffect()
    {
        sandstormImage.color = new Color(1f, 1f, 1f, 0.5f); // ノイズを表示

        float duration = 2f; // 砂嵐の持続時間
        yield return new WaitForSeconds(duration);

        sandstormImage.color = new Color(1f, 1f, 1f, 0f); // 徐々に透明に
    }

    // ヒットストップ処理
    IEnumerator HitStopEffect()
    {
        Time.timeScale = 0.1f; // 一瞬だけスロー
        yield return new WaitForSecondsRealtime(0.25f); // 0.1秒待機（リアルタイム）

        // 確実にタイムスケールを元に戻す
        Time.timeScale = 1f;
        
    }


}
