using UnityEngine;

public class ShadowFade : MonoBehaviour
{
    [SerializeField] private float fixedY = 0.5f; // �Œ肷��Y���W

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = fixedY;
        transform.position = pos;
    }
}
