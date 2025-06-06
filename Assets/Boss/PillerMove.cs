using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillerMove : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f; // ��]���x�iY���j
    private float scaleSpeed = 2f;     // �g��k���̑��x
    private float scaleAmount = 0.5f;  // �g��k���̕��i��: 0.5 �� X����0.5~1.5�{�ɂȂ�j
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale; // �����X�P�[�����L��
    }

    // Update is called once per frame
    void Update()
    {
        // Y���ɂ�������]
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        //// X���Ɋg��k���i�T�C���g�j
        //float scaleX = initialScale.x + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        //transform.localScale = new Vector3(scaleX, initialScale.y, initialScale.z);
    }
}
