using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject basicEnemyPrefab;
    public GameObject missileReloadPowerup3;

    [Header("Spawning Data")]
    public float xSpawnRange;
    public float zSpawnRange;
    public float safetyRadius;
    public GameObject player;
    [Header("Enemy Numbers")]
    public int basicEnemyCount;

    [Header("Wave Number")]
    public int waveNumber;
    public int basicEnemiesToSpawn;
    public bool spawnManagerisActive;


    // Start is called before the first frame update
    public void startGameSpawnManager()
    {
        spawnManagerisActive = true;
        player = GameObject.Find("Player");
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        basicEnemyCount = FindObjectsOfType<BasicEnemyControl>().Length;
        //Start new wave
        if (basicEnemyCount == 0 && spawnManagerisActive == true)
        {
            SpawnEnemyWave(basicEnemiesToSpawn);
        }
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        //Spawn Basic Enemy
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(basicEnemyPrefab, GenerateSpawnPosition(), basicEnemyPrefab.transform.rotation);
        }
        //Spawn missile reload
        int randomthing = Random.Range(1, 3);
        if (randomthing == 1)
        {
            Instantiate(missileReloadPowerup3, GenerateSpawnPosition(), missileReloadPowerup3.transform.rotation);
        }

    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-xSpawnRange, xSpawnRange);
        float spawnPosZ = Random.Range(-zSpawnRange, zSpawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        while((randomPos - player.transform.position).magnitude < safetyRadius)
        {
            spawnPosX = Random.Range(-xSpawnRange, xSpawnRange);
            spawnPosZ = Random.Range(-zSpawnRange, zSpawnRange);
            randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        }
        return randomPos;
    }
}
