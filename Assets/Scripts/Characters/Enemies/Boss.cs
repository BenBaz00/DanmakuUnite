using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float targetYPos;
    public Animator bossAnimator;
    public List<GameObject> turrets;
    public float beamCooldown;

    private float lastBeamTime;
    private int currTurret;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        GiveInvulnerability(5f);
        currTurret = -1;
    }

    protected override void Update()
    {
        base.Update();

        // Turret Movement
        // -- Change animation variables depending on whether the turret is returning or not
        if (currTurret != -1)
        {
            if (turrets[currTurret].GetComponent<Turret>().IsReturning())
            {
                bossAnimator.SetFloat("movingTurretY", 3f);
                bossAnimator.SetInteger("turretSelect", -1);
                bossAnimator.SetBool("returning", true);
                turrets[currTurret].GetComponent<CircleSpawn>().BeginFiring();
                currTurret = -1;
            }
            else
            {
                bossAnimator.SetFloat("movingTurretY", turrets[currTurret].transform.position.y);
            }
        }

    }

    protected override void Movement()
    {
        // Movement into game view
        if (rb2d.position.y > targetYPos)
        {
            moveDirection += Vector2.down;
        }
        else
        {
            // Once boss has moved into game view, begin chance to fire one of the boss' turret beams
            if (Random.value < .1f && activeTime - lastBeamTime > beamCooldown)
            {
                ChargeBeam();
            }
        }
    }

    // ChargeBeam()
    // --Determines if the left or right turret will charge and plays animations accordingly
    // then invokes the firing of that turrets beam after 2 seconds
    private void ChargeBeam()
    {
        lastBeamTime = activeTime;

        if (Random.value > .5f)
        {
            currTurret = 0;
        }
        else
        {
            currTurret = 1;
        }

        bossAnimator.SetBool("returning", false);
        bossAnimator.SetInteger("turretSelect", currTurret);
        turrets[currTurret].GetComponent<CircleSpawn>().StopFiring();

        Invoke("FireBeam", 2f);
    }

    private void FireBeam()
    {
        turrets[currTurret].GetComponent<Turret>().MoveDown();
    }

}
