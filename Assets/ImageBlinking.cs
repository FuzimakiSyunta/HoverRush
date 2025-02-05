using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBlinking : MonoBehaviour
{
    public Image img;
    float duration = 1.0f;
    //開始時の色。
    Color32 startColor = new Color32(255, 255, 255, 255);

    //終了(折り返し)時の色。
    Color32 endColor = new Color32(255, 255, 255, 64);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Awake()
    {
        if (img == null)
            img = GetComponent<Image>();
    }

    void Update()
    {
        //Color.Lerpに開始の色、終了の色、0～1までのfloatを渡すと中間の色が返される。
        //Mathf.PingPongに経過時間を渡すと、0～1までの値が返される。
        img.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time / duration, 1.0f));
    }
}
