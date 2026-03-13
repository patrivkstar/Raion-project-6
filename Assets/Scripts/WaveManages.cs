using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public List<EnemySpawner> waves; 
    private int currentWaveIndex = 0;

    void Start()
    {
        
        foreach (var spawner in waves)
        {
            spawner.gameObject.SetActive(false);
        }

      
        StartWave(0);
    }

    void StartWave(int index)
    {
        if (index < waves.Count)
        {
            Debug.Log("Memulai Wave " + (index + 1));
            waves[index].gameObject.SetActive(true);
            
            
            waves[index].OnSpawnerClear += NextWave;
        }
        else
        {
            Debug.Log("Semua Wave Selesai! Kamu Menang!");
        }
    }

    void NextWave()
    {
        currentWaveIndex++;
        StartWave(currentWaveIndex);
    }
}