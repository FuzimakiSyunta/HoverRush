using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIを操作するために必要

public class PlayerModels : MonoBehaviour
{
    public GameObject[] cubes; // 切り替えるCubeオブジェクトの配列
    public Image uiImage; // UIのImageコンポーネント
    public Sprite[] images; // 切り替える画像の配列

    private int currentIndex = 0; // 現在表示されているCubeのインデックス
    private bool canInput = true;  // 入力可能かどうかのフラグ
    private float inputCooldown = 0.3f; // クールダウン時間（秒）
    private float lastInputTime = 0f; // 最後に入力を受け付けた時間

    //select
    private SelectorMenu selectorMenuScript;
    public GameObject selectMenu;

    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    void Start()
    {
        //selector
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        UpdateCubeVisibility();
    }

    void Update()
    {
        if (selectorMenuScript.IsColorMenuFlag() == true && gameManagerScript.IsGameStart() == false)
        {
            float currentTime = Time.time;

            if (canInput && currentTime - lastInputTime > inputCooldown)
            {
                float verticalInput = Input.GetAxis("DPadVertical");

                if (Input.GetKeyDown(KeyCode.UpArrow) || verticalInput > 0)
                {
                    currentIndex = (currentIndex + 1) % cubes.Length;
                    UpdateCubeVisibility();
                    canInput = false;
                    lastInputTime = currentTime;
                }

                if (Input.GetKeyDown(KeyCode.DownArrow) || verticalInput < 0)
                {
                    currentIndex = (currentIndex - 1 + cubes.Length) % cubes.Length;
                    UpdateCubeVisibility();
                    canInput = false;
                    lastInputTime = currentTime;
                }
            }

            if (currentTime - lastInputTime > inputCooldown)
            {
                canInput = true;
            }
        }
    }

    void UpdateCubeVisibility()
    {
        // Cubeの切り替え
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].SetActive(i == currentIndex);
        }

        // UIの画像を同期
        if (uiImage != null && images.Length > currentIndex)
        {
            uiImage.sprite = images[currentIndex];
        }
    }

    // 現在のインデックスを取得
    public int IsIndex()
    {
        return currentIndex;
    }
}
