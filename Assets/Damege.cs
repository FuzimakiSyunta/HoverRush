using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Damege : MonoBehaviour
{
    [SerializeField] Image DamageImg;
    private PlayerScript playerScript;
    private Damege damege;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player"); //Playerっていうオブジェクトを探す
        playerScript = GetComponent<PlayerScript>(); //付いているスクリプトを取得
        damege = GetComponent<Damege>();
        DamageImg.color = Color.clear;
    }
    // Update is called once per frame
    void Update()
    {
        DamageImg.color = Color.Lerp(DamageImg.color, Color.clear, Time.deltaTime);
        Damaged();
    }
    void Damaged()
    {
       
         DamageImg.color = new Color(0.7f, 0, 0, 0.7f);
         return;
        
    }

}
