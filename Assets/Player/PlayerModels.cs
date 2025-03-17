using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModels : MonoBehaviour
{
    public GameObject[] cubes; // 切り替えるCubeオブジェクトの配列
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
            // 現在の時間を取得
            float currentTime = Time.time;

            // クールダウン確認
            if (canInput && currentTime - lastInputTime > inputCooldown)
            {
                // 上下の入力を受け取る
                float verticalInput = Input.GetAxis("DPadVertical");

                // 上キーで次の見た目に切り替え
                if (Input.GetKeyDown(KeyCode.UpArrow) || verticalInput > 0)
                {
                    currentIndex = (currentIndex + 1) % cubes.Length; // インデックスを進める
                    UpdateCubeVisibility();
                    canInput = false; // クールダウン開始
                    lastInputTime = currentTime; // 最後の入力時間を更新
                }

                // 下キーで前の見た目に切り替え
                if (Input.GetKeyDown(KeyCode.DownArrow) || verticalInput < 0)
                {
                    currentIndex = (currentIndex - 1 + cubes.Length) % cubes.Length; // インデックスを戻す
                    UpdateCubeVisibility();
                    canInput = false; // クールダウン開始
                    lastInputTime = currentTime; // 最後の入力時間を更新
                }
            }

            // クールダウンが終了したらcanInputをリセット
            if (currentTime - lastInputTime > inputCooldown)
            {
                canInput = true;
            }
        }
    }

    void UpdateCubeVisibility()
    {
        // 全てのCubeを非表示にして、現在のCubeだけ表示
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].SetActive(i == currentIndex);
        }
    }

    // 要素を取得する関数
    public int IsIndex()
    {
        return currentIndex;
    }
}
