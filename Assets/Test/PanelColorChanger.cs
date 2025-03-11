using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelColorChanger : MonoBehaviour
{
    public Image panelImage; // �p�l����Image�R���|�[�l���g
    public float transitionDuration = 1f; // �F��ς���̂ɂ����鎞��
    private bool isTriggered = false; // �����t���O

    private GameManager gameManagerScript;
    public GameObject gameManager;


    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    private void Update()
    {
        // Enter�L�[�i�܂���Return�L�[�j�������ꂽ�Ƃ��ɔ���
        if (Input.GetKeyDown(KeyCode.Return) && !isTriggered)
        {
            isTriggered = true; // �����t���O���Z�b�g
            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor()
    {
        // ���� (�A���t�@�l0)
        Color startColor = new Color(1f, 1f, 1f, 0f);
        // �� (�A���t�@�l1)
        Color endColor = new Color(1f, 1f, 1f, 1f);

        // �������甒�Ƀt�F�[�h
        yield return StartCoroutine(FadeColor(startColor, endColor));

        // �����ҋ@
        yield return new WaitForSeconds(1f);

        // �����瓧���Ƀt�F�[�h
        yield return StartCoroutine(FadeColor(endColor, startColor));

        // �����t���O�����Z�b�g
        isTriggered = false;
    }

    private IEnumerator FadeColor(Color fromColor, Color toColor)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            // �F���Ԃ��Đݒ�
            panelImage.color = Color.Lerp(fromColor, toColor, elapsedTime / transitionDuration);
            yield return null;
        }

        // �ŏI�I�ȐF�𐳊m�ɐݒ�
        panelImage.color = toColor;
    }
}
