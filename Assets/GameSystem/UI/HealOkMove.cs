using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOkMove : MonoBehaviour
{
    public GameObject gameManager; // GameManagerオブジェクト
    private GameManager gameManagerScript; // GameManagerのスクリプト
    private Animator healOkAnimation; // HealOkのアニメーションコンポーネント

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        healOkAnimation = GetComponent<Animator>(); // Animator
    }

    // Update is called once per frame
    void Update()
    {
        PlayHealOkAnimation();
    }

    // HealOkのアニメーションを再生するメソッド
    private void PlayHealOkAnimation()
    {
        int healBatteryEnergy = gameManagerScript.GetHealBatteryEnargy();
        if (healBatteryEnergy >= 9)
        {
            healOkAnimation.SetBool("isHealOk", true);  // HealOkのアニメーションを再生
        }
        else
        {
            healOkAnimation.SetBool("isHealOk",false); 
        }
    }
}
