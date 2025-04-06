using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    public TextMeshProUGUI GameTimerText; // TextMeshProテキスト

    public GameObject gameTimer;
    private GameTimer gameTimerScript;
    // Start is called before the first frame update
    void Start()
    {
        // GameManagerスクリプトの参照を取得
        gameTimerScript = gameTimer.GetComponent<GameTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 毎フレーム現在のエネルギー値を取得してテキストに反映
        int currentEnergy = (int)gameTimerScript.GetElapsedTime();
        GameTimerText.text = currentEnergy.ToString();

    }
}
