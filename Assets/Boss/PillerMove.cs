using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillerMove : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f; // ��]���x�iY���j

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Y���ɂ�������]
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
