using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class WaveConfiguration : ScriptableObject
{
    // wave configuration
    [SerializeField] GameObject enemyPrefab; // enemies
    [SerializeField] GameObject pathPrefab; // enemy routes
    [SerializeField] float spawnTime = 0.3f; // base time between spawning each enemy
    [SerializeField] float randomSpawnFactor = 0.3f; // randomizes time frame between spawns
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float enemyMoveSpeed = 7f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform pathChild in pathPrefab.transform)
        {
            waveWaypoints.Add(pathChild);
        }
        return waveWaypoints;
    }

    public float GetSpawnTime()
    {
        return spawnTime;
    }

    public float GetRandomSpawnFactor()
    {
        return randomSpawnFactor;
    }

    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }

    public float GetEnemyMoveSpeed()
    {
        return enemyMoveSpeed;
    }

}
