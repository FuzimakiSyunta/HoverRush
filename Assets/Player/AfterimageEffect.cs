using System.Collections;
using UnityEngine;

public class AfterimageEffect : MonoBehaviour
{
    public Material afterimageMaterial; // �l�I�����̃}�e���A��
    private float spawnInterval = 0.05f; // �c���𐶐�����Ԋu
    private float fadeDuration = 0.3f; // �c���̃t�F�[�h�A�E�g����

    public GameObject player; // �v���C���[�I�u�W�F�N�g
    private PlayerStatus playerStatus;

    private float timer;

    void Start()
    {
        playerStatus = player.GetComponent<PlayerStatus>();
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && playerStatus.IsMoveActive())
        {
            CreateAfterimage(); // �������g�̎c���𐶐�
            timer = 0f;
        }
    }

    void CreateAfterimage()
    {
        GameObject afterimage = Instantiate(gameObject, transform.position, transform.rotation);
        afterimage.transform.localScale = transform.localScale * 0.25f;

        foreach (var renderer in afterimage.GetComponentsInChildren<Renderer>())
        {
            renderer.material = afterimageMaterial;
        }

        Destroy(afterimage.GetComponent<AfterimageEffect>());
        Destroy(afterimage, fadeDuration);

        StartCoroutine(FadeOut(afterimage));
    }

    private IEnumerator FadeOut(GameObject afterimage)
    {
        if (afterimage == null) yield break;

        Renderer[] renderers = afterimage.GetComponentsInChildren<Renderer>();
        Transform afterimageTransform = afterimage.transform;

        float fadeTimer = 0f;

        while (fadeTimer < fadeDuration)
        {
            if (afterimage == null || afterimageTransform == null) yield break;

            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.25f, 0f, fadeTimer / fadeDuration);
            float scale = Mathf.Lerp(0.25f, 0.25f, fadeTimer / fadeDuration);

            afterimageTransform.localScale = Vector3.one * scale;

            foreach (var renderer in renderers)
            {
                if (renderer != null && renderer.material.HasProperty("_Color"))
                {
                    Color color = renderer.material.color;
                    color.a = alpha;
                    renderer.material.color = color;
                }
            }

            yield return null;
        }
    }
}