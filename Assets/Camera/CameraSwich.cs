using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwich : MonoBehaviour
{
    public Camera mainCamera;
    public Camera subCamera;
    public Camera gameCamera;
    public Camera BossDethCamera;

    private SelectorMenu selectorMenuScript;
    public GameObject selectMenu;

    private PlayerStatus playerStatus;
    public GameObject playerScript;

    private GameManager gameManagerScript;
    public GameObject gameManager;

    private TutorialCameraMove cameraMoveScript;
    public GameObject cameraMove;

    private bool hasStarted = false;
    private bool hasSwitchedToGameCamera = false;
    private bool isRotatingOnGameOver = false;
    private bool hasHandledGameClear = false;

    void Start()
    {
        // 各スクリプトの取得
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
        playerStatus = playerScript.GetComponent<PlayerStatus>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        cameraMoveScript = cameraMove.GetComponent<TutorialCameraMove>();

        // 初期カメラ設定（すべてnullチェック付き）
        if (mainCamera != null) mainCamera.enabled = true;
        if (subCamera != null) subCamera.enabled = false;
        if (gameCamera != null) gameCamera.enabled = false;
        if (BossDethCamera != null) BossDethCamera.enabled = false;
    }

    void Update()
    {
        // スタートフラグが立っていて、スペースまたは決定ボタンが押されたら2秒後にサブカメラへ切り替え
        if (!hasStarted && selectorMenuScript.IsStartButtonFlag())
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                hasStarted = true;
                StartCoroutine(SwitchToSubCameraAfterDelay(2.0f));
            }
        }

        // カメラスイッチフラグが立ったらゲームカメラへ切り替え
        if (!hasSwitchedToGameCamera && cameraMoveScript.IsCameraSwitch())
        {
            SwitchToGameCamera();
            hasSwitchedToGameCamera = true;
        }

        // ゲームオーバー時はゲームカメラを180度回転
        if (gameManagerScript.IsGameOver() && !isRotatingOnGameOver)
        {
            isRotatingOnGameOver = true;
            StartCoroutine(RotateCamera180(gameCamera?.transform, 2.0f));
        }

        // ゲームクリア時にボス死亡カメラに切り替え＆スロー演出
        if (gameManagerScript.IsGameClear() && !hasHandledGameClear)
        {
            hasHandledGameClear = true;
            StartCoroutine(HandleGameClearCameraEffect());
        }
    }

    // 指定秒数後にサブカメラへ切り替える処理
    IEnumerator SwitchToSubCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (mainCamera != null) mainCamera.enabled = false;
        if (subCamera != null) subCamera.enabled = true;
    }

    // サブカメラ → ゲームカメラへ切り替え
    void SwitchToGameCamera()
    {
        if (subCamera != null) subCamera.enabled = false;
        if (gameCamera != null) gameCamera.enabled = true;
    }

    // ゲームオーバー時：カメラを180度回転させる演出
    IEnumerator RotateCamera180(Transform target, float duration)
    {
        if (target == null) yield break;

        Quaternion startRotation = target.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 180f, 0);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            target.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        target.rotation = endRotation;
    }

    // ゲームクリア時：ボス死亡カメラに切り替え、2秒間スロー演出
    IEnumerator HandleGameClearCameraEffect()
    {
        if (gameCamera != null)
            gameCamera.enabled = false; // 元のゲームカメラを無効化

        if (BossDethCamera != null)
            BossDethCamera.enabled = true; // ボス死亡カメラを有効化

        Time.timeScale = 0.5f; // スローモーションに設定

        yield return new WaitForSecondsRealtime(2f); // スロー時間をリアル時間で待機

        Time.timeScale = 1f; // タイムスケールを元に戻す
    }
}