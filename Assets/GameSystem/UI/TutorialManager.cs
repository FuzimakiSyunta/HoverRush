using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    public Image[] TutorialPage;
    public Image[] Button;
    private int currentIndex = 0;

    private bool istutorialOpen;
    //チュートリアルが開かれたか
    public bool IsTutorialCheck = true;

    // Start is called before the first frame update
    void Start()
    {
        istutorialOpen = false;
        IsTutorialCheck=true; // チュートリアルが開かれたかのフラグを初期化
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (istutorialOpen)
        {
            // 最初の一枚を表示
            TutorialPage[currentIndex].gameObject.SetActive(true);
            //ボタン
            for (int i = 0; i < 2; i++)
            {
                Button[i].gameObject.SetActive(true);
            }
            // Enterキーが押された場合に切り替えを実行
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                SwitchImage();
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 2"))
            {
                istutorialOpen = false;
                gameManagerScript.GameStart();//ゲーム開始
                IsTutorialCheck = false; // チュートリアルが終了したのでフラグをリセット
            }
        }
        else
        {
            //チュートリアル
            for (int i = 0; i < 5; i++)
            {
                TutorialPage[i].gameObject.SetActive(false);
            }
            //ボタン
            for (int i = 0; i < 2; i++)
            {
                Button[i].gameObject.SetActive(false);
            }
        }
    }
    void SwitchImage()
    {
        // 現在表示中のImageを非表示
        TutorialPage[currentIndex].gameObject.SetActive(false);

        // 次のImageを表示
        currentIndex = (currentIndex + 1) % TutorialPage.Length;
        TutorialPage[currentIndex].gameObject.SetActive(true);
    }


    public bool IsTutorialOpen()
    {
        return istutorialOpen;
    }
    public void TutorialStart()
    {
        istutorialOpen = true;
    }
    public bool IsTutorialCheckOpen()
    {
        return IsTutorialCheck;
    }
}
