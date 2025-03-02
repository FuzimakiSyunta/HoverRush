using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDebug : MonoBehaviour
{
    //Enemy
    private EnemyScript enemy;
    public GameManager enemyScript;
    //NomalEnemy
    private NomalEnemy nomalEnemy;
    public GameManager nomalEnemyScript;
    //Meteorite
    private MeteoriteEnemySpeed meteoriteEnemySpeed;
    public GameManager meteoriteEnemySpeedScript;
    //Player
    private PlayerScript player;
    public GameManager playerScript;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = enemy.GetComponent<GameManager>();
        nomalEnemy = nomalEnemyScript.GetComponent<NomalEnemy>();
        meteoriteEnemySpeed = meteoriteEnemySpeedScript.GetComponent<MeteoriteEnemySpeed>();
        player = playerScript.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
