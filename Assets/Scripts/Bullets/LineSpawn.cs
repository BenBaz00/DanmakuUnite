using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletManager;

public class LineSpawn : MonoBehaviour
{
    public Shooter shooter;

    public float bulletSpeed;
    public float fireRate;
    public float degreeOffset;

    private float direction;

    // Player Controlled Fire Vars
    private float nextFire;

    // Start is called before the first frame update
    protected void Start()
    {
        nextFire = 0.0f;
        
        if (shooter == Shooter.enemy)
        {
            direction = 180f;
            StartCoroutine(InvokeLine(.5f, 1f));
        }
        else
        {
            direction = 0f;
        }
        
    }

    private IEnumerator InvokeLine(float firstDelay, float secondDelay)
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            FireLine(direction);

            if (i < 1)
            {
                yield return new WaitForSeconds(firstDelay);
            }
            else
            {
                yield return new WaitForSeconds(secondDelay);
            }

        }
    }

    public void FireLine(float bulletDegrees)
    {
        // Player controlled firerate
        if (shooter == Shooter.player) 
        {
            if (Time.time <= nextFire)
            {
                return;
            }
            else
            {
                var newBullet = BulletManager.SharedInstance.GrabBullet(Pattern.player, shooter, bulletSpeed, this.transform.position, bulletDegrees);
                newBullet.SetActive(true);
            }
        }
        else 
        {
            var newBullet = BulletManager.SharedInstance.GrabBullet(Pattern.line, shooter, bulletSpeed, this.transform.position, bulletDegrees);
            newBullet.SetActive(true);
        }

        nextFire = Time.time + fireRate;
    }
}
