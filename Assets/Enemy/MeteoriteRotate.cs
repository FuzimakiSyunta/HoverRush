using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteRotate : MonoBehaviour
{
    private float RotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateSpeed -= 0.5f;
        transform.rotation = Quaternion.Euler(RotateSpeed, 0, 0); // // X���𒆐S��45����]
    }
}
