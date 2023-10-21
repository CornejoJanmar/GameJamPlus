using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public GameObject parentEnemyPrefab;
    public bool waveIsDone;
    public float timeBetweenWaves = 5.0f;
    int waveCount = 1;
    public float spawnRate = 3.0f;
    public int enemyCount;
    public TMP_Text waveText;
    private void Start()
    {
        // Starts the wave
        waveIsDone = true;
    }
    private void Update() 
    {
        waveText.text = "Wave: " + waveCount.ToString();
        if(waveIsDone == true)
        {
            StartCoroutine(waveSpawner());
        }
    }

    IEnumerator waveSpawner()
    {
        if (parentEnemyPrefab != null)
        {
            Destroy(parentEnemyPrefab);
            parentEnemyPrefab = new GameObject("ParentEnemyHolder");
        }

        waveIsDone = false;
        for (int i = 0; i < enemyCount; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

            GameObject newObject = Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawnPoint].position, transform.rotation);
            newObject.transform.parent = parentEnemyPrefab.transform;
            yield return new WaitForSeconds(spawnRate);
        }

        spawnRate -= 1f;
        enemyCount +=1;
        waveCount += 1;

        yield return new WaitForSeconds(timeBetweenWaves);
        waveIsDone = true;
    }
}
