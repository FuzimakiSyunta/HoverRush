using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealStock : MonoBehaviour
{

    public TextMeshProUGUI HealStockText; // TextMeshProテキスト
    private GameManager gameManagerScript;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // 毎フレーム現在のエネルギー値を取得してテキストに反映
        int currentEnergy = (int)gameManagerScript.HealCount();
        HealStockText.text = currentEnergy.ToString();
    }
}
