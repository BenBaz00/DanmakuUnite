using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBasic : MonoBehaviour
{
    public float fireRate;
    private float nextFire;

    // Object Pooling player bullets to lessen CPU burden
    // REFACTOR -- THE SHARED STATIC INSTANCE MAY CONFLICT IF OTHER ENTITIES BESIDE PLAY USE THIS SCRIPT
    public static WeaponBasic SharedInstance;
    private List<GameObject> bulletPool;
    public GameObject playerBullet;
    public int maxBulletPool;

    // Awake is called once the entire script lifetime
    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        nextFire = 0.0f;

        // Instantiate Bullet Instances into Pool
        bulletPool = new List<GameObject>();
        GameObject tmp;
        for(int i=0; i<maxBulletPool; i++)
        {
            tmp = Instantiate(playerBullet);
            tmp.SetActive(false);
            bulletPool.Add(tmp);
        }
    }

    public void Shoot()
    {
        // Check firerate.
        if (Time.time <= nextFire) return;

        nextFire = Time.time + fireRate;

        // Activate Bullet in bulletPool
        GameObject bullet = WeaponBasic.SharedInstance.GetInactiveBullet();
        if (bullet != null)
        {
            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = this.transform.rotation;
            
            bullet.SetActive(true);

        }
    }

    public GameObject GetInactiveBullet()
    {
        for (int i=0; i<maxBulletPool; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }

        return null;
    }
}
