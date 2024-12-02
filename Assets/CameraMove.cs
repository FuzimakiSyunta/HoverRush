using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float MoveSpeed = 0.04f;
    private bool Moving;

    // Start is called before the first frame update
    void Start()
    {
        Moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Moving = true;
        }
        if(Moving==true)
        {
            if (Input.GetKey(KeyCode.D) && transform.position.x <= 10)
            {
                transform.position += new Vector3(MoveSpeed, 0, 0);
            }
            else if (Input.GetKey(KeyCode.A) && transform.position.x >= -10)
            {
                transform.position += new Vector3(-MoveSpeed, 0, 0);
            }
        }
    }
}
