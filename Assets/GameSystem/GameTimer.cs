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
            // �Q�[���J�n���Ƀ^�C�}�[���J�n
            StartTimer();
        }

        if (isTimerRunning)
        {
            // �o�ߎ��Ԃ��X�V
            elapsedTime = (int)(Time.time - startTime);
            Debug.Log("�o�ߎ���: " + elapsedTime + "�b");

            // �Q�[���I�[�o�[�܂��̓N���A���Ƀ^�C�}�[���~
            if (gameManagerScript.IsGameOver() || gameManagerScript.IsGameClear())
            {
                StopTimer();
            }
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
            totalElapsedTime = (int)(Time.time - startTime);
            isTimerRunning = false; // �^�C�}�[��~
            Debug.Log("�^�C�}�[����~���܂����B���o�ߎ���: " + totalElapsedTime + "�b");
        }
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public float GetTotalElapsedTime()
    {
        return totalElapsedTime;
    }
}