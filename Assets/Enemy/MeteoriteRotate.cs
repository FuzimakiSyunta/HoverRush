using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 1, 0); // X²‚ğ’†S‚É45‹‰ñ“]AY²Z²‚Í‰Šú’l
    }
}
