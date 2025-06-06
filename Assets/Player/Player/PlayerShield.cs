using System.Collections;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public GameObject Shield;
    public GameManager gameManagerScript;
    public PlayerStatus playerStatus;

    private bool isShieldActive = false;
    private Renderer shieldRenderer;

    void Start()
    {
        if (Shield != null)
        {
            shieldRenderer = Shield.GetComponent<Renderer>();
        }
    }

    public void TryActivateShield()
    {
        if (!isShieldActive &&
            gameManagerScript.GetBatteryEnargy() >= 30 &&
            (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown(KeyCode.Q)))
        {
            gameManagerScript.ShieldBatteryEnargy(); // エナジー消費
            StartCoroutine(HandleShield());
        }
    }

    private IEnumerator HandleShield()
    {
        isShieldActive = true;
        playerStatus.isShieldActive = true;
        Shield.SetActive(true);

        yield return new WaitForSeconds(2f); // 2秒間は通常表示

        // 点滅処理（1秒間に5回の点滅）
        float blinkTime = 1f;
        float interval = 0.2f;
        for (int i = 0; i < blinkTime / interval; i++)
        {
            if (Shield != null)
                Shield.SetActive(!Shield.activeSelf);
            yield return new WaitForSeconds(interval);
        }

        // 強制非表示と無敵解除
        if (Shield != null)
            Shield.SetActive(false);
        isShieldActive = false;
        playerStatus.isShieldActive = false;
    }
}