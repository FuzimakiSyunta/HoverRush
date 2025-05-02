using System.Collections;
using UnityEngine;

public class WaveTextScript : MonoBehaviour
{
    private GameManager gameManagerScript;

    // WAVE���Ƃ̃e�L�X�g�\���I�u�W�F�N�g�iInspector�Őݒ�j
    public GameObject[] waveTexts = new GameObject[6];

    // �eWAVE�̕\���ς݃t���O�i1�񂾂��\�����邽�߁j
    private bool[] waveShownFlags = new bool[6];

    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        // �������i���ׂĖ��\���j
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

    // �e�L�X�g��2�b�����\�����ď���
    private IEnumerator ShowWaveText(int waveIndex)
    {
        waveTexts[waveIndex].SetActive(true);
        yield return new WaitForSeconds(2f);
        waveTexts[waveIndex].SetActive(false);
    }
}
