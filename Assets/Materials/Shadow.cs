using UnityEngine;

public class ShadowFade : MonoBehaviour
{
    [SerializeField] private float fixedY = 0.5f; // å≈íËÇ∑ÇÈYç¿ïW

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = fixedY;
        transform.position = pos;
    }
}
