using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class FloorMove : MonoBehaviour
{
    private GameManager gameManagerScript;
    private GameObject gameManager;
    private float MoveSpeed = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        
            transform.position -= new Vector3(0, 0, MoveSpeed);
            if (transform.position.z <= -40)
            {
                transform.position = new Vector3(0.0f, 0.0f, 80.0f);
            }
        
      
    }
}
