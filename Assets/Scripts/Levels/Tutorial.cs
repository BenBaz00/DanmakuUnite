using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tutorial : LevelEditor
{
    private enum TutorialParts
    {
        Movement,
        Focus,

    }

    private Queue<TutorialParts> tutorial;

    private bool[] movementCheck;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Movement Test Wave (see MovementTest())
        enemyWaves.Enqueue(new EnemyWave
        {
            waveType = WaveType.Forced
        });

        // Focus Test Wave (see FocusTest())
        enemyWaves.Enqueue(new EnemyWave
        {
            waveType = WaveType.Forced
        });

        // Firing at enemy test
        enemyWaves.Enqueue(new EnemyWave
        {
            spawnee = LevelEnemies[0],
            spawnPos = new Vector2(0,12),
            quantity = 1,
            spawnDelay = 0f,
            waveType = WaveType.EnemiesGone
        });

        // 3 sec buffer
        enemyWaves.Enqueue(new EnemyWave
        {
            waveType = WaveType.Timed,
            nextWaveTimer = 3f
        });

        // 3 sec buffer
        enemyWaves.Enqueue(new EnemyWave
        {
            waveType = WaveType.Timed,
            nextWaveTimer = 3f
        });

        // Setup Tutorial Segments
        tutorial = new Queue<TutorialParts>();

        tutorial.Enqueue(TutorialParts.Movement);
        movementCheck = new bool[4]; //0123 = WASD

        tutorial.Enqueue(TutorialParts.Focus);

        SpawnWave();
        Debug.Log("Tutorial: Movement Tutorial Begin");

        // Give Player Invulnerability
        GameObject.Find("Player").GetComponent<Player>().SetInvulnerability(true);
    }

    protected override void Update()
    {
        
        // Determines which tutorial segment the user should be checked for
        if (tutorial.Count > 0)
        {
            switch (tutorial.Peek())
            {
                case TutorialParts.Movement:
                    MovementTest();
                    break;

                case TutorialParts.Focus:
                    FocusTest();
                    break;
            }
        }
        base.Update();
    }

    // MovementTest()
    // --Waits until user has used ALL movement keys until the wave is considered complete
    void MovementTest()
    {
        if (!PauseMenu.GamePaused)
        {
            // Check Horizontal Input
            if (Input.GetAxis("Horizontal") == -1)
            {
                movementCheck[1] = true;
            }else if (Input.GetAxis("Horizontal") == 1)
            {
                movementCheck[3] = true;
            }

            // Check Vertical Input
            if (Input.GetAxis("Vertical") == 1)
            {
                movementCheck[0] = true;
            }else if (Input.GetAxis("Vertical") == -1)
            {
                movementCheck[2] = true;
            }
        }
        // Check Completion of Movement Test
        if (movementCheck.All(x => x.Equals(true)))
        {
            Debug.Log("Tutorial: Movement tutorial Complete");
            ForceNextWave();
            tutorial.Dequeue();
        }
    }

    // FocusTest()
    // -- Waits until user has used the focus key ("fire3") until the wave is considered complete
    void FocusTest()
    {
        if (!PauseMenu.GamePaused)
        {
            if (Input.GetButtonUp("Fire3"))
            {
                ForceNextWave();
                tutorial.Dequeue();
            }
        }
    }
}
