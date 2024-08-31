using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;
    private bool Damage;
    private int DamegePoint;
    // Start is called before the first frame update
    void Start()
    {
        Damage = false;
        DamegePoint = 3;
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        Destroy(gameObject, 5);
        if (gameManagerScript.IsGameOver() == true)
        {
            return;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        int r = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameStart() == true)
        {
            float moveSpeed = -18.0f;
            Vector3 velocity = new Vector3(0, 0, moveSpeed * Time.deltaTime);
            transform.position += transform.rotation * velocity;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        //“G‚Æ’e
        if (other.gameObject.tag == "Bullet")
        {
            Damage = true;
            if (Damage == true)
            {
                DamegePoint = DamegePoint - 1;
                if (DamegePoint <= 0)
                {
                    Destroy(gameObject);
                    gameManagerScript.Score();
                }
            }

        }

        if (other.gameObject.tag == "Player")
        {
            float DamegeSpeed = 7.0f;
            Vector3 velocity = new Vector3(0, -DamegeSpeed * Time.deltaTime, 0);

        }


       

    }
}
