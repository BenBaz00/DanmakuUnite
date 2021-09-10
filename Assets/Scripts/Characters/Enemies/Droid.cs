using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : Enemy
{
    public float timeTurnOffset;
    public float targetYPos;

    private Vector2 leaveDirection;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        DetermineDirection();
    }

    protected override void Movement()
    {
        // Droid will move down to targetYPos, once reached will move towards it's exit direction
        if (rb2d.position.y > targetYPos)
        {
            moveDirection += Vector2.down;
        }
        else
        {
            moveDirection += leaveDirection;
        }
    }

    // DetermineDirection()
    // --Chooses which direction to leave from based on which is the farthest x-axis exit
    void DetermineDirection()
    {
        if (rb2d.position.x > 0)
        {
            leaveDirection = Vector2.left;
        }
        else
        {
            leaveDirection = Vector2.right;
        }
    }
}
