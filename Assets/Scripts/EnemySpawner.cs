using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemies;

    [Header("Spawn Rate")]
    public float maxSpawnRateInSeconds = 5f;


    [Header("Limites")]
    public float limiteX = 4f;

    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Player") != null)
        {
            Invoke("SpawnEnemies", maxSpawnRateInSeconds);
            InvokeRepeating("IncreaseSpawnRate", 0f, 15f);
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void SpawnEnemies()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anEnemy = (GameObject)Instantiate(Enemies);
        anEnemy.transform.position = new Vector2(UnityEngine.Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn();

    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;

        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = UnityEngine.Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawnInNSeconds = 1f;
        Invoke("SpawnEnemies", spawnInNSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }
}
