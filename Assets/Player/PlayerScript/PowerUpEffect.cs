using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;

    private PlayerStatus playerStatus;
    public GameObject player;

    private PlayerModels playerModelsScript;
    public GameObject playerModels;

    public GameObject explosionPrefab;
    public AudioClip explosionSound;

    private bool hasExploded = false;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        playerStatus = player.GetComponent<PlayerStatus>();
        playerModelsScript = playerModels.GetComponent<PlayerModels>();
    }

    void Update()
    {
        if (hasExploded) return;

        int index = playerModelsScript.IsIndex();

        if (index == 0 && playerStatus.IsSinglePoweredUp())
        {
            TriggerExplosion();
        }
        else if (index == 1 && playerStatus.IsLaserPoweredUp())
        {
            TriggerExplosion();
        }
        else if (index == 2 && playerStatus.IsPenetrationPoweredUp())
        {
            TriggerExplosion();
        }
    }

    void TriggerExplosion()
    {
        hasExploded = true;

        GameObject explosion = Instantiate(explosionPrefab, player.transform.position, Quaternion.identity);
        Destroy(explosion, 1f);

        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
    }
}