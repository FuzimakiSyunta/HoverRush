using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    // Boss
    public GameObject boss;
    private BossScript bossScript;

    //アニメーション
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
        //animation
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossScript.IsFinalBattle())
        {
            animator.SetBool("isFinalBattle", true);
        }
        else
        {
            animator.SetBool("isFinalBattle", false);

        }
    }
}
