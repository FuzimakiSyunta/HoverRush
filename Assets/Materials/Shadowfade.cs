using UnityEngine;

public class ShadowFade : MonoBehaviour
{
    private float fixedY = 0; // 固定するY座標

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = fixedY;
        transform.position = pos;
    }
}
