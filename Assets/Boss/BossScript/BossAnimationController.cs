using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    private Animator animator;
    private BossStatus bossStatus;

    void Start()
    {
        animator = GetComponent<Animator>();
        bossStatus = GetComponent<BossStatus>();
    }

    void Update()
    {
        if (bossStatus != null && bossStatus.IsDead())
        {
            animator.SetTrigger("Die");
        }
    }
}