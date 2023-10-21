using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public WaveSo[] waves;
    private WaveSo currentWave;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject parentEnemyPrefab;
    public TMP_Text waveText;
    private float timeBetweenSpawns;
    private bool stopSpawning;
    private int i = 0;

    private void Awake()
    {
        currentWave = waves[i];
        timeBetweenSpawns = currentWave.TimeBeforeThisWave;
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = currentWave.waveName.ToString();
        if(stopSpawning)
        {
            return;
        }

        if(Time.time >= timeBetweenSpawns)
        {
            SpawnWave();
            IncWave();

            timeBetweenSpawns = Time.time + currentWave.TimeBeforeThisWave;
        }
    }

    private void SpawnWave()
    {
        for(int i = 0; i < currentWave.NumberToSpawn; i++) 
        {
            int randomEnemy = Random.Range(0, currentWave.EnemiesInWave.Length);
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

            parentEnemyPrefab = GameObject.Find("ParentEnemyHolder");
            GameObject newObject = Instantiate(currentWave.EnemiesInWave[randomEnemy], spawnPoints[randomSpawnPoint].position, transform.rotation);
            newObject.transform.parent = parentEnemyPrefab.transform;
        }
    }
    private void IncWave()
    {
        if(i + 1 < waves.Length)
        {
            i++;
            currentWave = waves[i];
        }
        else
        {
            stopSpawning = true;
        }
    }
}
