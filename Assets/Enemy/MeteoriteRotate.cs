using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteRotate : MonoBehaviour
{
    private float RotateSpeed = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = RotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(move, 0, 0); // // X²‚ğ’†S‚É45‹‰ñ“]
    }
}
