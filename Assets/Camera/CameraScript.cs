using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Camera mainCamera;   // タイトル用
    public Camera subCamera;    // ゲーム導入 or チュートリアル用
    public Camera gameCamera;   // 通常プレイ用
    //public Camera bossCamera;   // ボス戦用

    // SELECT
    private SelectorMenu selectorMenuScript;
    public GameObject selectMenu;

    // PLAYER
    private PlayerScript playerScriptScript;
    public GameObject playerScript;

    // GAME MANAGER
    private GameManager gameManagerScript;
    public GameObject gameManager;

    // CAMERA MOVE (外部スクリプトで切り替えフラグを見る)
    private CameraMove cameraMoveScript;
    public GameObject cameraMove;

    private bool hasStarted = false;
    private bool hasSwitchedToGameCamera = false;

    // ゲームオーバー時回転用フラグ
    private bool isRotatingOnGameOver = false;

    void Start()
    {
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
        playerScriptScript = playerScript.GetComponent<PlayerScript>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        cameraMoveScript = cameraMove.GetComponent<CameraMove>();

        // 初期カメラ設定
        mainCamera.enabled = true;
        subCamera.enabled = false;
        gameCamera.enabled = false;
        //bossCamera.enabled = false;
    }

    void Update()
    {
        // --- スタートボタン押したら、2秒後に main → sub へ切り替え ---
        if (!hasStarted && selectorMenuScript.IsStartButtonFlag())
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                hasStarted = true;
                StartCoroutine(SwitchToSubCameraAfterDelay(2.0f));
            }
        }

        // --- チュートリアル完了などで sub → game へ切り替え ---
        if (!hasSwitchedToGameCamera && cameraMoveScript.IsCameraSwitch())
        {
            SwitchToGameCamera();
            hasSwitchedToGameCamera = true;
        }

        if (gameManagerScript.IsGameOver() && !isRotatingOnGameOver)
        {
            isRotatingOnGameOver = true;
            StartCoroutine(RotateCamera180(gameCamera.transform, 2.0f));
        }



    }

    IEnumerator SwitchToSubCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        mainCamera.enabled = false;
        subCamera.enabled = true;
    }

    void SwitchToGameCamera()
    {
        subCamera.enabled = false;
        gameCamera.enabled = true;
    }
    IEnumerator RotateCamera180(Transform target, float duration)
    {
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
}