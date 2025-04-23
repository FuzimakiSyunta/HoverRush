using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Camera mainCamera;
    public Camera subCamera;
    public Camera poworUpCamera;
    //select
    private SelectorMenu selectorMenuScript;
    public GameObject selectMenu;
    //Player
    private PlayerScript playerScriptScript;
    public GameObject playerScript;
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    void Start()
    {
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
        playerScriptScript = playerScript.GetComponent<PlayerScript>();
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera != null && subCamera != null && selectorMenuScript != null)
        {
            if (selectorMenuScript.IsStartButtonFlag())
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
                {
                    StartCoroutine(SwitchCameraAfterDelay(2.0f)); // 2�b��ɐ؂�ւ�
                }
            }
        }

        //if ((playerScriptScript.IsLazerShotChenge()&& gameManagerScript.GetBatteryEnargy() >= 20)||
        //    (playerScriptScript.IsPenetrationShotChenge() && gameManagerScript.GetBatteryEnargy() >= 30)||
        //    (playerScriptScript.IsSingleShotChenge() && gameManagerScript.GetBatteryEnargy() >= 25))
        //{
        //    if (poworUpCamera != null)
        //    {
        //        StartCoroutine(SwitchToPowerUpCamera()); // 2�b�ԃp���[�A�b�v�J������\��
        //    }
        //}
    }

    private IEnumerator SwitchCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        mainCamera.enabled = false; //�^�C�g���p�J����
        subCamera.enabled = true; //�Q�[���p�J����
    }

    private IEnumerator SwitchToPowerUpCamera()
    {
        poworUpCamera.enabled = true;
        mainCamera.enabled = false;
        subCamera.enabled = false;

        yield return new WaitForSeconds(2.0f);

        poworUpCamera.enabled = false;
        mainCamera.enabled = false; // �Q�[�����Ȃ� subCamera �ɖ߂�
        subCamera.enabled = true;
    }
}