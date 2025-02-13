using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Blinking : MonoBehaviour
{
    public float duration = 1.0f;
    public TextMeshProUGUI text;
    //開始時の色。
    Color32 startColor = new Color32(255, 255, 255, 255);

    //終了(折り返し)時の色。
    Color32 endColor = new Color32(255, 255, 255, 64);


    void Start()
    {
    }

    void Awake()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time / duration, 1.0f));
    }
}
