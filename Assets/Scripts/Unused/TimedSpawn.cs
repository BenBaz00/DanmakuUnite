using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawn : MonoBehaviour
{
    public GameObject spawnee;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    public List<GameObject> list;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject()
    {
        //Get Cam width and spawn entity within 0-max
        Camera cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        Vector3 spawnPos = new Vector3(Random.Range(-width/2, width/2), transform.position.y, 0);
        
        // Create spawnee GameObject
        Instantiate(spawnee, spawnPos, transform.rotation);

        // REMOVE LATER(?) Tells me if spawner is acting fucky
        // Sometimes boys spawn OoB
        if (spawnPos.x > 18f || spawnPos.x < -18f)
        {
            print("Error: TimedSpawn: Entity spawned OoB");
        }

        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
