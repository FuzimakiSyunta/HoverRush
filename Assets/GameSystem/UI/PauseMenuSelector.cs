using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSelector : MonoBehaviour
{
    private PauseSystem pauseSystemScript;
    public GameObject pauseSystem;

    public GameObject SelectorImage;
    public RectTransform Selector;

    public bool isRestartSelect;
    private bool isTitleBackSelect;

    public bool isRestart = false;
    public bool isTitleBack = false;

    private float upperPosition = 100f;
    private float lowerPosition = -100f;

    void Start()
    {
        pauseSystemScript = pauseSystem.GetComponent<PauseSystem>();
        isRestart = false;
        isTitleBack = false;
        isRestartSelect = false;
        isTitleBackSelect = false;
        SelectorImage.SetActive(false);
    }

    void Update()
    {
        if (pauseSystemScript.IsPaused())
        {
            SelectorImage.SetActive(true);

            HandleNavigation();
            HandleSelection();

            // isTitleBack‚ªtrue‚É‚È‚Á‚½‚çƒ|[ƒY‰ðœ
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

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || tri > 0)
        {
            Selector.anchoredPosition = new Vector2(Selector.anchoredPosition.x, upperPosition);
            isRestartSelect = true;
            isTitleBackSelect = false;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || tri < 0)
        {
            Selector.anchoredPosition = new Vector2(Selector.anchoredPosition.x, lowerPosition);
            isRestartSelect = false;
            isTitleBackSelect = true;
        }
    }

    private void HandleSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
        {
            if (isRestartSelect)
            {
                isRestart = true;
                Debug.Log("Selection: isRestart = true");
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
}