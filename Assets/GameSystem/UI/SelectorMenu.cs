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
    public bool SettingButtonNowFlag;//セッティングが出せる状態
    public bool GameStartButtonNowFlag;//ゲームが始められる状態
    private float SelectorMove = 1.0f;
    private float baseSpeed; // 基本速度を設定
    public RectTransform SettingMENUImage;
    public RectTransform StartImage;
    public GameObject LuleBGmage;
    public GameObject LuleUiImage;
    public GameObject LTRTImage;
    //性能表
    public GameObject SpecImage;

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
        //
        SpecImage.SetActive(false);

        
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
                // moveを時間に基づいて計算
                SelectorMove = baseSpeed * Time.deltaTime; // 経過時間を掛けて速度を調整
                SelectorMove *= -1; // 移動方向を反転



                StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
                StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 120);
                
                //Selector
                if (SettingButtonNowFlag == false && isSeaneEffect == false)
                {
                    GameStartButtonNowFlag = true;
                    StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
                    StartImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 120);
                    LuleBGmage.SetActive(true);
                    LuleUiImage.SetActive(true);
                    SpecImage.SetActive(false);


                    if (Input.GetKeyDown(KeyCode.S)|| tri > 0)
                    {
                        
                        GameStartButtonNowFlag = false;
                        SettingButtonNowFlag = true;
                        
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
                if (SettingButtonNowFlag == true)
                {
                    SettingMENUImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
                    SettingMENUImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 120);
                    LuleBGmage.SetActive(false);
                    LuleUiImage.SetActive(false);
                    SpecImage.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.W)||  tri < 0&&isSeaneEffect == false)
                    {
                        
                        SettingButtonNowFlag = false;
                        GameStartButtonNowFlag = true;
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
                SettingMENUImage.position += new Vector3(SelectorMove, 0, 0);
                StartImage.position += new Vector3(SelectorMove, 0, 0);
                
            }
        }else
        {
            isSeaneEffect = false;
        }
        
    }
    public bool IsColorMenuFlag()
    {
        return SettingButtonNowFlag;//SettingボタンのFrag
    }
    public bool IsStartButtonFlag()
    {
        return GameStartButtonNowFlag;//GameStartボタンのFrag
    }

    public bool IsSeaneEffectFlag()
    {
        return isSeaneEffect;//画面推移Effectフラグ
    }
}
