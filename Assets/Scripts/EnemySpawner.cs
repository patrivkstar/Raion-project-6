using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public int amount = 1;          
    public float spawnDelay = 1.0f; 
    private int deadCount = 0;

    public void StartSpawning()
    {
        deadCount = 0;
        if (enemyPrefab != null) StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject e = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            EnemyHealth health = e.GetComponent<EnemyHealth>();
            if (health != null) health.onDeathCallback = ReportDeath;
            if (i < amount - 1) yield return new WaitForSeconds(spawnDelay);
        }
    }

    void ReportDeath()
    {
        deadCount++;
        if (deadCount >= amount)
        {
            gameObject.SetActive(false); 
        }
    }
}