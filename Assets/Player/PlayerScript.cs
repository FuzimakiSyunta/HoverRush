using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float MoveSpeed = 0.02f;
    public GameObject bullet;
    float bulletTimer = 0.0f;
    private GameManager gameManagerScript;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)&& transform.position.x <= 10)
        {
            transform.position += new Vector3(MoveSpeed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)&& transform.position.x >= -10)
        {
            transform.position += new Vector3(-MoveSpeed, 0, 0);
        }
        else
        {
            transform.position += new Vector3(0, 0, 0);
        }
    }
    void FixedUpdate()
    {

        if (bulletTimer == 0.0f)
        {
            //’e”­ŽË
            if (Input.GetKey(KeyCode.Space))
            {
                Vector3 position = transform.position;
                position.y += 0.3f;
                position.z += 1.0f;
                Instantiate(bullet, position, Quaternion.identity);
                bulletTimer = 1.0f;
            }
        }
        else
        {
            bulletTimer++;
            if (bulletTimer > 5.0f)
            {
                bulletTimer = 0.0f;
            }
        }
    }
}
