using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    //gamemanager
    private GameManager gameManagerScript;
    public GameObject gameManager;

    //Enemy
    public GameObject attackEnemy;
    public GameObject meteoriteEnemy;
    public GameObject healPlaneEnemy;
    public GameObject hoverCarEnemy;
    public GameObject powerCarEnemy;

    //�G�o��
    private int currentPatternIndex = 0;
    private float spawnTimer = 0.0f;
    private float spawnCooldown = 2.0f; // 2�b���ƂɓG���o��

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 5��ނ̓G�z�u�p�^�[��
    private Vector3[][] enemyPatterns = new Vector3[][]
    {
    new Vector3[] { new Vector3(-8.0f, 1.5f, 45.0f), new Vector3(0.0f, 1.5f, 45.0f), new Vector3(8.0f, 1.5f, 45.0f) },
    new Vector3[] { new Vector3(-4.0f, 1.5f, 45.0f), new Vector3(4.0f, 1.5f, 45.0f), new Vector3(0.0f, 1.5f, 45.0f) },
    new Vector3[] { new Vector3(-8.0f, 1.5f, 45.0f), new Vector3(8.0f, 1.5f, 45.0f), new Vector3(0.0f, 1.5f, 45.0f) },
    new Vector3[] { new Vector3(-8.0f, 1.5f, 45.0f), new Vector3(-4.0f, 1.5f, 45.0f), new Vector3(4.0f, 1.5f, 45.0f) },
    new Vector3[] { new Vector3(-4.0f, 1.5f, 45.0f), new Vector3(8.0f, 1.5f, 45.0f), new Vector3(0.0f, 1.5f, 45.0f) }
    };

    private void FixedUpdate()
    {
        if (gameManagerScript.IsGameOver() || gameManagerScript.IsGameClear()) return;

        if (gameManagerScript.IsGameStart()&&gameManagerScript.IsWave()<=2)
        {
            // WAVE�̒l�ɉ����ăX�|�[���N�[���^�C����ύX
            int currentWave = gameManagerScript.IsWave();
            if (currentWave >= 2)
            {
                spawnCooldown = 1.0f; // WAVE 2 �ȍ~�� 1�b
            }
            else if (currentWave >= 1)
            {
                spawnCooldown = 1.5f; // WAVE 1 �� 1.5�b
            }
            else
            {
                spawnCooldown = 2.0f; // ����ȊO�� 2�b
            }

            spawnTimer += Time.deltaTime;

            // 2�b���ƂɓG���o��
            if (spawnTimer >= spawnCooldown)
            {
                currentPatternIndex = Random.Range(0, enemyPatterns.Length);
                spawnTimer = 0.0f; // ���Z�b�g

                // �G�̎�ނ�����
                List<GameObject> enemyTypes = new List<GameObject> { attackEnemy, meteoriteEnemy, healPlaneEnemy };

                if (gameManagerScript.IsWave() >= 2) enemyTypes.Add(hoverCarEnemy);
                if (gameManagerScript.IsWave() >= 3) enemyTypes.Add(powerCarEnemy);

                // �����_���Ȏ�ނ̓G���o��
                foreach (var pos in enemyPatterns[currentPatternIndex])
                {
                    int randomEnemyIndex = Random.Range(0, enemyTypes.Count);
                    GameObject newEnemy = Instantiate(enemyTypes[randomEnemyIndex], pos, Quaternion.identity);

                    // `enemy` �̂� y ���W�� 8 �ɕύX
                    if (enemyTypes[randomEnemyIndex] == attackEnemy)
                    {
                        newEnemy.transform.position = new Vector3(pos.x, 8.0f, pos.z);
                    }
                    // `meteorite` �̂� y ���W�� 3 �ɕύX
                    if (enemyTypes[randomEnemyIndex] == meteoriteEnemy)
                    {
                        newEnemy.transform.position = new Vector3(pos.x, 3.0f, pos.z);
                    }
                }
            }
        }
    }
}
