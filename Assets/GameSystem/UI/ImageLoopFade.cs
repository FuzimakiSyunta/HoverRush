using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SequentialImageFadeWithKeyStart : MonoBehaviour
{
    public Image[] images; // 順番に表示する画像の配列
    public float fadeDuration = 1f; // フェードにかかる時間
    public float delayBetweenImages = 1f; // 各画像間の遅延時間
    private bool isProcessingStarted = false; // 処理が開始されたかどうかのフラグ

    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;


    private void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        // 処理開始前にすべての画像を非表示
        HideAllImages();
    }

    private void Update()
    {
        // スペースキーで処理を開始
        if (!isProcessingStarted && gameManagerScript.IsOpenSelector())
        {
            isProcessingStarted = true;
            StartCoroutine(StartFadeSequence());
        }
    }

    private void HideAllImages()
    {
        foreach (Image image in images)
        {
            Color color = image.color;
            color.a = 0f;
            image.color = color;
        }
    }

    private IEnumerator StartFadeSequence()
    {
        // 最初の1枚目の画像を完全に表示
        ShowFirstImage();

        // 遅延後にフェードループを開始
        yield return new WaitForSeconds(delayBetweenImages);
        StartCoroutine(FadeImagesInSequence());
    }

    private void ShowFirstImage()
    {
        Color color = images[0].color;
        color.a = 1f; // 完全に表示
        images[0].color = color;
    }

    private IEnumerator FadeImagesInSequence()
    {
        int currentImageIndex = 0;

        while (true)
        {
            // 現在の画像をフェードアウト
            yield return StartCoroutine(FadeOut(images[currentImageIndex]));

            // 次の画像に移動
            currentImageIndex = (currentImageIndex + 1) % images.Length;

            // 次の画像をフェードイン
            yield return StartCoroutine(FadeIn(images[currentImageIndex]));
            yield return new WaitForSeconds(delayBetweenImages);
        }
    }

    private IEnumerator FadeIn(Image image)
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1f;
        image.color = color;
    }

    private IEnumerator FadeOut(Image image)
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 0f;
        image.color = color;
    }
}