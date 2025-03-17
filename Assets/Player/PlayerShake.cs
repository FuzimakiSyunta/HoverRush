using UnityEngine;

public class PlayerShake : MonoBehaviour
{
    private float amplitude = 0.2f; // 振幅を小さく設定
    private float frequency = 100f; // 周波数を高く設定
    private float dampingFactor = 3.95f; // 減衰係数
    private Vector3 shakeOffset = Vector3.zero; // 揺れのオフセットを保存
    private bool isShaking = false;
    private float shakeStartTime;

    private PlayerScript playerScript;
    public GameObject player;

    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
    }

    void Update()
    {
        // ダメージを受けた際にシェイクを開始
        if (!isShaking && playerScript.IsDamege() && playerScript.DamegeCoolTimer() <= 0.5f)
        {
            StartShaking();
        }

        // シェイク中の処理
        if (isShaking)
        {
            float time = Time.time - shakeStartTime;
            float damping = Mathf.Exp(-dampingFactor * time);
            float offset = amplitude * damping * Mathf.Sin(frequency * time);
            shakeOffset = new Vector3(offset, 0, 0);

            if (damping < 0.01f) // 減衰が十分小さくなったら終了
            {
                StopShaking();
            }
        }
        else
        {
            shakeOffset = Vector3.zero; // シェイクが終わったらオフセットをリセット
        }

        // シェイクの影響を加えつつプレイヤー位置を更新
        player.transform.position += shakeOffset;
    }

    public void StartShaking()
    {
        isShaking = true;
        shakeStartTime = Time.time;
    }

    public void StopShaking()
    {
        isShaking = false;
    }

    public bool IsShaking()
    {
        return isShaking;
    }
}
