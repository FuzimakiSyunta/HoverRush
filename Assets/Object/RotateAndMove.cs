using UnityEngine;

public class RotateAndMove : MonoBehaviour
{
    public float rotationSpeed = 90f; // 回転速度（度/秒）
    public float moveAmplitude = 1.3f; // 上下の振幅（移動量）
    public float moveFrequency = 1f; // 上下の頻度（動く速さ）

    

    void Start()
    {
        
    }

    void Update()
    {
        // 回転
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // 上下の動き（Sin波）
        float verticalOffset = Mathf.Sin(Time.time * moveFrequency) * moveAmplitude;
        
    }
}