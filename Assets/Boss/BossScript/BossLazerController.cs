using System.Collections;
using UnityEngine;

public class BossLaserController : MonoBehaviour
{
    [Header("レーザーオブジェクト")]
    [SerializeField] private GameObject lazerL;
    [SerializeField] private GameObject lazerR;
    [SerializeField] private GameObject finalBossLazer;

    [Header("制御アニメーター")]
    [SerializeField] private Animator animator;

    private bool isFinalLaserActive = false;

    private void Start()
    {
        // 初期状態ではレーザーは非表示
        lazerL.SetActive(false);
        lazerR.SetActive(false);
        finalBossLazer.SetActive(false);
    }

    private void Update()
    {
        // レーザーの状態を更新
        HandleLaserWave();
        HandleFinalLaser();
    }

    private void HandleLaserWave()
    {
        // アニメーターの状態に基づいてレーザーを表示
        if (animator.GetBool("isLazer"))
        {
            lazerL.SetActive(true);
            lazerR.SetActive(true);
        }
        else
        {
            lazerL.SetActive(false);
            lazerR.SetActive(false);
        }
    }

    private void HandleFinalLaser()
    {
        // 最終波のレーザーの状態を更新
        if (animator.GetBool("FinalWave") && !isFinalLaserActive)
        {
            StartCoroutine(ActivateFinalLaserAfterDelay(4f));
        }
        else if (!animator.GetBool("FinalWave"))
        {
            finalBossLazer.SetActive(false);
            isFinalLaserActive = false;
        }
    }

    private IEnumerator ActivateFinalLaserAfterDelay(float delay)
    {
        // 最終波のレーザーを指定時間後にアクティブにする
        isFinalLaserActive = true;
        yield return new WaitForSeconds(delay);
        finalBossLazer.SetActive(true);
    }
}
