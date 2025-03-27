using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSelector : MonoBehaviour
{
    private PauseSystem pauseSystemScript;
    public GameObject pauseSystem;

    public GameObject SelectorImage;
    public RectTransform Selector;

    //ëÄçÏê‡ñæâÊëú
    public GameObject OperationImage;

    public bool isOperationSelect;
    private bool isTitleBackSelect;

    public bool isOperation = false;
    public bool isTitleBack = false;

    private float upperPosition = 107f;
    private float lowerPosition = -105.7f;

    void Start()
    {
        pauseSystemScript = pauseSystem.GetComponent<PauseSystem>();
        isOperation = false;
        isTitleBack = false;
        isOperationSelect = true;
        isTitleBackSelect = false;
        SelectorImage.SetActive(false);
        OperationImage.SetActive(false);
    }

    void Update()
    {
        if (pauseSystemScript.IsPaused())
        {
            SelectorImage.SetActive(true);

            HandleNavigation();
            HandleSelection();

            // isTitleBackÇ™trueÇ…Ç»Ç¡ÇΩÇÁÉ|Å[ÉYâèú
            if (isTitleBack)
            {
                Debug.Log("isTitleBack is true. Disabling pause.");
                pauseSystemScript.DisablePause();
            }
        }
        else
        {
            SelectorImage.SetActive(false);
        }
    }

    private void HandleNavigation()
    {
        float tri = Input.GetAxis("L_R_Trigger");

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || tri < 0)
        {
            Selector.anchoredPosition = new Vector2(Selector.anchoredPosition.x, upperPosition);
            isOperationSelect = true;
            isTitleBackSelect = false;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || tri > 0)
        {
            Selector.anchoredPosition = new Vector2(Selector.anchoredPosition.x, lowerPosition);
            isOperationSelect = false;
            isTitleBackSelect = true;
        }

        if(isOperation)//ëÄçÏê‡ñæ
        {
            OperationImage.SetActive(true);
            SelectorImage.SetActive(false);
            isOperationSelect = true;
            if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1"))
            {
                OperationImage.SetActive(false);
                isOperation =false;
            }
        }
        else
        {
            OperationImage.SetActive(false);
            SelectorImage.SetActive(true);
        }
        

    }

    private void HandleSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
        {
            if (isOperationSelect)
            {
                isOperation = true;
            }
            else if (isTitleBackSelect)
            {
                isTitleBack = true;
                Debug.Log("Selection: isTitleBack = true");
            }
        }
    }


    public bool IsTitleBack()
    {
        return isTitleBack;
    }

    public bool IsOperation()
    {
        return isOperation;
    }
}