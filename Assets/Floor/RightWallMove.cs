using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class RightWallMove : MonoBehaviour
{

    private float MoveSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        transform.position -= new Vector3(0, 0, MoveSpeed);
        if (transform.position.z <= -320)
        {
            transform.position = new Vector3(25.0f, 6.84f, 240.0f);
        }
    }
}
