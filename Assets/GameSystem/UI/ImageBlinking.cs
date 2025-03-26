using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBlinking : MonoBehaviour
{
    public Image[] img; // Image 配列
    float duration = 1.0f;
    //開始時の色
    Color32 startColor = new Color32(255, 255, 255, 255);

    //終了時の色
    Color32 endColor = new Color32(255, 255, 255, 64);

    void Awake()
    {
        // img の null チェックとデフォルト値設定は、以下のように修正
        if (img == null || img.Length == 0)
        {
            img = GetComponentsInChildren<Image>(); // 子オブジェクトから Image コンポーネントを取得
        }
    }

    void Update()
    {
        // for ループ修正: 配列のインデックス範囲を適切に制御
        for (int i = 0; i < img.Length; i++)
        {
            img[i].color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time / duration, 1.0f));
        }
    }
}