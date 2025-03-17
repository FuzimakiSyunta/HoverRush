using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using UnityEngine.UI;

public class SelectorMenu : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    //public RectTransform Selector;
    //public GameObject SelectorImage;
    public GameObject GAMESTARTImage;
    public GameObject SETTINGImage;
    public bool ColorMenuFlag;//セッティングが出せる状態
    public bool StartFlag;//ゲームが始められる状態
    private float move = 5.5f;
    //private float selectormove = 210.0f;
    public RectTransform SettingMENUImage;
    public RectTransform StartImage;
    public GameObject LuleBGmage;
    public GameObject LuleUiImage;
    public GameObject LTRTImage;

    private bool isSeaneEffect = false;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        ////selector
        //SelectorImage.SetActive(false);
        
        //Lule
        LuleBGmage.SetActive(false);
        LuleUiImage.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        //L Stick
        float tri = Input.GetAxis("L_R_Trigger");
        

        if (gameManagerScript.IsOpenSelector()==true&&gameManagerScript.IsGameStart()==false)
        {
            //画像移動
            if (SettingMENUImage.position.x >= 410.0f)
            {
                move -= 5.5f;
                

                StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
                StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 120);
                
                //Selector
                if (ColorMenuFlag == false && isSeaneEffect == false)
                {
                    StartFlag = true;
                    StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
                    StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 120);
                    LuleBGmage.SetActive(true);
                    LuleUiImage.SetActive(true);


                    if (Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow)|| tri > 0)
                    {
                        
                        StartFlag = false;
                        ColorMenuFlag = true;
                        
                    }
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
                    {
                        isSeaneEffect = true;
                        LuleBGmage.SetActive(false);
                        LuleUiImage.SetActive(false);
                        GAMESTARTImage.SetActive(false);
                        SETTINGImage.SetActive(false);
                        
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
                    LuleBGmage.SetActive(false);
                    LuleUiImage.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow) || tri < 0&&isSeaneEffect == false)
                    {
                        
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
        }else
        {
            isSeaneEffect = false;
        }
        
    }
    public bool IsColorMenuFlag()
    {
        return ColorMenuFlag;//SettingボタンのFrag
    }
    public bool IsStartButtonFlag()
    {
        return StartFlag;//GameStartボタンのFrag
    }

    public bool IsSeaneEffectFlag()
    {
        return isSeaneEffect;//画面推移Effectフラグ
    }
}
