using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    private float WheelSpeed=3.0f;
    private float RotateX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transformを取得
        Transform myTransform = this.transform;

        RotateX = RotateX + WheelSpeed;

        // ワールド座標基準で、現在の回転量へ加算する
        myTransform.Rotate(RotateX, 0.0f, 0.0f, Space.World);
    }
}
