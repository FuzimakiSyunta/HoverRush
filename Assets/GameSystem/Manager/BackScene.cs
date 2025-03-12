using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene : MonoBehaviour
{
    private float Timer;

    void Start()
    {
        Timer = 0.0f;
        Debug.Log("BackScene initialized. Timer reset.");
    }

    void Update()
    {
        Timer += Time.deltaTime;
        Debug.Log($"Timer: {Timer}, TimeScale: {Time.timeScale}");

        if (Timer >= 2.0f) // 2•bŒã‚ÉƒV[ƒ“‘JˆÚ
        {
            Debug.Log("Attempting to load PlayerMove scene...");
            SceneManager.LoadScene("PlayerMove");
        }
    }
}
