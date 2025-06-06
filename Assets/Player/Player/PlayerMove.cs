using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 18.0f;
    public GameObject MachingunFire;
    public GameObject LazerFire;
    public GameObject PanetrationFire;

    private bool isMoveActive = false;

    public bool IsMoveActive() => isMoveActive;

    public void Move()
    {
        float move = moveSpeed * Time.deltaTime;

        float stick = Input.GetAxis("Horizontal");
        float Vstick = Input.GetAxis("Vertical");
        float horizontalInput = stick + (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);
        float verticalInput = Vstick + (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0);

        Vector3 moveDir = Vector3.zero;

        if (horizontalInput > 0 && transform.position.x <= 10)
            moveDir.x = move;
        else if (horizontalInput < 0 && transform.position.x >= -10)
            moveDir.x = -move;

        if (verticalInput > 0 && transform.position.z <= 15)
            moveDir.z = move;
        else if (verticalInput < 0 && transform.position.z >= -8.5f)
            moveDir.z = -move;

        transform.position += moveDir;

        isMoveActive = (horizontalInput != 0 || verticalInput != 0);

        bool isForward = verticalInput > 0;
        MachingunFire.SetActive(isForward);
        LazerFire.SetActive(isForward);
        PanetrationFire.SetActive(isForward);
    }
}