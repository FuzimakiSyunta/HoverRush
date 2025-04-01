using UnityEngine;

public class RotateAndMove : MonoBehaviour
{
    public float rotationSpeed = 90f; // ��]���x�i�x/�b�j
    public float moveAmplitude = 1.3f; // �㉺�̐U���i�ړ��ʁj
    public float moveFrequency = 1f; // �㉺�̕p�x�i���������j

    

    void Start()
    {
        
    }

    void Update()
    {
        // ��]
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // �㉺�̓����iSin�g�j
        float verticalOffset = Mathf.Sin(Time.time * moveFrequency) * moveAmplitude;
        
    }
}