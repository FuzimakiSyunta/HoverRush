using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPatternAttack : MonoBehaviour
{
    private GameTimer gameTimerScript;
    public GameObject gameTimer;

    public GameObject gameManager;
    private GameManager gameManagerScript;

    public GameObject DamegePiller;

    public GameObject LeftPiller;
    public GameObject RightPiller;
    public GameObject CenterPiller;

    public GameObject LeftQoadAllart;
    public GameObject RightQoadAllart;
    public GameObject CenterQoadAllart;

    private bool started = false;

    void Start()
    {
        gameTimerScript = gameTimer.GetComponent<GameTimer>();
        gameManagerScript = gameManager.GetComponent<GameManager>();
        DamegePiller.SetActive(false);
        DisableAllPillers();
    }

    void Update()
    {
        // ŠÔ‚É‚æ‚éƒsƒ‰[‚Ì•\¦Ø‘Ö
        if (!started && gameTimerScript.GetElapsedTime() >= 130)
        {
            DamegePiller.SetActive(true);
            StartCoroutine(SwitchPillers());
            started = true;
        }
        else if (!started)
        {
            DamegePiller.SetActive(false);
        }
        if(gameManagerScript.IsGameOver()||gameManagerScript.IsGameClear())
        {
            DamegePiller.SetActive(false);
            DisableAllPillers();
        }
    }

    IEnumerator SwitchPillers()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            //Leftƒsƒ‰[‚ÌŒx•UŒ‚
            yield return StartCoroutine(ShowAlertBeforeAttack(LeftQoadAllart));
            LeftPiller.SetActive(true);
            yield return new WaitForSeconds(3f);
            LeftPiller.SetActive(false);

            //Centerƒsƒ‰[‚ÌŒx•UŒ‚
            yield return StartCoroutine(ShowAlertBeforeAttack(CenterQoadAllart));
            CenterPiller.SetActive(true);
            yield return new WaitForSeconds(3f);
            CenterPiller.SetActive(false);

            //Rightƒsƒ‰[‚ÌŒx•UŒ‚
            yield return StartCoroutine(ShowAlertBeforeAttack(RightQoadAllart));
            RightPiller.SetActive(true);
            yield return new WaitForSeconds(3f);
            RightPiller.SetActive(false);
        }
    }

    IEnumerator ShowAlertBeforeAttack(GameObject alert)
    {
        float duration = 3f;
        float blinkInterval = 0.3f;
        float timer = 0f;

        while (timer < duration)
        {
            alert.SetActive(true);
            yield return new WaitForSeconds(blinkInterval);
            alert.SetActive(false);
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval * 2;
        }
    }


    void DisableAllPillers()
    {
        LeftPiller.SetActive(false);
        CenterPiller.SetActive(false);
        RightPiller.SetActive(false);
    }
}
