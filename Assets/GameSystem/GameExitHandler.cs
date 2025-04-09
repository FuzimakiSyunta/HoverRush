using UnityEngine;

public class GameExitHandler : MonoBehaviour
{
    void Update()
    {
        // ESC�L�[�������ꂽ�ꍇ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    // �Q�[�����I�����郁�\�b�h
    void QuitGame()
    {
#if UNITY_EDITOR
        // Unity�G�f�B�^�p
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // �r���h��̃A�v���P�[�V�����I��
        Application.Quit();
#endif
        Debug.Log("ESC�L�[�������ꂽ���߁A�Q�[�����I�����܂��B");
    }
}