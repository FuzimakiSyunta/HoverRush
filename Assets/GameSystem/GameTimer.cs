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
            // ゲーム開始時にタイマーを開始
            StartTimer();
        }

        if (isTimerRunning)
        {
            // 経過時間を更新
            elapsedTime = (int)(Time.time - startTime);
            Debug.Log("経過時間: " + elapsedTime + "秒");

            // ゲームオーバーまたはクリア時にタイマーを停止
            if (gameManagerScript.IsGameOver() || gameManagerScript.IsGameClear())
            {
                StopTimer();
            }
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
            totalElapsedTime = (int)(Time.time - startTime);
            isTimerRunning = false; // タイマー停止
            Debug.Log("タイマーが停止しました。総経過時間: " + totalElapsedTime + "秒");
        }
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public float GetTotalElapsedTime()
    {
        return totalElapsedTime;
    }
}