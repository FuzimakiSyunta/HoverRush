using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public GameObject player; // プレイヤーオブジェクト
    private PlayerScript playerScript; // プレイヤースクリプト参照
    public GameObject pausesystem; // PauseSystemオブジェクト
    private PauseSystem pauseSystemScript; // PauseSystemのスクリプト

    public RawImage damageImage; // ダメージエフェクト用の画像（赤色）

    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        pauseSystemScript = pausesystem.GetComponent<PauseSystem>(); // PauseSystemのスクリプトを取得
        damageImage.color = new Color(1f, 0f, 0f, 0f); // 最初は透明
    }

    void Update()
    {
        if (playerScript.IsDamage()) // ダメージ発生時
        {
            StartCoroutine(FadeOutDamageImage()); // フェードアウト処理を開始
            StartCoroutine(HitStopEffect()); // ヒットストップ処理を開始
        }
        

    }

    // ダメージ画像をフェードアウトさせる処理
    IEnumerator FadeOutDamageImage()
    {
        damageImage.color = new Color(1f, 0f, 0f, 0.5f); // 赤い画像を表示

        float duration = 0.5f; // フェードアウトにかかる時間
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0.5f, 0f, elapsed / duration); // Alpha値を徐々に減少
            damageImage.color = new Color(1f, 0f, 0f, alpha); // 赤色を維持しながら透明度を変更
            yield return null; // フレームごとに待機
        }

        // 完全に透明に設定
        damageImage.color = new Color(1f, 0f, 0f, 0f);
    }

    // ヒットストップ処理
    IEnumerator HitStopEffect()
    {
        if (!pauseSystemScript.IsPaused())
        {
            Time.timeScale = 0.1f; // 一瞬だけスロー
            yield return new WaitForSecondsRealtime(0.25f); // 0.25秒リアルタイムで待機
            // ゲームスピードを元に戻す
            Time.timeScale = 1f;
        }
        else
        {
            // ポーズ中は時間を元に戻さない
            Time.timeScale = 0f;

        }
    }
}