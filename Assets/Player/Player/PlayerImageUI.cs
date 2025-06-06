using UnityEngine;

public class PlayerImageUI : MonoBehaviour
{
    public GameManager gameManagerScript;
    public SelectorMenu selectorMenuScript;
    public GameObject HealImage;
    public GameObject NoHealImage;
    public GameObject HPSlider;
    public Animator animator;
    public GameObject HealCanvas;

    public void UpdateUI()
    {
        if (selectorMenuScript.IsColorMenuFlag())
        {
            HealImage.SetActive(false);
            NoHealImage.SetActive(false);
            HealCanvas.SetActive(false);
        }

        if (gameManagerScript.IsGameOver())
        {
            animator.SetBool("GameOver", true);
            HPSlider.SetActive(false);
            HealCanvas.SetActive(false);
        }
        else
        {
            animator.SetBool("GameOver", false);
        }

        if (gameManagerScript.IsGameStart())
        {
            HPSlider.SetActive(true);
            HealCanvas.SetActive(true);
        }
        else
        {
            HealCanvas.SetActive(false);
        }
    }

    public void SetHealImage(bool canHeal)
    {
        HealImage.SetActive(canHeal);
        NoHealImage.SetActive(!canHeal);
    }
}