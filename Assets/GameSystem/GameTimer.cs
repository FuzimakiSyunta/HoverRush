using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gameManagerScript;

    private int startTime;
    private int elapsedTime;
    public int totalElapsedTime;

    private bool isTimerRunning; // �^�C�}�[���i�s�����ǂ����𔻒�

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        elapsedTime = 0;
        totalElapsedTime = 0;
        isTimerRunning = false; // ������Ԃł̓^�C�}�[��~
    }

    void Update()
    {
        if (gameManagerScript.IsGameStart() && !isTimerRunning)
        {
            StartTimer(); // �Q�[���J�n���Ƀ^�C�}�[���J�n
        }

        if (isTimerRunning)
        {
            // �o�ߎ��Ԃ��X�V
            elapsedTime = (int)(Time.time - startTime);
            Debug.Log("�o�ߎ���: " + elapsedTime + "�b");
        }

        if (gameManagerScript.IsGameClear())
        {
            // �Q�[���N���A���Ƀ^�C�}�[���p���i�^�C�}�[��~���������s���Ȃ��j
            Debug.Log("�Q�[���N���A�I �^�C�}�[�p����... ���݂̌o�ߎ���: " + elapsedTime + "�b");
        }

        if (gameManagerScript.IsGameOver() && isTimerRunning)
        {
            // �Q�[���I�[�o�[���̂݃^�C�}�[��~
            StopTimer();
            Debug.Log("�Q�[���I�[�o�[�B���o�ߎ���: " + totalElapsedTime + "�b");
        }
    }

    public void StartTimer()
    {
        if (!isTimerRunning) // ��d�Ăяo����h�~
        {
            startTime = (int)Time.time; // ���݂̎��Ԃ��L�^
            isTimerRunning = true; // �^�C�}�[�J�n
            Debug.Log("�^�C�}�[���J�n����܂����I");
        }
    }

    public void StopTimer()
    {
        if (isTimerRunning) // ��d�Ăяo����h�~
        {
            totalElapsedTime = (int)(Time.time - startTime); // ���o�ߎ��Ԃ��L�^
            isTimerRunning = false; // �^�C�}�[��~
            Debug.Log("�^�C�}�[����~���܂����B���o�ߎ���: " + totalElapsedTime + "�b");
        }
    }

    public int GetElapsedTime()
    {
        return elapsedTime;
    }

    public int GetTotalElapsedTime()
    {
        return totalElapsedTime;
    }
}