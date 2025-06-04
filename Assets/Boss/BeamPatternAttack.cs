using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPatternAttack : MonoBehaviour
{
    private GameTimer gameTimerScript;
    public GameObject gameTimer;

    public GameObject DamegePiller;

    public GameObject LeftPiller;
    public GameObject RightPiller;
    public GameObject CenterPiller;

    private bool started = false;

    void Start()
    {
        gameTimerScript = gameTimer.GetComponent<GameTimer>();
        DamegePiller.SetActive(false);
        SetAllInactive();
    }

    void Update()
    {
        // ŽžŠÔ‚É‚æ‚éƒsƒ‰[‚Ì•\Ž¦Ø‘Ö
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
    }

    IEnumerator SwitchPillers()
    {
        while (true)
        {
            LeftPiller.SetActive(true);
            CenterPiller.SetActive(false);
            RightPiller.SetActive(false);
            yield return new WaitForSeconds(3f);

            LeftPiller.SetActive(false);
            CenterPiller.SetActive(true);
            RightPiller.SetActive(false);
            yield return new WaitForSeconds(3f);

            LeftPiller.SetActive(false);
            CenterPiller.SetActive(false);
            RightPiller.SetActive(true);
            yield return new WaitForSeconds(3f);
        }
    }

    void SetAllInactive()
    {
        LeftPiller.SetActive(false);
        CenterPiller.SetActive(false);
        RightPiller.SetActive(false);
    }
}
