using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        // 一時停止中にゲームオブジェクトを停止する例
        foreach (var obj in GameObject.FindGameObjectsWithTag("Pauseable"))
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = isPaused;
            }
        }
    }
}
