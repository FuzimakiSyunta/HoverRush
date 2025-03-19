using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PanelEffect : MonoBehaviour
{
    public Image panelImage; // �p�l����Image�R���|�[�l���g
    public float transitionDuration = 2f; // �F��ς���̂ɂ����鎞��

    private bool isChangingColor = false; // �F�ύX���̃t���O
    private bool hasTriggeredEffect = false; // �G�t�F�N�g�������������ǂ����̃t���O

    private SelectorMenu selectorMenuScript;
    public GameObject selectorMenu;
    private GameManager gameManagerScript;
    public GameObject gameManager;
    public GameObject Allui;
    private bool isWhite;
    private bool isAlpha;

    void Start()
    {
        selectorMenuScript = selectorMenu.GetComponent<SelectorMenu>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    private void Update()
    {
        // �t�F�[�h���łȂ��A���G�t�F�N�g���܂��������Ă��Ȃ��ꍇ�ɊJ�n
        if (selectorMenuScript.IsSeaneEffectFlag() == true && !isChangingColor && !hasTriggeredEffect)
        {
            StartCoroutine(ChangeColor());
        }
    }

    private IEnumerator ChangeColor()
    {
        isChangingColor = true; // �t���O���Z�b�g���đ��d���s��h�~
        hasTriggeredEffect = true; // �G�t�F�N�g�����t���O���Z�b�g

        // ���� (�A���t�@�l0) ���� �� (�A���t�@�l1)
        Color startColor = new Color(1f, 1f, 1f, 0f);
        Color endColor = new Color(1f, 1f, 1f, 1f);

        // �������甒�Ƀt�F�[�h
        yield return StartCoroutine(FadeColor(startColor, endColor));
        isWhite = true;
        isAlpha = false;
        Allui.SetActive(false);

        // �����ҋ@�i���̏�Ԃ��ێ����鎞�ԁj
        yield return new WaitForSeconds(2f);
        

        // �����瓧���Ƀt�F�[�h
        yield return StartCoroutine(FadeColor(endColor, startColor));
        isWhite = false;
        isAlpha = true;
        Allui.SetActive(true);
        gameManagerScript.GameStart();//�Q�[���X�^�[�g


        isChangingColor = false; // �F�ύX�t���O�����Z�b�g
    }

    private IEnumerator FadeColor(Color fromColor, Color toColor)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            panelImage.color = Color.Lerp(fromColor, toColor, elapsedTime / transitionDuration);
            yield return null;
        }

        // �ŏI�I�ȐF�𐳊m�ɐݒ�
        panelImage.color = toColor;
    }

    // �K�v�ɉ����ĊO������G�t�F�N�g��Ԃ����Z�b�g�\
    public void ResetEffect()
    {
        hasTriggeredEffect = false;
    }
    public bool IsWhite()
    {
        return isWhite;
    }
    public bool IsAlpha()
    {
        return isAlpha;
    }
}
