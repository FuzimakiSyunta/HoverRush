using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private float RotateSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //R Stick
        float rsh = Input.GetAxis("R_Stick_H");
        float rsv = Input.GetAxis("R_Stick_V");
        ///‰ñ“]
        if (rsv > 0)
        {
            transform.rotation = Quaternion.Euler(0, RotateSpeed, 0);
        }
        else if (rsv < 0)
        {
            transform.rotation = Quaternion.Euler(0, -RotateSpeed, 0);
        }
        /////////////////////////////////////////////
    }
}
