using UnityEngine;
using UnityEngine.UI;

public class HealStockDisplay : MonoBehaviour
{
    public GameObject gameManager; // GameManagerオブジェクト
    private GameManager gameManagerScript; // GameManagerのスクリプト
    public Image[] healStockImages; // HealStockを表す画像の配列
    public Image healStockGage; // HealStockのゲージ
    public GameObject pausesystem; // PauseSystemオブジェクト
    private PauseSystem pauseSystemScript; // PauseSystemのスクリプト

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        pauseSystemScript = pausesystem.GetComponent<PauseSystem>();
    }

    void Update()
    {
        UpdateHealStockUI();
    }

    void UpdateHealStockUI()
    {
        // GameManagerの状態を取得
        bool isGameStart = gameManagerScript.IsGameStart();
        bool isGameOver = gameManagerScript.IsGameOver();
        bool isGameClear = gameManagerScript.IsGameClear();

        // ゲームがスタートしていない、またはゲームオーバー、またはゲームクリアの場合は非表示
        if (!isGameStart || isGameOver || isGameClear||pauseSystemScript.IsPaused())
        {
            foreach (var image in healStockImages)
            {
                image.enabled = false; // 全て非表示
            }
            healStockGage.gameObject.SetActive(false); // ゲージを完全に非表示
        }
        else
        {
            healStockGage.gameObject.SetActive(true); // ゲージを表示
            // HealStock数に応じて画像を表示・非表示
            int healCount = gameManagerScript.HealCount();
            for (int i = 0; i < healStockImages.Length; i++)
            {
                healStockImages[i].enabled = i < healCount; // 条件付き表示
            }
        }
    }
}