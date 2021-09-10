using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletManager;

public class ZigzagSpawn : MonoBehaviour
{
    public float bulletSpeed;
    public float fireRate;
    public float degreeOffset;

    // var - Zigzag Information
    public float frequency;
    public float magnitude;


    // Start is called before the first frame update
    protected void Start()
    {
        InvokeRepeating("FireZigzag", 1f, fireRate);
    }

    void FireZigzag()
    {
        var newBullet = BulletManager.SharedInstance.GrabBullet(Pattern.zigzag, Shooter.enemy, bulletSpeed, this.transform.position, 180);
        newBullet.GetComponent<ZigzagBullet>().SetBulletData(this.transform.position.x, frequency, magnitude);

        newBullet.SetActive(true);
    }

}
