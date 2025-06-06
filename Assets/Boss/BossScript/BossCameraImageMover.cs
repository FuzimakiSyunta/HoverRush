using UnityEngine;
using UnityEngine.UI;

public class BossCameraImageMover : MonoBehaviour
{
    public RectTransform imageToMove;
    public GameObject gameManager;
    public RectTransform warnigBand; // UI警告バンド

    public Vector2 startPos = new Vector2(1000f, 0f);
    public Vector2 endPos = new Vector2(-1000f, 0f);

    private float moveSpeed = 1200f;       // メイン画像の移動速度
    private float bandMoveSpeed = 600f;    // バンドの移動速度

    private GameManager gameManagerScript;
    private CanvasGroup canvasGroup;
    private bool hasStartedMoving = false;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();

        if (imageToMove != null)
        {
            imageToMove.anchoredPosition = startPos;

            // CanvasGroupを取得または追加
            canvasGroup = imageToMove.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = imageToMove.gameObject.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        if (warnigBand != null)
        {
            warnigBand.anchoredPosition = startPos;
            warnigBand.gameObject.SetActive(false);
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
            if (!hasStartedMoving)
            {
                imageToMove.anchoredPosition = startPos;
                canvasGroup.alpha = 1f;

                if (warnigBand != null)
                {
                    warnigBand.anchoredPosition = startPos;
                    warnigBand.gameObject.SetActive(true);
                }

                hasStartedMoving = true;
            }

            imageToMove.anchoredPosition = Vector2.MoveTowards(
                imageToMove.anchoredPosition,
                endPos,
                moveSpeed * Time.deltaTime
            );

            if (warnigBand != null)
            {
                warnigBand.anchoredPosition = Vector2.MoveTowards(
                    warnigBand.anchoredPosition,
                    endPos,
                    bandMoveSpeed * Time.deltaTime
                );
            }

            if (Vector2.Distance(imageToMove.anchoredPosition, endPos) < 0.1f)
            {
                canvasGroup.alpha = 0f;

                if (warnigBand != null)
                    warnigBand.gameObject.SetActive(false);
            }
        }
        else
        {
            if (canvasGroup.alpha > 0f)
                canvasGroup.alpha = 0f;

            if (warnigBand != null && warnigBand.gameObject.activeSelf)
                warnigBand.gameObject.SetActive(false);

            hasStartedMoving = false;
        }
    }
}
