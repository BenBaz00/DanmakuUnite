using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : LevelEditor
{
    private enum DemoSections
    {
        Miniboss,
        Boss,

    }

    private Queue<DemoSections> demo;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // 3x Droids - Right
        enemyWaves.Enqueue(new EnemyWave 
        {
            spawnee = LevelEnemies[1],
            spawnPos = new Vector2(-8, 12),
            quantity = 3,
            spawnDelay = 1f,
            waveType = WaveType.EnemiesGone
        });

        // 3x Droids - Left
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[1],
            spawnPos = new Vector2(8, 12),
            quantity = 3,
            spawnDelay = 1f,
            waveType = WaveType.EnemiesGone
        });

        // 1x Bat - Mid
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[0],
            spawnPos = new Vector2(0, 12),
            quantity = 1,
            spawnDelay = 0f,
            waveType = WaveType.EnemiesGone
        });

        // 1x Observer - Mid
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[2],
            spawnPos = new Vector2(0, 12),
            quantity = 1,
            spawnDelay = 0f,
            waveType = WaveType.EnemiesGone
        });

        // 1+1x Observer - Left & Right
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[3],
            spawnPos = new Vector2(-9, 12),
            quantity = 1,
            spawnDelay = 0f,
            nextWaveTimer = 0f,
            waveType = WaveType.Timed
        });
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[4],
            spawnPos = new Vector2(9, 12),
            quantity = 1,
            spawnDelay = 0f,
            waveType = WaveType.EnemiesGone
        });

        // Droid Rush
        //  -Left
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[5],
            spawnPos = new Vector2(-10, 12),
            quantity = 3,
            spawnDelay = 1f,
            nextWaveTimer = 0f,
            waveType = WaveType.Timed
        });
        
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[1],
            spawnPos = new Vector2(-7, 12),
            quantity = 3,
            spawnDelay = 1f,
            nextWaveTimer = 5f,
            waveType = WaveType.Timed
        });

        //  -Right 
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[5],
            spawnPos = new Vector2(10, 12),
            quantity = 3,
            spawnDelay = 1f,
            nextWaveTimer = 0f,
            waveType = WaveType.Timed
        });

        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[1],
            spawnPos = new Vector2(7, 12),
            quantity = 3,
            spawnDelay = 1f,
            waveType = WaveType.EnemiesGone
        });

        // Miniboss (w/ droid adds)
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[6],
            spawnPos = new Vector2(0, 12),
            quantity = 1,
            spawnDelay = 0f,
            nextWaveTimer = 2f,
            waveType = WaveType.Timed
        });
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[1],
            spawnPos = new Vector2(-9, 12),
            quantity = 3,
            spawnDelay = 5f,
            nextWaveTimer = 2f,
            waveType = WaveType.Timed
        });
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[1],
            spawnPos = new Vector2(9, 12),
            quantity = 3,
            spawnDelay = 5f,
            waveType = WaveType.EnemiesGone
        });
        enemyWaves.Enqueue(new EnemyWave
        {
            nextWaveTimer = 3f,
            waveType = WaveType.Timed
        });

        // Boss
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[7],
            spawnPos = new Vector2(0, 16),
            quantity = 1,
            spawnDelay = 5f,
            waveType = WaveType.EnemiesGone
        });

        // 3 sec buffer
        enemyWaves.Enqueue(new EnemyWave
        {
            nextWaveTimer = 3f,
            waveType = WaveType.Timed
        });

        SpawnWave();
        Debug.Log("Demo: Start");
    }

}
