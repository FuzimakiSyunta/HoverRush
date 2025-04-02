using TMPro;
using UnityEngine;

public class EnargyText : MonoBehaviour
{
    public TextMeshProUGUI enargyText; // TextMeshProテキスト
    // GameManager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    void Start()
    {
        // GameManagerスクリプトの参照を取得
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        // 毎フレーム現在のエネルギー値を取得してテキストに反映
        int currentEnergy = gameManagerScript.GetBatteryEnargy();
        enargyText.text = currentEnergy.ToString();

        //// Fキーが押されたときにエネルギーを増加
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    gameManagerScript.BatteryEnargyUp(); // エネルギーを1増加
        //}
    }
}