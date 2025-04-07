using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    public TextMeshProUGUI GameTimerText; // TextMeshPro�e�L�X�g

    public GameObject gameTimer;
    private GameTimer gameTimerScript;
    // Start is called before the first frame update
    void Start()
    {
        // GameManager�X�N���v�g�̎Q�Ƃ��擾
        gameTimerScript = gameTimer.GetComponent<GameTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���t���[�����݂̃G�l���M�[�l���擾���ăe�L�X�g�ɔ��f
        int currentEnergy = (int)gameTimerScript.GetElapsedTime();
        GameTimerText.text = currentEnergy.ToString();

    }
}
