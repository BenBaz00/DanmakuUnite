using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletManager;

public class CircleSpawn : MonoBehaviour
{
    public bool semiMode;
    public float bulletSpeed;
    public float fireRate;
    public float degreeOffset;

    // vars - Circle Information
    public int sprockets;   // # of points that bullets will spawns
    public int curve;       // curve of bullet over time
    public float spinSpeed; // spin of sprocket spawn positions over time

    private float nextSpin = 0;
    // Start is called before the first frame update
    void Start()
    {
        BeginFiring();
    }

    // BeginFiring()
    // --Begins a repeating method depending on if it's full-circle or semi-circle mode
    public void BeginFiring()
    {
        if (semiMode)
        {
            InvokeRepeating("FireSemiCircle", 1f, fireRate);
        }
        else
        {
            InvokeRepeating("FireCircle", 1f, fireRate);
        }
    }

    // FireCircle()
    // --Fires a circle of bullets around the gameobject
    void FireCircle()
    {
        // Spawn bullets at each sprocket point
        float bulletAngle = 0 + nextSpin;
        for (int i = 0; i < sprockets; i++)
        {
            bulletAngle += (float)(2 * Mathf.PI / sprockets);
            
            // Grab and Activate Bullet with necessary data
            var newBullet = BulletManager.SharedInstance.GrabBullet(Pattern.circle, Shooter.enemy, bulletSpeed, this.transform.position, bulletAngle * Mathf.Rad2Deg + degreeOffset);
            newBullet.GetComponent<CircleBullet>().SetBulletData(this.transform.position, curve);
            newBullet.SetActive(true);
        }
        nextSpin += spinSpeed;

        if (nextSpin >= 360)
        {
            nextSpin %= 360;
        }
    }

    // FireSemiCircle()
    // --Fires a semi-circle of bullets around the gameobject
    void FireSemiCircle()
    {
        // Spawn bullets at each sprocket point
        float bulletAngle = 0 + nextSpin;
        for (int i = 0; i < sprockets; i++)
        {
            bulletAngle += (float)(Mathf.PI / sprockets);

            // Grab and Activate Bullet with necessary data
            var newBullet = BulletManager.SharedInstance.GrabBullet(Pattern.circle, Shooter.enemy, bulletSpeed, this.transform.position, bulletAngle * Mathf.Rad2Deg + degreeOffset);
            newBullet.GetComponent<CircleBullet>().SetBulletData(this.transform.position, curve);
            newBullet.SetActive(true);
        }
        nextSpin += spinSpeed;

        if (nextSpin >= 360)
        {
            nextSpin %= 360;
        }
    }

    public void StopFiring()
    {
        CancelInvoke();
    }
}
