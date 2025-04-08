using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    public Image[] TutorialPage;
    private int currentIndex = 0;

    private bool istutorialOpen;
    // Start is called before the first frame update
    void Start()
    {
        istutorialOpen = false;
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (istutorialOpen)
        {
            // �ŏ��̈ꖇ��\��
            TutorialPage[currentIndex].gameObject.SetActive(true);
            // Enter�L�[�������ꂽ�ꍇ�ɐ؂�ւ������s
            if (Input.GetKeyDown(KeyCode.Return)) // KeyCode.Return��Enter�L�[���w���܂�
            {
                SwitchImage();
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                istutorialOpen = false;
                gameManagerScript.GameStart();//�Q�[���J�n
            }
        }
        else
        {
           for (int i = 0; i < 3; i++)
           {
               TutorialPage[i].gameObject.SetActive(false);
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
        istutorialOpen=true;
    }
}
