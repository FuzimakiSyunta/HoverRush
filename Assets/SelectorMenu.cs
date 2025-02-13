using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    public RectTransform Selector;
    public GameObject SelectorImage;
    public bool LuleFlag = false;
    public bool StartFlag = true;
    public RectTransform LuleImage;
    private float move = 1.0f;
    private float selectormove = 100.0f;
    public RectTransform StartImage;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        //selector
        SelectorImage.SetActive(false);
        StartFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.IsOpenSelector()==true)
        {
            //‰æ‘œˆÚ“®
            if (LuleImage.position.x >= 150.0f)
            {
                move -= 1.0f;
                SelectorImage.SetActive(true);

                //Selector
                if (StartFlag == true)
                {
                    
                    if (StartFlag == true &&Input.GetKeyDown(KeyCode.S))
                    {
                        Selector.position += new Vector3(0, -selectormove, 0);
                        StartFlag = false;
                        LuleFlag = true;
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        gameManagerScript.GameStart();
                    }
                }
                else
                {
                    if (LuleFlag == true&&Input.GetKeyDown(KeyCode.W))
                    {
                        Selector.position += new Vector3(0, selectormove, 0);
                        LuleFlag = false;
                        StartFlag = true;

                    }
                }
            }
            else
            {
                LuleImage.position += new Vector3(move, 0, 0);
                StartImage.position += new Vector3(move, 0, 0);
            }
        }
        
    }
    public bool IsLuleFlag()
    {
        return LuleFlag;
    }
    public bool IsStartFlag()
    {
        return StartFlag;
    }
}
