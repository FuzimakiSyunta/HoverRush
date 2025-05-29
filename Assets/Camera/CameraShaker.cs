using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float shakeMagnitude = 0.2f;

    private Vector3 originalPos;
    private float shakeDuration = 0f;
    private bool continuousShake = false;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (continuousShake || shakeDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;

            if (!continuousShake)
            {
                shakeDuration -= Time.unscaledDeltaTime;
                if (shakeDuration <= 0)
                {
                    transform.localPosition = originalPos;
                }
            }
        }
        else
        {
            transform.localPosition = originalPos;
        }
    }

    public void ShakeOnce(float duration = 0.3f, float magnitude = 0.2f)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    public void StartContinuousShake(float magnitude = 0.2f)
    {
        continuousShake = true;
        shakeMagnitude = magnitude;
    }

    public void StopContinuousShake()
    {
        continuousShake = false;
    }
}
