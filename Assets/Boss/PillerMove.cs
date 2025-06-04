using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillerMove : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f; // 回転速度（Y軸）
    private float scaleSpeed = 2f;     // 拡大縮小の速度
    private float scaleAmount = 0.5f;  // 拡大縮小の幅（例: 0.5 → X軸が0.5~1.5倍になる）
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale; // 初期スケールを記憶
    }

    // Update is called once per frame
    void Update()
    {
        // Y軸にゆっくり回転
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        //// X軸に拡大縮小（サイン波）
        //float scaleX = initialScale.x + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        //transform.localScale = new Vector3(scaleX, initialScale.y, initialScale.z);
    }
}
