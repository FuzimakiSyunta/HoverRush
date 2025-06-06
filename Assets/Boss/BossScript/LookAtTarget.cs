using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    // ターゲットとなるオブジェクトを設定
    public Transform target;

    void Update()
    {
        if (target != null)
        {
            // ターゲットの方向を向く
            transform.LookAt(target);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y-40f, 0);
        }
    }
}