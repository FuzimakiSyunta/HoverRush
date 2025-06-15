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
            gameManagerScript.ShieldBatteryEnargy(); // �G�i�W�[����
            StartCoroutine(HandleShield());
        }
    }

    private IEnumerator HandleShield()
    {
        isShieldActive = true;
        playerStatus.isShieldActive = true;
        Shield.SetActive(true);

        yield return new WaitForSeconds(2f); // 2�b�Ԃ͒ʏ�\��

        // �_�ŏ����i1�b�Ԃ�5��̓_�Łj
        float blinkTime = 1f;
        float interval = 0.2f;
        for (int i = 0; i < blinkTime / interval; i++)
        {
            if (Shield != null)
                Shield.SetActive(!Shield.activeSelf);
            yield return new WaitForSeconds(interval);
        }

        // ������\���Ɩ��G����
        if (Shield != null)
            Shield.SetActive(false);
        isShieldActive = false;
        playerStatus.isShieldActive = false;
    }
}