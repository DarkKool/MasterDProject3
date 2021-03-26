using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EXPERIMENTAL_EnemySpawner : MonoBehaviour
{
    //Enemy Prefab
    private GameObject enemy;

    //Spawn Timer
    [SerializeField] private float spawnTime;

    //Spawn Time Tracker
    private float spawnTimeTrack;

    //Enemy Spawn Position Boundaries
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float zAxisPos;

    private void Start()
    {
        enemy = (GameObject) Resources.Load("EnemyConceptModel");
    }

    private void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        if(spawnTimeTrack >= spawnTime)
        {
            Vector3 enemyPosition = new Vector3(Random.Range(leftLimit, rightLimit), 0, zAxisPos);

            GameObject enemyInstance = Instantiate(enemy);
            enemyInstance.GetComponent<EXPERIMENTAL_Enemy>().SetPosition(enemyPosition);
            
            spawnTimeTrack = 0;
        }
        else
        {
            spawnTimeTrack += Time.deltaTime;
        }
    }
}
