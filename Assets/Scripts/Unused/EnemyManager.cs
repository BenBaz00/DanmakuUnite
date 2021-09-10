using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager SharedInstance;

    private List<GameObject> enemyPool;
    public int amtEachEnemy;

    // Start is called before the first frame update
    void Start()
    {
        // Find enemies for level and instantiate each in pool
        List<GameObject> enemies = Component.FindObjectOfType<LevelEditor>().LevelEnemies;
        
        enemyPool = new List<GameObject>();

        GameObject tmp;
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int e = 0; e < amtEachEnemy; e++)
            {
                tmp = Instantiate(enemies[i]);

                tmp.SetActive(false);
                enemyPool.Add(tmp);
            }
        }
    }

    public GameObject GrabEnemy()
    {
        //GameObject enemy = EnemyManager.SharedInstance.GetInactiveEnemy();
        return null;
    }


}
