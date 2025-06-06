using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] Material[] materialArray = new Material[3];
    private int count;
    private bool canInput = true;  // 入力可能かどうかを示すフラグ
    private float previousDph = 0; // 前回のD_Pad_Hの値
    private float previousDpv = 0; // 前回のD_Pad_Vの値
    //select
    private SelectorMenu selectorMenuScript;
    public GameObject selectMenu;
    //メニュー画面
    public GameObject colorMenuNumber_L;
    public GameObject colorMenuNumber_CENTER;
    public GameObject colorMenuNumber_R;

    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;


    void Start()
    {
        count = 0;
        //selector
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        
        if (selectorMenuScript.IsColorMenuFlag() == true&& gameManagerScript.IsGameStart() == false)
        {
            float dph = Input.GetAxis("D_Pad_H");
            float dpv = Input.GetAxis("D_Pad_V");
            
            
            if (canInput)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || dph > 0 && previousDph >= 0)
                {
                    count++;
                    if (count > materialArray.Length - 1)
                    {
                        count = 0;
                    }
                    GetComponent<MeshRenderer>().material = materialArray[count];
                    canInput = false;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) || dph < 0 && previousDph <= 0)
                {
                    count--;
                    if (count < 0)
                    {
                        count = materialArray.Length - 1;
                    }
                    GetComponent<MeshRenderer>().material = materialArray[count];
                    canInput = false;
                }

                if (dpv > 0 && previousDpv <= 0)
                {
                    // 必要に応じて何かの処理を追加
                    canInput = false;
                }

                if (dpv < 0 && previousDpv >= 0)
                {
                    // 必要に応じて何かの処理を追加
                    canInput = false;
                }
            }

            if (dph == 0 && dpv == 0)
            {
                canInput = true;
            }

            previousDph = dph;
            previousDpv = dpv;

            // マテリアルの名前をチェックしてメニューを切り替える
            Material currentMaterial = materialArray[count];
            if (currentMaterial.name.Contains("StarSparrow_RED") || currentMaterial.name.Contains("StarSparrow_RED_1") || currentMaterial.name.Contains("StarSparrow_RED_2"))
            {
                colorMenuNumber_L.SetActive(false);
                colorMenuNumber_CENTER.SetActive(true);
                colorMenuNumber_R.SetActive(false);
            }
            else if (currentMaterial.name.Contains("StarSparrow_WHITE") || currentMaterial.name.Contains("StarSparrow_WHITE_1") || currentMaterial.name.Contains("StarSparrow_WHITE_2"))
            {
                colorMenuNumber_L.SetActive(true);
                colorMenuNumber_CENTER.SetActive(false);
                colorMenuNumber_R.SetActive(false);
            }
            else if (currentMaterial.name.Contains("StarSparrow_YELLOW") || currentMaterial.name.Contains("StarSparrow_YELLOW_1") || currentMaterial.name.Contains("StarSparrow_YELLOW_2"))
            {
                colorMenuNumber_L.SetActive(false);
                colorMenuNumber_CENTER.SetActive(false);
                colorMenuNumber_R.SetActive(true);
            }
        }
        else
        {
            colorMenuNumber_L.SetActive(false);
            colorMenuNumber_CENTER.SetActive(false);
            colorMenuNumber_R.SetActive(false);
        }
    }
}
