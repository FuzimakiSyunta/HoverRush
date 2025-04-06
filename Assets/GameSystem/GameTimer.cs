using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gameManagerScript;

    private int startTime;
    private int elapsedTime;
    public int totalElapsedTime;

    private bool isTimerRunning; // タイマーが進行中かどうかを判定

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        elapsedTime = 0;
        totalElapsedTime = 0;
        isTimerRunning = false; // 初期状態ではタイマー停止
    }

    void Update()
    {
        if (gameManagerScript.IsGameStart() && !isTimerRunning)
        {
            StartTimer(); // ゲーム開始時にタイマーを開始
        }

        if (isTimerRunning)
        {
            // 経過時間を更新
            elapsedTime = (int)(Time.time - startTime);
            Debug.Log("経過時間: " + elapsedTime + "秒");
        }

        if (gameManagerScript.IsGameClear())
        {
            // ゲームクリア時にタイマーを継続（タイマー停止処理を実行しない）
            Debug.Log("ゲームクリア！ タイマー継続中... 現在の経過時間: " + elapsedTime + "秒");
        }

        if (gameManagerScript.IsGameOver() && isTimerRunning)
        {
            // ゲームオーバー時のみタイマー停止
            StopTimer();
            Debug.Log("ゲームオーバー。総経過時間: " + totalElapsedTime + "秒");
        }
    }

    public void StartTimer()
    {
        if (!isTimerRunning) // 二重呼び出しを防止
        {
            startTime = (int)Time.time; // 現在の時間を記録
            isTimerRunning = true; // タイマー開始
            Debug.Log("タイマーが開始されました！");
        }
    }

    public void StopTimer()
    {
        if (isTimerRunning) // 二重呼び出しを防止
        {
            totalElapsedTime = (int)(Time.time - startTime); // 総経過時間を記録
            isTimerRunning = false; // タイマー停止
            Debug.Log("タイマーが停止しました。総経過時間: " + totalElapsedTime + "秒");
        }
    }

    public int GetElapsedTime()
    {
        return elapsedTime;
    }

    public int GetTotalElapsedTime()
    {
        return totalElapsedTime;
    }
}