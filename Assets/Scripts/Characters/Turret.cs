using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float targetYPos;
    public float speed;
    public GameObject beam;

    private bool moving;
    private bool returning;
    private Vector2 targetPos;
    private Vector2 originPos;


    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        returning = false;

        targetPos = new Vector2(transform.position.x, targetYPos);
        beam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If the moving bool is true, move down to target position
        if (moving)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if ((Vector2)transform.position == targetPos)
            {
                beam.SetActive(false);
                moving = false;
                returning = true;
            }
        }
        
        // Once the target position is met, the returning bool becomes active and the turret will
        // move back to it's origin position
        if (returning)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, originPos, speed * Time.deltaTime * 3f);

            if ((Vector2)transform.position == originPos) returning = false;
        }
    }

    // MoveDown()
    // --Determines it's current position as the origin 
    //      and sets bool that will allow it to start moving down to target position
    public void MoveDown()
    {
        originPos = new Vector2(transform.position.x, transform.position.y);
        moving = true;

        beam.SetActive(true);
    }

    public bool IsReturning()
    {
        return returning;
    }
}
