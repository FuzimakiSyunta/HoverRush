using UnityEngine;

public class PlayerHover : MonoBehaviour
{
    public GameObject HoverParticle;
    private float verticalSpeed = 0f;
    private float jumpPower = 50f;
    private float gravity = 9.8f;
    private float maxHeight = 5.6f;
    private float minHeight = 0.5f;

    public void Hover()
    {
        float RT = Input.GetAxis("RightTrigger");
        Vector3 pos = transform.position;

        if (RT > 0 || Input.GetKey(KeyCode.Space) && pos.y < maxHeight)
            verticalSpeed = jumpPower;
        else
            verticalSpeed -= gravity * Time.deltaTime;

        pos.y += verticalSpeed * Time.deltaTime;

        if (pos.y < minHeight)
        {
            pos.y = minHeight;
            verticalSpeed = 0f;
            HoverParticle.SetActive(false);
        }
        else if (pos.y > maxHeight)
        {
            pos.y = maxHeight;
            verticalSpeed = 0f;
            HoverParticle.SetActive(true);
        }

        transform.position = pos;
    }
}