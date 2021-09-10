using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    public float targetYPos;
    private Transform target;

    public float leaveTime;
    private Vector2 leaveDirection;

    private float leftBound;
    private float rightBound;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Determine Screen Boundaries (including sprite width)
        Vector3 left = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 spriteExtend = spriteRender.bounds.extents;

        leftBound = left.x + spriteExtend.x;
        rightBound = right.x - spriteExtend.x;

        // Choose a random direction to leave to
        leaveDirection = RandomXDirection();
    }

    protected override void Movement()
    {
        // Entering: Force applied to allow bat to enter game view
        // Maintaining y-position: after entering, force will be applied to have bat hover around set y-position
        if (rb2d.position.y > targetYPos)
        {
            rb2d.AddForce(Vector2.down/3, ForceMode2D.Impulse);
        }
        else
        {
            rb2d.AddForce(Vector2.up/3, ForceMode2D.Impulse);
        }
        
        if (activeTime > leaveTime)
        {
            moveDirection = leaveDirection;
        }
        else
        {
            // Bat will follow players x-position
            if (rb2d.position.x < target.position.x)
            {
                moveDirection += Vector2.right;
            }
            else if (rb2d.position.x > target.position.x)
            {
                moveDirection += Vector2.left;
            }

            // Bat will bounces off screen boundaries until their leave time
            if (rb2d.position.x <= leftBound)
            {
                rb2d.AddForce(Vector2.right / 2, ForceMode2D.Impulse);
            }
            else if (rb2d.position.x >= rightBound)
            {
                rb2d.AddForce(Vector2.left / 2, ForceMode2D.Impulse);
            }
        }
    }
}
