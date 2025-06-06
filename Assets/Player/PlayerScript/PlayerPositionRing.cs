using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionRing : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    // Playerの位置を示すリング
    public GameObject playerPositionRing;

    // Start is called before the first frame update
    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        playerPositionRing.SetActive(false);// 初期状態では非表示
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameStart())
        {
            // ゲームが開始されている場合、Playerの位置を示すリングを表示
            playerPositionRing.SetActive(true);
        }
        else
        {
            // ゲームが開始されていない場合、Playerの位置を示すリングを非表示
            playerPositionRing.SetActive(false);
        }
    }
}
