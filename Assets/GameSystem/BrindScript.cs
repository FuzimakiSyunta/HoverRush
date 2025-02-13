using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrindScript : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossBullet;
    public GameObject EnergyImage;
    public GameObject StartSelectImage;
    public GameObject LuleSelectImage;
    public GameObject SelectorImage;
    //public GameObject LuleImage;
    private GameManager gameManagerScript;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
        boss.SetActive(false);
        bossBullet.SetActive(false);
        EnergyImage.SetActive(false);
        //LuleImage.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.IsGameStart()==true)
        {
            boss.SetActive(true);
            bossBullet.SetActive(true);
            EnergyImage.SetActive(true);
            StartSelectImage.SetActive(false);
            LuleSelectImage.SetActive(false);
            SelectorImage.SetActive(false);

        }
    }
}
