using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shooter determines which entity type is firing the bullet
// the ints assigned to the enum is based on the physics layer #
public enum Shooter
{
    player,
    enemy,
}

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public int damage = 1;

    private Renderer bulletRenderer;
    private Rigidbody2D rb2d;


    public Shooter shooter;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bulletRenderer = GetComponent<Renderer>();

    }

    // OnEnable is called when the object is enabled in play (i.e setActive(true))
    protected virtual void OnEnable()
    {
        Fire();
    }

    private void FixedUpdate()
    {
        FollowPath();
    }

    // Update is called once per frame
    void Update()
    {
        // De-activate bullet if off screen
        if (!bulletRenderer.isVisible) gameObject.SetActive(false);
    }

    // Fire()
    // --Initial velocity in direction of the front of bullet sprite
    void Fire()
    {
        rb2d.velocity = transform.up * bulletSpeed;
    }

    // OnTriggerEnter2D(collider2D)
    // -- Event triggered on collision with other collider object
    // -- If other collider is a character type, determine if damage is given
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Character entity = collision.gameObject.GetComponent<Character>();
        

        if (entity != null)
        {
            //Debug.Log("Hit -- " + shooter + " on " + entity.tag);

            // Damage is given if shooter is player-to-enemy or enemy-to-player
            switch (shooter)
            {
                case Shooter.player:
                    if (entity.CompareTag("Enemy"))
                    {
                        entity.TakeDamage(damage);
                        this.gameObject.SetActive(false);
                    }
                    break;

                case Shooter.enemy:
                    if (entity.CompareTag("Player")) {
                        entity.TakeDamage(damage);
                        this.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    public virtual void FollowPath(){}
}
public class CircleBullet : Bullet
{
    private Vector3 firePos;
    private int curve;

    public void SetBulletData(Vector3 pos, int curveDegree)
    {
        firePos = pos;
        curve = curveDegree;
    }

    // FollowPath()
    // --CircleBullet path is created by rotating around it's inital spawn object's position
    public override void FollowPath()
    {
        this.transform.RotateAround(firePos, Vector3.forward, curve * Time.deltaTime);
    }
}

public class ZigzagBullet : Bullet
{
    private float activeTime;
    private float spawnX;
    private float frequency;
    private float magnitude;

    protected override void OnEnable()
    {
        activeTime = Time.time;

        base.OnEnable();
    }
    public void SetBulletData(float spawn, float freq, float mag)
    {
        spawnX = spawn;
        frequency = freq;
        magnitude = mag;
    }

    // FollowPath() - Sinusoidal
    public override void FollowPath()
    {
        this.transform.position = new Vector3(spawnX + (Mathf.Sin((Time.time - activeTime)*frequency)*magnitude), this.transform.position.y, this.transform.position.z);
    }
}