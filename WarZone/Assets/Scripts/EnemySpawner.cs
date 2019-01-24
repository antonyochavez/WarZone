using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfiguration> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = true;

    // Start is called before the first frame update
    IEnumerator Start() // puts coroutine for spawning waves on loop
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves() // coroutine that spawns waves in order 
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemies(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemies(WaveConfiguration waveConfig) /* coroutine
        that spawns enemies in a wave configuration */       
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfiguration(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetSpawnTime());
        }

    }

}
