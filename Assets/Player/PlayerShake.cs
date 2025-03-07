using UnityEngine;

public class PLayerShake : MonoBehaviour
{
    private float amplitude = 0.5f; // 振幅を小さく設定
    private float frequency = 100f; // 周波数を高く設定
    private float dampingFactor = 6f; // 減衰係数
    private Vector3 startPos;
    private bool isShaking = false;
    private float shakeStartTime;

    //
    private PlayerScript playerScript;
    public GameObject player;

    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        startPos = player.transform.position;
    }

    void Update()
    {
        if (!isShaking && playerScript.IsDamege()==true)
        {
            StartShaking();
        }

        if (isShaking)
        {
            float time = Time.time - shakeStartTime;
            float damping = Mathf.Exp(-dampingFactor * time);
            float offset = amplitude * damping * Mathf.Sin(frequency * time);
            player.transform.position = startPos + new Vector3(offset, 0, 0);

            if (damping < 0.01f) // 減衰が十分に小さくなったら揺れを止める
            {
                StopShaking();
            }
        }
    }

    public void StartShaking()
    {
        startPos = player.transform.position;
        isShaking = true;
        shakeStartTime = Time.time;
    }

    public void StopShaking()
    {
        isShaking = false;
        player.transform.position = startPos;
    }

    public bool IsShaking()
    {
        return isShaking;
    }
}
