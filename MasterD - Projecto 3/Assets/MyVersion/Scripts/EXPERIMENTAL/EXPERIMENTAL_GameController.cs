using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPERIMENTAL_GameController : MonoBehaviour
{
    //DataRepository Reference
    private EXPERIMENTAL_DataRepository repository;

    private EXPERIMENTAL_UIManager uiManager;

    //TESTS
    private float points;
    private float coins;
    private float enemies;
    private float missiles;


    private void Awake()
    {
        repository = new EXPERIMENTAL_DataRepository();
        uiManager = GameObject.Find("Canvas").GetComponent<EXPERIMENTAL_UIManager>();
        
    }

    private void Start()
    {
        uiManager.UpdateUI(repository.GetStats());
    }

    private void Update()
    {
        points += Time.deltaTime / 2f;
        coins += Time.deltaTime / 2.5f;
        enemies += Time.deltaTime / 1.5f;
        missiles += Time.deltaTime;
    }

    public void UpdateUI()
    {
        repository.UpdateStats(Mathf.FloorToInt(points), Mathf.FloorToInt(coins), Mathf.FloorToInt(enemies), Mathf.FloorToInt(missiles));
        uiManager.UpdateUI(repository.GetStats());
    }
}
