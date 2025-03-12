using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBlinking : MonoBehaviour
{
    public Image img;
    float duration = 1.0f;
    //�J�n���̐F�B
    Color32 startColor = new Color32(255, 255, 255, 255);

    //�I��(�܂�Ԃ�)���̐F�B
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
        //Color.Lerp�ɊJ�n�̐F�A�I���̐F�A0�`1�܂ł�float��n���ƒ��Ԃ̐F���Ԃ����B
        //Mathf.PingPong�Ɍo�ߎ��Ԃ�n���ƁA0�`1�܂ł̒l���Ԃ����B
        img.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time / duration, 1.0f));
    }
}
