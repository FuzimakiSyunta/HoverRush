using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossUIUpdater : MonoBehaviour
{
    [Header("UI要素")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Camera mainCamera;

    [Header("ダメージ画像")]
    [SerializeField] private GameObject bulletDamageImage;
    [SerializeField] private GameObject machinegunDamageImage;
    [SerializeField] private GameObject penetrationDamageImage;
    [SerializeField] private GameObject lazerDamageImage;
    [SerializeField] private GameObject lazerLDamageImage;
    [SerializeField] private GameObject lazerRDamageImage;

    [Header("設定")]
    [SerializeField] private float imageDisplayTime = 1.0f;

    private bool hpSliderActivated = false;

    private void Start()
    {
        SetSliderActive(false); // 初期状態では非表示
    }

    private void Update()
    {
        if (hpSlider != null && hpSlider.gameObject.activeSelf)
        {
            hpSlider.transform.rotation = mainCamera.transform.rotation;
        }
    }

    public void ShowDamageImage(string tag, Vector3 position)
    {
        ActivateHpSliderIfNeeded();

        GameObject image = GetDamageImage(tag);
        if (image == null) return;

        Vector3 adjustedPosition = position + new Vector3(0, 6.5f, 0);
        image.transform.position = adjustedPosition;
        image.SetActive(true);

        Image imageComponent = image.GetComponent<Image>();
        if (imageComponent != null)
        {
            StopAllCoroutines();
            Color tempColor = imageComponent.color;
            tempColor.a = 1.0f;
            imageComponent.color = tempColor;
            StartCoroutine(MoveUpward(image));
            StartCoroutine(FadeOutImage(imageComponent));
            StartCoroutine(ForceHideAllAfterDelay(0.5f));
        }
    }

    private void ActivateHpSliderIfNeeded()
    {
        if (!hpSliderActivated && hpSlider != null)
        {
            hpSlider.gameObject.SetActive(true);
            hpSliderActivated = true;
        }
    }

    public void SetSliderActive(bool active)
    {
        if (hpSlider != null)
        {
            hpSlider.gameObject.SetActive(active);
            hpSliderActivated = active;
        }
    }

    private GameObject GetDamageImage(string tag)
    {
        return tag switch
        {
            "Bullet" => bulletDamageImage,
            "Machinegun" => machinegunDamageImage,
            "PenetrationBullet" => penetrationDamageImage,
            "PlayerLazer" => lazerDamageImage,
            "PlayerLazer_L" => lazerLDamageImage,
            "PlayerLazer_R" => lazerRDamageImage,
            _ => null,
        };
    }

    private IEnumerator MoveUpward(GameObject imageObj)
    {
        float moveDuration = 1.0f;
        float elapsed = 0f;
        Vector3 start = imageObj.transform.position;
        Vector3 end = start + new Vector3(0, 5f, 0);

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            imageObj.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        imageObj.transform.position = end;
    }

    private IEnumerator FadeOutImage(Image imageComponent)
    {
        float fadeDuration = imageDisplayTime;
        float elapsed = 0f;
        Color originalColor = imageComponent.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsed / fadeDuration);
            originalColor.a = alpha;
            imageComponent.color = originalColor;
            yield return null;
        }

        imageComponent.gameObject.SetActive(false);
    }

    private IEnumerator ForceHideAllAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        bulletDamageImage.SetActive(false);
        machinegunDamageImage.SetActive(false);
        penetrationDamageImage.SetActive(false);
        lazerDamageImage.SetActive(false);
        lazerLDamageImage.SetActive(false);
        lazerRDamageImage.SetActive(false);
    }
}
