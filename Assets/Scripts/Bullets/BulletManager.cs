using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // var - Spawner Information
    public enum Pattern
    {
        player,
        line,
        circle,
        zigzag,
        diamond,
    }


    // vars - Object Pool
    public static BulletManager SharedInstance;

    private List<GameObject> playerPool;
    public GameObject playerBulletObject;
    public int maxPlayerPool;

    private List<GameObject> linePool;
    public GameObject lineObject;
    public int maxLinePool;

    private List<GameObject> circlePool;
    public GameObject circleObject;
    public int maxCirclePool;

    private List<GameObject> zigzagPool;
    public GameObject zigzagObject;
    public int maxZigzagPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Instantiate Bullet Instances into Pool and add bullet component to object

        // -Personal Player Pool
        playerPool = new List<GameObject>();

        GameObject tmp;
        for (int i = 0; i < maxPlayerPool; i++)
        {
            tmp = Instantiate(playerBulletObject);

            tmp.AddComponent<Bullet>();

            tmp.SetActive(false);
            playerPool.Add(tmp);
        }

        // -Line Pool
        linePool = new List<GameObject>();

        for (int i = 0; i < maxLinePool; i++)
        {
            tmp = Instantiate(lineObject);
      
            tmp.AddComponent<Bullet>();

            tmp.SetActive(false);
            linePool.Add(tmp);
        }

        // -Do the same for the circle bullet pool
        circlePool = new List<GameObject>();
        for (int i = 0; i < maxCirclePool; i++)
        {
            tmp = Instantiate(circleObject);

            tmp.AddComponent<CircleBullet>();

            tmp.SetActive(false);
            circlePool.Add(tmp);
        }

        // -and for zigzag bullet pool
        zigzagPool = new List<GameObject>();
        for (int i = 0; i < maxZigzagPool; i++)
        {
            tmp = Instantiate(zigzagObject);

            tmp.AddComponent<ZigzagBullet>();

            tmp.SetActive(false);
            zigzagPool.Add(tmp);
        }
    }

    // GrabBullet(Pattern, Shooter, float, Vector3, float)
    // --Grabs bullet of certain pattern type, and adds information to the bullet (speed, pos, angle, shooter)
    public GameObject GrabBullet(Pattern p, Shooter s, float speed, Vector3 pos, float bulletDegrees)
    {
        GameObject bullet = BulletManager.SharedInstance.GetInactiveBullet(p);
        if (bullet != null)
        {
            bullet.transform.position = pos;
            bullet.transform.rotation = Quaternion.Euler(Vector3.forward * (bulletDegrees));

            // Change public variables to reflect shooters bullet information (TODO PUT SOMEWHERE ELSE)
            bullet.GetComponent<Bullet>().bulletSpeed = speed;
            bullet.GetComponent<Bullet>().shooter = s;

            // Change Physics layer to ignore certain collisions
            switch (s)
            {
                case Shooter.player:
                    bullet.layer = 10;
                    break;
                case Shooter.enemy:
                    bullet.layer = 11;
                    break;
            }

            return bullet;
        }
        return null;
    }

    // GetInactiveBullet()
    // --Gets unused bullet of certain type from its' object pool
    private GameObject GetInactiveBullet(Pattern type)
    {
        List<GameObject> list = null;
        int maxPool = 0;
        switch (type)
        {
            case Pattern.player:
                list = playerPool;
                maxPool = maxPlayerPool;
                break;

            case Pattern.line:
                list = linePool;
                maxPool = maxLinePool;
                break;

            case Pattern.circle:
                list = circlePool;
                maxPool = maxCirclePool;
                break;

            case Pattern.zigzag:
                list = zigzagPool;
                maxPool = maxZigzagPool;
                break;
        }

        for (int i = 0; i < maxPool; i++)
        {
            if (!list[i].activeInHierarchy)
            {
                return list[i];
            }
        }

        return null;
    }
}