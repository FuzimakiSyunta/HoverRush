using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBomm : MonoBehaviour
{
    private GameManager gameManagerScript;
    public GameObject gameManager;

    private PlayerScript playerScript;
    public GameObject player;

    private PlayerModels playerModelsScript;
    public GameObject playerModels;

    public GameObject explosionPrefab;
    public AudioClip explosionSound;

    private bool hasExploded = false;

    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        playerScript = player.GetComponent<PlayerScript>();
        playerModelsScript = playerModels.GetComponent<PlayerModels>();
    }

    void Update()
    {
        if (hasExploded) return;

        int index = playerModelsScript.IsIndex();

        // 正しいインデックスに応じたパワーアップ条件の判定
        if (index == 0 && playerScript.IsSinglePowerUp())
        {
            TriggerExplosion();
        }
        else if (index == 1 && playerScript.IsLazerPowerUp())
        {
            TriggerExplosion();
        }
        else if (index == 2 && playerScript.IsPenetrationPowerUp())
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
