using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    public Image[] TutorialPage;
    public Image[] Button;
    private int currentIndex = 0;

    private bool istutorialOpen;
    //�`���[�g���A�����J���ꂽ��
    public bool IsTutorialCheck = true;

    // Start is called before the first frame update
    void Start()
    {
        istutorialOpen = false;
        IsTutorialCheck=true; // �`���[�g���A�����J���ꂽ���̃t���O��������
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (istutorialOpen)
        {
            // �ŏ��̈ꖇ��\��
            TutorialPage[currentIndex].gameObject.SetActive(true);
            //�{�^��
            for (int i = 0; i < 2; i++)
            {
                Button[i].gameObject.SetActive(true);
            }
            // Enter�L�[�������ꂽ�ꍇ�ɐ؂�ւ������s
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
            {
                SwitchImage();
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 2"))
            {
                istutorialOpen = false;
                gameManagerScript.GameStart();//�Q�[���J�n
                IsTutorialCheck = false; // �`���[�g���A�����I�������̂Ńt���O�����Z�b�g
            }
        }
        else
        {
            //�`���[�g���A��
            for (int i = 0; i < 5; i++)
            {
                TutorialPage[i].gameObject.SetActive(false);
            }
            //�{�^��
            for (int i = 0; i < 2; i++)
            {
                Button[i].gameObject.SetActive(false);
            }
        }
    }
    void SwitchImage()
    {
        // ���ݕ\������Image���\��
        TutorialPage[currentIndex].gameObject.SetActive(false);

        // ����Image��\��
        currentIndex = (currentIndex + 1) % TutorialPage.Length;
        TutorialPage[currentIndex].gameObject.SetActive(true);
    }


    public bool IsTutorialOpen()
    {
        return istutorialOpen;
    }
    public void TutorialStart()
    {
        istutorialOpen = true;
    }
    public bool IsTutorialCheckOpen()
    {
        return IsTutorialCheck;
    }
}
