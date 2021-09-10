using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEditor : MonoBehaviour
{

    public static int waveNumber;

    public List<GameObject> LevelEnemies;

    public Queue<EnemyWave> enemyWaves;

    private float waveTime = 0.0f;
    protected WaveType currWaveTrigger;
    protected EnemyWave currentWave;
    private bool wavesComplete;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        wavesComplete = false;
        waveNumber = 0;

        enemyWaves = new Queue<EnemyWave>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        waveTime += Time.deltaTime;

        // Check Enemy Spawning Stack
        if (!wavesComplete)
        {
            // Handle trigger for when the next wave should spawn
            switch (currentWave.waveType)
            {
                case WaveType.Timed:
                    CheckWaveTime();
                    break;

                case WaveType.EnemiesGone:
                    if (!CheckActiveEnemies())
                    {
                        Debug.Log("LevelEditor: Enemies Inactive - Starting Next Wave");
                        SpawnWave();
                    }
                    break;

                case WaveType.Forced:
                    break;

                default:
                    Debug.LogError("LevelEditor: Error: WaveType not Selected for wave " + waveNumber);
                    break;
            }

        }
        else
        {
            // If no other enemy waves are in the enemyWave queue, Finish the level
            FinishLevel();
        }
  
    }

    // SpawnWave - dequeues next wave in Queue and begins instantiation of that enemy wave
    protected void SpawnWave() 
    {
        if (enemyWaves.Count > 0)
        {
            currentWave = enemyWaves.Dequeue();
            Debug.Log(currentWave.waveType);
            // Reset WaveTimer and begin spawning coroutine
            waveTime = 0f;
            StartCoroutine(InvokeDelay(currentWave));

            waveNumber++;
            Debug.Log("LevelEditor: Wave: " + waveNumber);
        }
        else
        {
            wavesComplete = true;
        }
    }

    // CheckWaveTime - Timed based wave trigger. Determines if next wave should be spawned based
    //                 on time since last wave spawned and predetermined time that next wave should spawn.
    private void CheckWaveTime()
    {
        if (waveTime > currentWave.nextWaveTimer)
        {
            SpawnWave();
        }
    }

    // ForceNextWave - Directly accessible wave trigger, typically used with the 'Forced' wave type.
    //                 Used for when the player must complete certain tasks before next wave spawns.
    public void ForceNextWave()
    {
        SpawnWave();
    }

    //InvokeDelay()
    // --Invokes enemy instantiation based on information given from the EnemyWave
    private IEnumerator InvokeDelay(EnemyWave enemyData)
    {
        for (int i = 0; i < enemyData.quantity; i++)
        {
            Instantiate(enemyData.spawnee, enemyData.spawnPos, transform.rotation);
            
            yield return new WaitForSeconds(enemyData.spawnDelay);
        }
    }
    // CheckActiveEnemies - Object based wave trigger. Determines if next wave should be spawned based
    //                      on whether any other enemies are present.
    private bool CheckActiveEnemies()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Debug.Log("LevelEditor: Enemies in play: " + enemies.Length);
        if (enemies.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // FinishLevel()
    // -- Loads and returns user to the Main Menu Scene.
    private void FinishLevel()
    {
        Debug.Log("Loading Main Menu...");

        SceneManager.LoadScene("MenuScene");
    }

    // WaveType - Wave Triggers
    public enum WaveType
    {
        Timed,
        EnemiesGone,
        Forced,
    }
    public class EnemyWave
    {
        public GameObject spawnee;  // Prefab of enemy Gameobject
        public Vector2 spawnPos;    // Position that enemies will spawn
        public float nextWaveTimer; // Timed (WaveType) - Time that next wave will spawn

        public int quantity;        // amount of spawnee that will be created
        public float spawnDelay;    // Delay between each instance of spawnee that is created
        public WaveType waveType;   // Trigger type for next wave spawn
    }
}
