using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerMove playerMove;
    private PlayerShot playerShot;
    private PlayerHover playerHover;
    private PlayerShield playerShield;
    private PlayerDamage playerDamage;
    private PlayerHeal playerHeal;
    private PlayerImageUI playerImageUI;

    private GameManager gameManagerScript;
    public GameObject gameManager; // Å© InspectorÇ≈ê›íËïKóv

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerShot = GetComponent<PlayerShot>();
        playerHover = GetComponent<PlayerHover>();
        playerShield = GetComponent<PlayerShield>();
        playerDamage = GetComponent<PlayerDamage>();
        playerHeal = GetComponent<PlayerHeal>();
        playerImageUI = GetComponent<PlayerImageUI>();

        if (gameManager != null)
            gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManagerScript.IsGameStart())
        {
            if (playerMove != null)
            {
                playerMove.Move();
            }

            if (playerHover != null)
            {
                playerHover.Hover();
            }
        }
        if (playerImageUI != null)
        {
            playerImageUI.UpdateUI();
        }

        if (playerShot != null)
        {
            playerShot.UpdateShotPattern();
        }

        if (playerShield != null)
        {
            playerShield.TryActivateShield();
        }

        if (playerDamage != null)
        {
            playerDamage.DamageUpdate();
        }

        if (playerHeal != null)
        {
            playerHeal.Heal();
        }
    }

    void FixedUpdate()
    {
        if (playerShot != null)
        {
            playerShot.HandleFixedUpdate();
        }
    }
}