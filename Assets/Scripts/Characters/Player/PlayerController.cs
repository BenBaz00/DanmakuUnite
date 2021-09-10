using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float focusSpeed;

    private bool focused;
    private Rigidbody2D rb2d;
    private Queue<LineSpawn> playerFire;

    void Start()
    {
        focused = false;

        rb2d = GetComponent<Rigidbody2D>();
        var weapons = GetComponentsInChildren<LineSpawn>();

        playerFire = new Queue<LineSpawn>();
        for (int i=0; i < weapons.Length; i++)
        {
            playerFire.Enqueue(weapons[i]);
        }
    }
    void FixedUpdate()
    {
        // Player Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Update Player Position
        Vector2 movement;
        if (focused)
        {
            movement = new Vector2(moveHorizontal * focusSpeed, moveVertical * focusSpeed);
        }
        else
        {
            movement = new Vector2(moveHorizontal * speed, moveVertical * speed);
        }
        
        rb2d.MovePosition((Vector2)transform.position + movement);
    }

    private void Update()
    {
        if (PauseMenu.GamePaused == true) return;

        // Primary Weapon Fire
        if (Input.GetButton("Fire1"))
        {
            playerFire.Enqueue(playerFire.Peek());
            playerFire.Dequeue().FireLine(0);
        }

        // Focus Movement
        if (Input.GetButton("Fire3"))
        {
            focused = true;
        }
        else
        {
            focused = false;
        }
    }

}
