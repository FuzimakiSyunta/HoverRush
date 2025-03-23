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

    // Start is called before the first frame update
    void Start()
    {
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null && subCamera != null && selectorMenuScript != null)
        {
            if (selectorMenuScript.IsStartButtonFlag() == true)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
                {
                    StartCoroutine(SwitchCameraAfterDelay(2.0f)); // 2�b��ɐ؂�ւ�
                }
            }
        }
    }

    private IEnumerator SwitchCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        mainCamera.enabled = false;//�^�C�g���p�J����
        subCamera.enabled = true;//�Q�[���p�J����
    }
}