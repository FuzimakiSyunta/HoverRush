using UnityEngine;

public class ObjectDropper : MonoBehaviour
{
    public GameObject objectPrefab;
    private BossScript bossScript;
    public GameObject boss;
    //private bool isDropping = false;

    void Start()
    {
        bossScript = boss.GetComponent<BossScript>();
    }

    private Vector3[] dropPositions = new Vector3[]
    {
        new Vector3(0f, 20f, 6f),
        new Vector3(9.3f, 20f, 5.2f),
        new Vector3(6.26f, 20f, -0.5f),
        new Vector3(-11.1f, 20f, -0.8f),
        new Vector3(-9f, 20f, 10.8f)
    };

    void Update()
    {
        //if (bossScript.IsDrop() && !isDropping)
        //{
        //    isDropping = true;
        //    InvokeRepeating("DropRandomObject", 0f, 0.5f);
        //}
    }

    void DropRandomObject()
    {
        int index = Random.Range(0, dropPositions.Length);
        Vector3 dropPosition = dropPositions[index];
        Instantiate(objectPrefab, dropPosition, Quaternion.identity);
    }
}