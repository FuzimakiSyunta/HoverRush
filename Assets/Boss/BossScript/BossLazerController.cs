using UnityEngine;

public class BossLazerController : MonoBehaviour
{
    public GameObject finalLazer;
    private float timer = 0f;
    public float fireInterval = 5f;

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > fireInterval)
        {
            timer = 0f;
            if (finalLazer != null)
                finalLazer.SetActive(true);
        }
    }
}