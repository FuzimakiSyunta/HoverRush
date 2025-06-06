using UnityEngine;

public class PlayerShake : MonoBehaviour
{
    private float amplitude = 0.15f;     // 揺れの大きさを3倍
    private float frequency = 100f;      // ややゆっくり揺れる
    private float dampingFactor = 3f;    // 減衰を緩やかに

    private Vector3 shakeOffset = Vector3.zero;
    private bool isShaking = false;
    private float shakeStartTime;

    private PlayerStatus playerStatus;
    public GameObject player;

    void Start()
    {
        playerStatus = player.GetComponent<PlayerStatus>();
    }

    void Update()
    {
        if (!isShaking && playerStatus.IsDamage() && playerStatus.DamegeCoolTimer() <= 0.5f)
        {
            StartShaking();
        }

        if (isShaking)
        {
            float time = Time.time - shakeStartTime;
            float damping = Mathf.Exp(-dampingFactor * time);
            float offset = amplitude * damping * Mathf.Sin(frequency * time);
            shakeOffset = new Vector3(offset, 0, 0);

            if (damping < 0.01f)
            {
                StopShaking();
            }
        }
        else
        {
            shakeOffset = Vector3.zero;
        }

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