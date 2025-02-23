using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeed : MonoBehaviour
{
    private float MeteoriteSpeedY = 0.01f;
    private float MeteoriteSpeedZ = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ˆÚ“®
        Vector3 position = transform.position;

        transform.position -= new Vector3(0, MeteoriteSpeedY, MeteoriteSpeedZ);
    }
}
