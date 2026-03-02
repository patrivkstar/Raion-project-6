using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;

    public float spawnRadius = 12f;
    public float delay = 2f;

    public int maxEnemy = 30;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= delay)
        {
            timer = 0;
            SpawnEnemy();
        }
        if (player == null) 
        return;
    }

    void SpawnEnemy()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemy)
            return;

        Vector2 pos =
            (Vector2)player.position +
            Random.insideUnitCircle * spawnRadius;

        Instantiate(enemyPrefab,pos,Quaternion.identity);
    }
}