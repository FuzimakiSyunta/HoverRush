using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    private float MoveSpeed = 0.10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, 0, MoveSpeed);
        Vector3 position = transform.position;
        if (transform.position.z <= -40)
        {
            transform.position = new Vector3(0.0f, 0.0f, 80.0f);
        }
    }
}
