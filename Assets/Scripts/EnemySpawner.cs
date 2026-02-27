using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRange = 10f;
    public float spawnTime = 3f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnTime);
    }

    void SpawnEnemy()
    {
        Vector2 randomPos =
            (Vector2)transform.position +
            Random.insideUnitCircle * spawnRange;

        Instantiate(enemyPrefab, randomPos, Quaternion.identity);
    }
}