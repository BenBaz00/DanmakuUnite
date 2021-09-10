using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Enemy
 * -- Enemy extends Character, as it is an entity that will take damage from player projectiles
 * -- Enemy is a standard to extend from for enemy behavior, as all enemies will flash when
 * they take damage (as an indicator to the player)
 * -- Enemy destruction is handled when one of two conditions is met; when health is depleted or if the enemy exits the camera view.
 */
public abstract class Enemy : Character
{
    protected float activeTime;

    private Renderer enemyRenderer;
    private const float startupTime = 3f;

    protected SpriteRenderer spriteRender;
    protected Color normalColor;
    private const float flashTime = .05f;

    public float speed;
    public float maxSpeed;
    protected Rigidbody2D rb2d;
    protected Vector2 moveDirection;

    protected virtual void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        spriteRender = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();

        normalColor = spriteRender.color;
    }
    private void FixedUpdate()
    {
        // Allow Enemy's velocity to only go up to it's max Speed
        if (rb2d.velocity.magnitude > maxSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }

        rb2d.AddRelativeForce(moveDirection * speed);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        activeTime += Time.deltaTime;
        moveDirection = Vector2.zero;

        Movement();

        // Checks if enemy is visible after startup time is reached (to avoid pre-mature deletion of enemy)
        if (activeTime > startupTime)
        {
            CheckVisible();
        }
    }

    protected abstract void Movement();

    // HandleDamage()
    // --For enemies, when damaged the sprite will flash black momentarily
    protected override void HandleDamage()
    {
        spriteRender.color = Color.black;
        Invoke("StopFlash", flashTime);
    }

    // HandleDestruction()
    // --GameObject will be removed from scene
    protected override void HandleDestruction()
    {
        Destroy(this.gameObject);
    }
    
    // CheckVisible() - Determines if the Enemy is within screen view, if not, remove it from play
    private void CheckVisible()
    {
        if (!enemyRenderer.isVisible) HandleDestruction();
    }

    private void StopFlash()
    {
        spriteRender.color = normalColor;
    }

    protected Vector2 RandomXDirection()
    {
        if (Random.value < 0.5f)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.left;
        }
    }
}
