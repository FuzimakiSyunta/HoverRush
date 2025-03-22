using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControll : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;

    //select
    private SelectorMenu selectorMenuScript;
    public GameObject selectMenu;

    // プレイヤーを変数に格納
    public GameObject Player;
    // 回転させるスピード
    private float rotateSpeed = 20.0f;
    float angle;

    // カメラの上後方オフセット
    private Vector3 cameraUPBackOffset = new Vector3(0, 2.8f, 9.02f);
    // カメラの後方オフセット
    public Vector3 cameraBackOffset = new Vector3(0, 1, -200.0f);

    // カメラの後方オフセット
    private Vector3 cameraRightBackOffset = new Vector3(2.48f, 1.95f, 3.77f);


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        selectorMenuScript = selectMenu.GetComponent<SelectorMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        // 時間依存の移動
        if (gameManagerScript.IsGameStart() == false)
        {
            // Time.deltaTime を掛けて時間依存のスピードに
            angle = rotateSpeed * Time.deltaTime;
            // プレイヤー位置情報
            Vector3 playerPos = Player.transform.position;
            // カメラを回転させる
            transform.RotateAround(playerPos, Vector3.up, angle);
        }
        //ゲームスタートボタン押したら
        if (selectorMenuScript.IsStartButtonFlag() == true)
        {
            // プレイヤーの方を向く
            Vector3 direction = Player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

            // 後方に移動
            Vector3 targetPosition = Player.transform.position + cameraUPBackOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2.0f);
        }
        //ゲームスタートボタン押したら
        if (selectorMenuScript.IsColorMenuFlag() == true)
        {
            // プレイヤーの方を向く
            Vector3 direction = Player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
            // 右後方に移動
            Vector3 targetPosition = Player.transform.position + cameraRightBackOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2.0f);
        }
        //シーン推移したら
        if (selectorMenuScript.IsSeaneEffectFlag() == true)
        {
            // プレイヤーの方を向く
            Vector3 direction = Player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

            // Z方向に後退し続ける
            StartCoroutine(MoveBackForSeconds(2.0f));
        }

        IEnumerator MoveBackForSeconds(float duration)
        {
            float elapsedTime = 0.0f;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = startPosition + new Vector3(0, 0, -20.0f); // Z方向に下がる量を設定

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = endPosition; // 最終位置を正確に設定
        }


    }
}