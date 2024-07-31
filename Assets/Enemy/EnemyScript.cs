using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        Destroy(gameObject, 5);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        int r = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = -18.0f;
        Vector3 velocity = new Vector3(0, 0, moveSpeed * Time.deltaTime);
        transform.position += transform.rotation * velocity;
    }
}
