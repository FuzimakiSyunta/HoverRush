using System.Collections;
using UnityEngine;

public class AfterimageEffect : MonoBehaviour
{
    public Material afterimageMaterial; // �l�I�����̃}�e���A��
    private float spawnInterval = 0.05f; // �c���𐶐�����Ԋu
    private float fadeDuration = 0.3f; // �c���̃t�F�[�h�A�E�g����

    public GameObject player; // �v���C���[�I�u�W�F�N�g
    private PlayerScript playerScript;

    private float timer;


    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval&&playerScript.IsMoveActive())
        {
            CreateAfterimage(); // �������g�̎c���𐶐�
            timer = 0f;
        }
    }

    void CreateAfterimage()
{
    // ���g�̌��݂̏�Ԃ��R�s�[
    GameObject afterimage = Instantiate(gameObject, transform.position, transform.rotation);

    // �X�P�[��������������
    afterimage.transform.localScale = transform.localScale * 0.25f; // �c��������50%�̃T�C�Y�ɐݒ�

    // �c���I�u�W�F�N�g�̐ݒ�
    foreach (var renderer in afterimage.GetComponentsInChildren<Renderer>())
    {
        renderer.material = afterimageMaterial; // �l�I�����̃}�e���A����K�p
    }

    // �s�v�ȃR���|�[�l���g���폜
    Destroy(afterimage.GetComponent<AfterimageEffect>()); // �c���͓��삵�Ȃ����߃X�N���v�g���폜
    Destroy(afterimage, fadeDuration); // ��莞�Ԍ�Ɏc�����폜

    StartCoroutine(FadeOut(afterimage)); // �t�F�[�h�A�E�g�������J�n
}

    private IEnumerator FadeOut(GameObject afterimage)
    {
        if (afterimage == null) yield break; // �����I��: afterimage��null�̏ꍇ

        Renderer[] renderers = afterimage.GetComponentsInChildren<Renderer>();
        Transform afterimageTransform = afterimage.transform; // �c����Transform���擾

        float fadeTimer = 0f;

        while (fadeTimer < fadeDuration)
        {
            // afterimage���j������Ă��邩���m�F
            if (afterimage == null || afterimageTransform == null) yield break;

            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.25f, 0f, fadeTimer / fadeDuration);
            float scale = Mathf.Lerp(0.25f, 0.25f, fadeTimer / fadeDuration); // �T�C�Y���k��

            afterimageTransform.localScale = Vector3.one * scale; // �T�C�Y��ύX

            foreach (var renderer in renderers)
            {
                if (renderer != null && renderer.material.HasProperty("_Color"))
                {
                    Color color = renderer.material.color;
                    color.a = alpha; // �����x��ݒ�
                    renderer.material.color = color;
                }
            }

            yield return null;
        }
    }

}