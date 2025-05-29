using UnityEngine;

public class ShadowFade : MonoBehaviour
{
    private float fixedY = 0; // å≈íËÇ∑ÇÈYç¿ïW

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = fixedY;
        transform.position = pos;
    }
}
