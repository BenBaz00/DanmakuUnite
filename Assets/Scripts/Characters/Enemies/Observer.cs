using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : Enemy
{
    public float targetYPos;

    public bool disableLeaving = false;
    public float leaveTime;
    public Vector2 leaveDirection;
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if (leaveDirection == null)
        {
            leaveDirection = RandomXDirection();
        }
       
    }

    protected override void Movement()
    {
        // Observer will move down towards it's target Y position
        if (rb2d.position.y > targetYPos)
        {
            moveDirection += Vector2.down;
        }

        // After leaveTime is met, 
        // if allowed to leave then observer will leave in a random left/right direction
        if (activeTime > leaveTime && !disableLeaving)
        {
            moveDirection += leaveDirection;
        }

    }

}
