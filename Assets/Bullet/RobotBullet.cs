using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    
    public Transform player; // プレイヤーオブジェクトのTransform
    public float moveSpeed = 5.0f; // 敵の移動速度

    // Start is called before the first frame update
    void Start()
    {
        

        //float moveSpeedZ = 125.0f;
        //float moveSpeedY = 60.0f;
        //float rotateZ = 45.0f;

        //rb.velocity = new Vector3(0, -moveSpeedY, -moveSpeedZ);
        //transform.rotation = Quaternion.Euler(0, 0, rotateZ); // // Y軸を中心に45°回転
        //Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // プレイヤーの位置を向く
            transform.LookAt(player);

            // プレイヤーに向かって移動
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

    }
}
