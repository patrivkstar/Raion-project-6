using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemySpawner> spawnersInThisWave; 
        public float intervalBetweenSpawners = 2.0f;
    }

    public List<Wave> waves;
    private int currentWaveIndex = 0;

    void Start()
    {
        foreach (var wave in waves)
        {
            foreach (var s in wave.spawnersInThisWave) 
            {
                if(s != null) s.gameObject.SetActive(false);
            }
        }
        StartCoroutine(WaveSequence());
    }

    IEnumerator WaveSequence()
    {
        while (currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];

            foreach (var spawner in currentWave.spawnersInThisWave)
            {
                if (spawner != null)
                {
                    spawner.gameObject.SetActive(true);
                    spawner.StartSpawning();
                    yield return new WaitForSeconds(currentWave.intervalBetweenSpawners);
                }
            }

            bool waveFinished = false;
            while (!waveFinished)
            {
                waveFinished = true;
                foreach (var spawner in currentWave.spawnersInThisWave)
                {
                    if (spawner != null && spawner.gameObject.activeInHierarchy)
                    {
                        waveFinished = false;
                        break;
                    }
                }
                yield return new WaitForSeconds(0.5f);
            }

            currentWaveIndex++;
            yield return new WaitForSeconds(2.0f);
        }
    }
}