using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet_1 : MonoBehaviour
{
    private float moveSpeedZ = 40.0f;
    private float moveSpeedY = 0.5f;
    private float timeElapsed = 0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, 2f); 
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // ˆê’è‘¬“x ~ ŠÔ‚ÅˆÊ’u‚ğŒvZ
        Vector3 offset = new Vector3(0, -moveSpeedY * timeElapsed, -moveSpeedZ * timeElapsed);
        transform.position = startPosition + offset;

        // Y²‚Ì‰ñ“]‚ğí‚É -90“x‚ÉŒÅ’è
        transform.rotation = Quaternion.Euler(0, -180f, 0);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
