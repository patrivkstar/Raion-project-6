using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float spawnInterval = 2f;
    public int totalStokMusuh = 5;
    public float spawnRange = 3f;

    private int sisaStok;
    private int musuhAktif = 0;
    private float nextSpawnTime;
    private bool isFinished = false;

    public Action OnSpawnerClear; 
    void Start()
    {
        sisaStok = totalStokMusuh;
    }

    void Update()
    {
        if (sisaStok > 0 && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

        
        if (sisaStok <= 0 && musuhAktif <= 0 && !isFinished)
        {
            isFinished = true;
            OnSpawnerClear?.Invoke();
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomPos = (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * spawnRange;
        GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        
        sisaStok--;
        musuhAktif++;

      
        enemy.GetComponent<EnemyHealth>().OnDeath += () => {
            musuhAktif--;
        };
    }
}