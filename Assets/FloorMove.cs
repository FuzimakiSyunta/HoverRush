using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;
    private float MoveSpeed = 0.10f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        if (gameManagerScript.IsGameOver() == true)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameOver() == true)
        {
            return;
        }
        transform.position -= new Vector3(0, 0, MoveSpeed);
        Vector3 position = transform.position;
        if (transform.position.z <= -40)
        {
            transform.position = new Vector3(0.0f, 0.0f, 80.0f);
        }
    }
}
