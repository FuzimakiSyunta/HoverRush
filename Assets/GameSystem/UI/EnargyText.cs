using TMPro;
using UnityEngine;

public class EnargyText : MonoBehaviour
{
    public TextMeshProUGUI enargyText; // TextMeshPro�e�L�X�g
    private GameManager gameManagerScript;
    public GameObject gameManager;

    private int previousEnergy = 0;
    private Vector3 originalScale;
    private float scaleDuration = 0.2f;
    private float scaleTimer = 0f;
    private bool isScaling = false;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        originalScale = enargyText.transform.localScale;
        previousEnergy = gameManagerScript.GetBatteryEnargy();
    }

    void Update()
    {
        int currentEnergy = gameManagerScript.GetBatteryEnargy();
        enargyText.text = currentEnergy.ToString();

        // ���l���������������g�剉�o
        if (currentEnergy > previousEnergy)
        {
            isScaling = true;
            scaleTimer = 0f;
        }

        // �g��k���̃A�j���[�V����
        if (isScaling)
        {
            scaleTimer += Time.deltaTime;
            float t = scaleTimer / scaleDuration;

            if (t < 0.5f)
            {
                // �g��
                enargyText.transform.localScale = Vector3.Lerp(originalScale, originalScale * 1.4f, t * 2f);
            }
            else if (t < 1f)
            {
                // �k��
                enargyText.transform.localScale = Vector3.Lerp(originalScale * 1.4f, originalScale, (t - 0.5f) * 2f);
            }
            else
            {
                // �I��
                enargyText.transform.localScale = originalScale;
                isScaling = false;
            }
        }

        previousEnergy = currentEnergy;
    }
}
