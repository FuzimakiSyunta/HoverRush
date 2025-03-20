using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] Material[] materialArray = new Material[3];
    private int count;
    private bool canInput = true;  // ���͉\���ǂ����������t���O
    private float previousDph = 0; // �O���D_Pad_H�̒l
    private float previousDpv = 0; // �O���D_Pad_V�̒l
    //select
    private SelectorMenu selectorMenuScript;
    public GameObject selectMenu;
    //���j���[���
    public GameObject colorMenuWHITE;
    public GameObject colorMenuRED;
    public GameObject colorMenuYELLOW;

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
                if (Input.GetKeyDown(KeyCode.LeftArrow) || dph < 0 && previousDph >= 0)
                {
                    count++;
                    if (count > materialArray.Length - 1)
                    {
                        count = 0;
                    }
                    GetComponent<MeshRenderer>().material = materialArray[count];
                    canInput = false;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow) || dph > 0 && previousDph <= 0)
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
                    // �K�v�ɉ����ĉ����̏�����ǉ�
                    canInput = false;
                }

                if (dpv < 0 && previousDpv >= 0)
                {
                    // �K�v�ɉ����ĉ����̏�����ǉ�
                    canInput = false;
                }
            }

            if (dph == 0 && dpv == 0)
            {
                canInput = true;
            }

            previousDph = dph;
            previousDpv = dpv;

            // �}�e���A���̖��O���`�F�b�N���ă��j���[��؂�ւ���
            Material currentMaterial = materialArray[count];
            if (currentMaterial.name.Contains("StarSparrow_WHITE")|| currentMaterial.name.Contains("StarSparrow_WHITE_1") || currentMaterial.name.Contains("StarSparrow_WHITE_2"))
            {
                colorMenuWHITE.SetActive(true);
                colorMenuRED.SetActive(false);
                colorMenuYELLOW.SetActive(false);
            }
            else if (currentMaterial.name.Contains("StarSparrow_RED") || currentMaterial.name.Contains("StarSparrow_RED_1") || currentMaterial.name.Contains("StarSparrow_RED_2"))
            {
                colorMenuWHITE.SetActive(false);
                colorMenuRED.SetActive(true);
                colorMenuYELLOW.SetActive(false);
            }
            else if (currentMaterial.name.Contains("StarSparrow_YELLOW") || currentMaterial.name.Contains("StarSparrow_YELLOW_1") || currentMaterial.name.Contains("StarSparrow_YELLOW_2"))
            {
                colorMenuWHITE.SetActive(false);
                colorMenuRED.SetActive(false);
                colorMenuYELLOW.SetActive(true);
            }
        }
        else
        {
            colorMenuWHITE.SetActive(false);
            colorMenuRED.SetActive(false);
            colorMenuYELLOW.SetActive(false);
        }
    }
}
