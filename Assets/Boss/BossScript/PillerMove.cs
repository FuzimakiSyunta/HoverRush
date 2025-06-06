using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillerMove : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f; // âÒì]ë¨ìxÅiYé≤Åj

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Yé≤Ç…Ç‰Ç¡Ç≠ÇËâÒì]
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
