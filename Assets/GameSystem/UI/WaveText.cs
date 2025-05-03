using System.Collections;
using UnityEngine;

public class WaveTextScript : MonoBehaviour
{
    
    public GameObject[] leftImages;
    public GameObject[] rightImages;

    
    public AnimationCurve moveCurve;
    public float moveDuration = 2f;

    private GameManager gameManagerScript;
    private bool[] waveShownFlags = new bool[6];

    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gameManagerScript.IsGameStart()) return;

        int currentWave = gameManagerScript.IsWave();
        TryStartWaveAnimation(currentWave);
    }

    /// <summary>
    /// Wave番号に応じてアニメーションを開始する
    /// </summary>
    private void TryStartWaveAnimation(int waveIndex)
    {
        if (waveIndex < 0 || waveIndex >= waveShownFlags.Length) return;
        if (waveShownFlags[waveIndex]) return;

        GameObject left = GetImage(leftImages, waveIndex);
        GameObject right = GetImage(rightImages, waveIndex);

        if (left != null && right != null)
        {
            StartCoroutine(PlayWaveAnimation(left, right));
            waveShownFlags[waveIndex] = true;
        }
    }

    /// <summary>
    /// 指定インデックスの画像を配列から取得
    /// </summary>
    private GameObject GetImage(GameObject[] images, int index)
    {
        if (index >= 0 && index < images.Length) return images[index];
        return null;
    }

    /// <summary>
    /// 2つの画像を中央ですれ違わせるアニメーション
    /// </summary>
    private IEnumerator PlayWaveAnimation(GameObject leftImage, GameObject rightImage)
    {
        float elapsed = 0f;

        Transform leftTransform = leftImage.transform;
        Transform rightTransform = rightImage.transform;

        leftImage.SetActive(true);
        rightImage.SetActive(true);

        // Y位置とZ位置を保ったまま、X座標だけ移動させる
        Vector3 leftStart = new Vector3(-2000f, leftTransform.localPosition.y, leftTransform.localPosition.z);
        Vector3 leftEnd = new Vector3(2000f, leftTransform.localPosition.y, leftTransform.localPosition.z);

        Vector3 rightStart = new Vector3(2000f, rightTransform.localPosition.y, rightTransform.localPosition.z);
        Vector3 rightEnd = new Vector3(-2000f, rightTransform.localPosition.y, rightTransform.localPosition.z);

        leftTransform.localPosition = leftStart;
        rightTransform.localPosition = rightStart;

        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            float curveT = moveCurve.Evaluate(t);

            leftTransform.localPosition = Vector3.Lerp(leftStart, leftEnd, curveT);
            rightTransform.localPosition = Vector3.Lerp(rightStart, rightEnd, curveT);

            elapsed += Time.deltaTime;
            yield return null;
        }

        leftTransform.localPosition = leftEnd;
        rightTransform.localPosition = rightEnd;

        leftImage.SetActive(false);
        rightImage.SetActive(false);
    }

}
