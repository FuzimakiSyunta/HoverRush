using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class FloorMove : MonoBehaviour
{
    private float MoveSpeed = 0.4f;
    //private float MoveSpeed = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        
        transform.position -= new Vector3(0, 0, MoveSpeed);
        if (transform.position.z <= -165)
        {
            transform.position = new Vector3(0.0f, 0.0f, 165.0f);
        }
        
      
    }
}
