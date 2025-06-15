using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionRing : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;
    // Player�̈ʒu�����������O
    public GameObject playerPositionRing;

    // Start is called before the first frame update
    void Start()
    {
        //gamemanager
        gameManagerScript = gameManager.GetComponent<GameManager>();
        playerPositionRing.SetActive(false);// ������Ԃł͔�\��
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.IsGameStart())
        {
            // �Q�[�����J�n����Ă���ꍇ�APlayer�̈ʒu�����������O��\��
            playerPositionRing.SetActive(true);
        }
        else
        {
            // �Q�[�����J�n����Ă��Ȃ��ꍇ�APlayer�̈ʒu�����������O���\��
            playerPositionRing.SetActive(false);
        }
    }
}
