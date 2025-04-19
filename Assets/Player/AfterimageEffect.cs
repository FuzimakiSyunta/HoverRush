using System.Collections;
using UnityEngine;

public class AfterimageEffect : MonoBehaviour
{
    public Material afterimageMaterial; // ネオン風のマテリアル
    private float spawnInterval = 0.05f; // 残像を生成する間隔
    private float fadeDuration = 0.3f; // 残像のフェードアウト時間

    public GameObject player; // プレイヤーオブジェクト
    private PlayerScript playerScript;

    private float timer;


    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval&&playerScript.IsMoveActive())
        {
            CreateAfterimage(); // 自分自身の残像を生成
            timer = 0f;
        }
    }

    void CreateAfterimage()
{
    // 自身の現在の状態をコピー
    GameObject afterimage = Instantiate(gameObject, transform.position, transform.rotation);

    // スケールを小さくする
    afterimage.transform.localScale = transform.localScale * 0.25f; // 残像を元の50%のサイズに設定

    // 残像オブジェクトの設定
    foreach (var renderer in afterimage.GetComponentsInChildren<Renderer>())
    {
        renderer.material = afterimageMaterial; // ネオン風のマテリアルを適用
    }

    // 不要なコンポーネントを削除
    Destroy(afterimage.GetComponent<AfterimageEffect>()); // 残像は動作しないためスクリプトを削除
    Destroy(afterimage, fadeDuration); // 一定時間後に残像を削除

    StartCoroutine(FadeOut(afterimage)); // フェードアウト処理を開始
}

    private IEnumerator FadeOut(GameObject afterimage)
    {
        if (afterimage == null) yield break; // 早期終了: afterimageがnullの場合

        Renderer[] renderers = afterimage.GetComponentsInChildren<Renderer>();
        Transform afterimageTransform = afterimage.transform; // 残像のTransformを取得

        float fadeTimer = 0f;

        while (fadeTimer < fadeDuration)
        {
            // afterimageが破棄されているかを確認
            if (afterimage == null || afterimageTransform == null) yield break;

            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.25f, 0f, fadeTimer / fadeDuration);
            float scale = Mathf.Lerp(0.25f, 0.25f, fadeTimer / fadeDuration); // サイズを縮小

            afterimageTransform.localScale = Vector3.one * scale; // サイズを変更

            foreach (var renderer in renderers)
            {
                if (renderer != null && renderer.material.HasProperty("_Color"))
                {
                    Color color = renderer.material.color;
                    color.a = alpha; // 透明度を設定
                    renderer.material.color = color;
                }
            }

            yield return null;
        }
    }

}