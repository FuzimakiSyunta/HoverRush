using TMPro;
using UnityEngine;

public class EnargyText : MonoBehaviour
{
    public TextMeshProUGUI enargyText; // TextMeshPro�e�L�X�g
    // GameManager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    void Start()
    {
        // GameManager�X�N���v�g�̎Q�Ƃ��擾
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        // ���t���[�����݂̃G�l���M�[�l���擾���ăe�L�X�g�ɔ��f
        int currentEnergy = gameManagerScript.GetBatteryEnargy();
        enargyText.text = currentEnergy.ToString();
    }
}