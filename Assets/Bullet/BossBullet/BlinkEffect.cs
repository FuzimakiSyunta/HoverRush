using UnityEngine;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    public Renderer targetRenderer;
    public Color flashColor = Color.white;
    public float flashDuration = 0.1f;

    private Material originalMaterial;
    private Color originalColor;

    void Start()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        originalMaterial = targetRenderer.material;
        originalColor = originalMaterial.color;
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        targetRenderer.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        targetRenderer.material.color = originalColor;
    }
}
