using UnityEngine;

public class ShadowFade : MonoBehaviour
{
    private float fixedY = 0; // �Œ肷��Y���W

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = fixedY;
        transform.position = pos;
    }
}
