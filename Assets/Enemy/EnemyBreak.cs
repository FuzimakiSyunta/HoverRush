using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBreak : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    public float forceAmount = 150f; // どのくらいの力でオブジェクトを飛ばすか

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = new Vector3(
                    Random.Range(-50f, 50f),
                    Random.Range(0f, 250f),
                    Random.Range(1f, -1f)
                ).normalized;

                rb.AddForce(randomDirection * forceAmount, ForceMode.Impulse);
            }
        }
    }
}
