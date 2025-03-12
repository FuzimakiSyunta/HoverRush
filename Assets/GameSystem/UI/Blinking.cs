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
    //�J�n���̐F�B
    Color32 startColor = new Color32(255, 255, 255, 255);

    //�I��(�܂�Ԃ�)���̐F�B
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
