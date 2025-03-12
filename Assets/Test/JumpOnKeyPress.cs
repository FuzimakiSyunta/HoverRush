using UnityEngine;

public class JumpOnKeyPress : MonoBehaviour
{
    public float jumpForce = 10f; // �W�����v�̗͂̑傫��

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
