using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    // �^�[�Q�b�g�ƂȂ�I�u�W�F�N�g��ݒ�
    public Transform target;

    void Update()
    {
        if (target != null)
        {
            // �^�[�Q�b�g�̕���������
            transform.LookAt(target);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y-40f, 0);
        }
    }
}