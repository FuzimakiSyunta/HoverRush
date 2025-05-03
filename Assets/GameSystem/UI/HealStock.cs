using UnityEngine;
using UnityEngine.UI;

public class HealStockDisplay : MonoBehaviour
{
    public GameObject gameManager; // GameManager�I�u�W�F�N�g
    private GameManager gameManagerScript; // GameManager�̃X�N���v�g
    public Image[] healStockImages; // HealStock��\���摜�̔z��
    public Image healStockGage; // HealStock�̃Q�[�W
    public GameObject pausesystem; // PauseSystem�I�u�W�F�N�g
    private PauseSystem pauseSystemScript; // PauseSystem�̃X�N���v�g

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        pauseSystemScript = pausesystem.GetComponent<PauseSystem>();
    }

    void Update()
    {
        UpdateHealStockUI();
    }

    void UpdateHealStockUI()
    {
        // GameManager�̏�Ԃ��擾
        bool isGameStart = gameManagerScript.IsGameStart();
        bool isGameOver = gameManagerScript.IsGameOver();
        bool isGameClear = gameManagerScript.IsGameClear();

        // �Q�[�����X�^�[�g���Ă��Ȃ��A�܂��̓Q�[���I�[�o�[�A�܂��̓Q�[���N���A�̏ꍇ�͔�\��
        if (!isGameStart || isGameOver || isGameClear||pauseSystemScript.IsPaused())
        {
            foreach (var image in healStockImages)
            {
                image.enabled = false; // �S�Ĕ�\��
            }
            healStockGage.gameObject.SetActive(false); // �Q�[�W�����S�ɔ�\��
        }
        else
        {
            healStockGage.gameObject.SetActive(true); // �Q�[�W��\��
            // HealStock���ɉ����ĉ摜��\���E��\��
            int healCount = gameManagerScript.HealCount();
            for (int i = 0; i < healStockImages.Length; i++)
            {
                healStockImages[i].enabled = i < healCount; // �����t���\��
            }
        }
    }
}