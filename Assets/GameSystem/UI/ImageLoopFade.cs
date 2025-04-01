using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SequentialImageFadeWithKeyStart : MonoBehaviour
{
    public Image[] images; // ���Ԃɕ\������摜�̔z��
    public float fadeDuration = 1f; // �t�F�[�h�ɂ����鎞��
    public float delayBetweenImages = 1f; // �e�摜�Ԃ̒x������
    private bool isProcessingStarted = false; // �������J�n���ꂽ���ǂ����̃t���O

    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;


    private void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        // �����J�n�O�ɂ��ׂẲ摜���\��
        HideAllImages();
    }

    private void Update()
    {
        // �X�y�[�X�L�[�ŏ������J�n
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
        // �ŏ���1���ڂ̉摜�����S�ɕ\��
        ShowFirstImage();

        // �x����Ƀt�F�[�h���[�v���J�n
        yield return new WaitForSeconds(delayBetweenImages);
        StartCoroutine(FadeImagesInSequence());
    }

    private void ShowFirstImage()
    {
        Color color = images[0].color;
        color.a = 1f; // ���S�ɕ\��
        images[0].color = color;
    }

    private IEnumerator FadeImagesInSequence()
    {
        int currentImageIndex = 0;

        while (true)
        {
            // ���݂̉摜���t�F�[�h�A�E�g
            yield return StartCoroutine(FadeOut(images[currentImageIndex]));

            // ���̉摜�Ɉړ�
            currentImageIndex = (currentImageIndex + 1) % images.Length;

            // ���̉摜���t�F�[�h�C��
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