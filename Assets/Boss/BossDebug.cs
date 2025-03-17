using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDebug : MonoBehaviour
{
    private GameObject bossScript;
    public BossScript bossScriptScript;
    // Start is called before the first frame update
    void Start()
    {
        bossScriptScript = bossScript.GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
