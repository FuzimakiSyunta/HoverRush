using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using UnityEngine.UI;

public class SelectorMenu : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    public RectTransform Selector;
    public GameObject SelectorImage;
    public GameObject GAMESTARTImage;
    public GameObject SETTINGImage;
    public bool ColorMenuFlag;//セッティングが出せる状態
    public bool StartFlag;//ゲームが始められる状態
    public RectTransform SettingMENUImage;
    private float move = 1.0f;
    private float selectormove = 210.0f;
    public RectTransform StartImage;
    public bool Hrizonal_L;
    public bool Vertical_L;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //selector
        SelectorImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.IsOpenSelector()==true)
        {
            //画像移動
            if (SettingMENUImage.position.x >= 325.0f)
            {
                move -= 1.0f;
                SelectorImage.SetActive(true);

                StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
                StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 120);

                //Selector
                if (ColorMenuFlag == false)
                {
                    StartFlag = true;
                    StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
                    StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 120);
                    //L Stick
                    float lsh = Input.GetAxis("L_Stick_H");
                    float lsv = Input.GetAxis("L_Stick_V");
                    if ((lsh != 0))
                    {
                        Hrizonal_L = true;
                        Vertical_L = false;
                    }
                    if ((lsv != 0))
                    {
                        Hrizonal_L = false;
                        Vertical_L = true;
                    }

                    if (Input.GetKeyDown(KeyCode.S)/*|| Hrizonal_L == true*/)
                    {
                        Selector.position += new Vector3(0, -selectormove, 0);
                        StartFlag = false;
                        ColorMenuFlag = true;
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        gameManagerScript.GameStart();
                    }
                }
                else
                {
                    StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
                    StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
                }
                if (ColorMenuFlag == true)
                {
                    SettingMENUImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
                    SettingMENUImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 120);
                    if (Input.GetKeyDown(KeyCode.W)/*|| Vertical_L == true*/)
                    {
                        Selector.position += new Vector3(0, selectormove, 0);
                        ColorMenuFlag = false;
                        StartFlag = true;
                    }
                }
                else
                {
                    SettingMENUImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
                    SettingMENUImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
                }
            }
            else
            {
                SettingMENUImage.position += new Vector3(move, 0, 0);
                StartImage.position += new Vector3(move, 0, 0);
            }
        }
        
    }
    public bool IsColorMenuFlag()
    {
        return ColorMenuFlag;
    }
    public bool IsStartFlag()
    {
        return StartFlag;
    }
}
