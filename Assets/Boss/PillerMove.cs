using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillerMove : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f; // ñ]¬xiY²j

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Y²ÉäÁ­èñ]
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
