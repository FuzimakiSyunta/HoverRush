using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class FloorMove : MonoBehaviour
{
    private float MoveSpeed = 80.0f;
    //private float MoveSpeed = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ŽžŠÔˆË‘¶‚ÌˆÚ“®
        float move = MoveSpeed * Time.deltaTime;

        Vector3 position = transform.position;
        
        transform.position -= new Vector3(0, 0, move);
        if (transform.position.z <= -165)
        {
            transform.position = new Vector3(0.0f, 0.0f, 165.0f);
        }
        
      
    }
}
