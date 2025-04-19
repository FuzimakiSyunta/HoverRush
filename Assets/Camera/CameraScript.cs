using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Camera mainCamera;
    public Camera subCamera;
   
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
                    StartCoroutine(SwitchCameraAfterDelay(2.0f)); // 2秒後に切り替え
                }
            }
        }

       
    }

    private IEnumerator SwitchCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        mainCamera.enabled = false; //タイトル用カメラ
        subCamera.enabled = true; //ゲーム用カメラ
    }

   
}