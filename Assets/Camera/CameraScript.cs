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
                    mainCamera.enabled = false;
                    subCamera.enabled = true;
                }
            }
        }
    }
}
