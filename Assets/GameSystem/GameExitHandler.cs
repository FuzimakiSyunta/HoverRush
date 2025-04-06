using UnityEngine;

public class GameExitHandler : MonoBehaviour
{
    void Update()
    {
        // ESCキーが押された場合
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    // ゲームを終了するメソッド
    void QuitGame()
    {
#if UNITY_EDITOR
        // Unityエディタ用
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルド後のアプリケーション終了
        Application.Quit();
#endif
        Debug.Log("ESCキーが押されたため、ゲームを終了します。");
    }
}