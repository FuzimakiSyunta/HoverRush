using System.Collections;
using UnityEngine;

public class WaveTextScript : MonoBehaviour
{
    private GameManager gameManagerScript;

    // WAVEごとのテキスト表示オブジェクト（Inspectorで設定）
    public GameObject[] waveTexts = new GameObject[6];

    // 各WAVEの表示済みフラグ（1回だけ表示するため）
    private bool[] waveShownFlags = new bool[6];

    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 初期化（すべて未表示）
        for (int i = 0; i < waveTexts.Length; i++)
        {
            waveTexts[i].SetActive(false);
            waveShownFlags[i] = false;
        }
    }

    void Update()
    {
        if (!gameManagerScript.IsGameStart()) return;

        int currentWave = gameManagerScript.IsWave();

        if (currentWave < waveShownFlags.Length && !waveShownFlags[currentWave])
        {
            StartCoroutine(ShowWaveText(currentWave));
            waveShownFlags[currentWave] = true;
        }
    }

    // テキストを2秒だけ表示して消す
    private IEnumerator ShowWaveText(int waveIndex)
    {
        waveTexts[waveIndex].SetActive(true);
        yield return new WaitForSeconds(2f);
        waveTexts[waveIndex].SetActive(false);
    }
}
