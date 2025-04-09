using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealStock : MonoBehaviour
{

    public TextMeshProUGUI HealStockText; // TextMeshPro�e�L�X�g
    private GameManager gameManagerScript;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���t���[�����݂̃G�l���M�[�l���擾���ăe�L�X�g�ɔ��f
        int currentEnergy = (int)gameManagerScript.HealCount();
        HealStockText.text = currentEnergy.ToString();
    }
}
