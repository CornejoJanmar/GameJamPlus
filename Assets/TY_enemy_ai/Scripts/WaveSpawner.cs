using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public WaveSo[] waves;
    public WaveSo currentWave;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject parentEnemyPrefab;
    public TMP_Text waveText;
    private float timeBetweenSpawns;
    private bool stopSpawning;
    private int i = 0;
    public float cooldownTimer;
    private void Awake()
    {
        currentWave = waves[i];
        timeBetweenSpawns = currentWave.TimeBeforeThisWave;
        cooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = currentWave.waveName.ToString();
        if (stopSpawning)
        {
            return;
        }


        if (parentEnemyPrefab.transform.childCount <= 0)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= currentWave.TimeBeforeThisWave)
            {
                cooldownTimer = 0;
                Debug.Log("Spawn!!!");
                SpawnWave();
                IncWave();
            }
        }
    }

    private void SpawnWave()
    {

        for (int i = 0; i < currentWave.NumberToSpawn; i++)
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
        if (i + 1 < waves.Length)
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
