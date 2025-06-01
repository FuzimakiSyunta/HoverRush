using UnityEngine;
using UnityEngine.UI;

public class BossCameraImageMover : MonoBehaviour
{
    public RectTransform imageToMove;
    public GameObject gameManager;

    public Vector2 startPos = new Vector2(1000f, 0f);  // �E�X�^�[�g�ʒu
    public Vector2 endPos = new Vector2(-1000f, 0f);   // ���S�[���ʒu
    public float moveSpeed = 300f;

    private GameManager gameManagerScript;
    private CanvasGroup canvasGroup;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();

        if (imageToMove != null)
        {
            imageToMove.anchoredPosition = startPos;

            // CanvasGroup���擾�܂��͒ǉ�
            canvasGroup = imageToMove.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = imageToMove.gameObject.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0f; // �ŏ��͔�\��
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    void Update()
    {
        float count = gameManagerScript.IsGamePlayCount();
        bool isBossWave = gameManagerScript.IsBossWave();

        bool useBossCamera =
            (count >= 77.0f && count <= 101.0f) ||
            (isBossWave && count >= 18.0f && count <= 58.0f) ||
            (isBossWave && count >= 58.0f && count < 77.0f) ||
            (isBossWave && count >= 124.0f);

        if (useBossCamera)
        {
            if (canvasGroup.alpha == 0f)
            {
                imageToMove.anchoredPosition = startPos;
                canvasGroup.alpha = 1f;
            }

            imageToMove.anchoredPosition = Vector2.MoveTowards(
                imageToMove.anchoredPosition,
                endPos,
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            if (canvasGroup.alpha > 0f)
                canvasGroup.alpha = 0f;
        }
    }
}
