using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float MoveSpeed = 0.02f;
        //キーボード/////////////////////////////////
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(MoveSpeed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-MoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, MoveSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -MoveSpeed);
        }
        //////////////////////////////////////////////
    }
}
