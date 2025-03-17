using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModels : MonoBehaviour
{
    
    public GameObject[] cubes; // 切り替えるCubeオブジェクトの配列
    private int currentIndex = 0; // 現在表示されているCubeのインデックス

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
            // 上キーで次の見た目に切り替え
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentIndex = (currentIndex + 1) % cubes.Length; // インデックスを進める
                UpdateCubeVisibility();
            }

            // 下キーで前の見た目に切り替え
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentIndex = (currentIndex - 1 + cubes.Length) % cubes.Length; // インデックスを戻す
                UpdateCubeVisibility();
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

    //要素
    public int IsIndex()
    {
        return currentIndex;
    }
}
