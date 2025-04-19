using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteEnemySpeed : MonoBehaviour
{
   
    private float MeteoriteSpeedZ = 20.0f;

    //private float MeteoriteSpeedY = 0.015f;
    //private float MeteoriteSpeedZ = 0.09f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float moveZ = MeteoriteSpeedZ * Time.deltaTime;

        //ˆÚ“®
        Vector3 position = transform.position;

        transform.position -= new Vector3(0, 0, moveZ);
    }

    
    public float SpeedZ()
    {
        return MeteoriteSpeedZ * Time.deltaTime;
    }
}
