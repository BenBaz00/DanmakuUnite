using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Character
 * -- Used for any entity that life is tied to health. Typically a character will be able to take damage.
 * -- In a standard case, damage is taken through a collision event between two entities (for example, projectile hit)
 * unless the character is invulnerable.
 */
public abstract class Character : MonoBehaviour
{
    public int health = 3;
    private bool invulnerable = false;

    // TakeDamage()
    // --Called by collider triggers to deal damage to character that has been hit with a projectile
    public void TakeDamage(int damage)
    {
        if (invulnerable) return;

        health -= damage;

        HandleDamage();

        // If character has no more health, handle their destruction
        if (health <= 0)
        {
            HandleDestruction();
        }
    }

    protected abstract void HandleDamage();
    protected abstract void HandleDestruction();

    // GiveInvulnerability() 
    // --Gives Entity Invulnerability over taking damage for a set amount of time before removing it
    public void GiveInvulnerability(float time)
    {
        invulnerable = true;

        Invoke("RemoveInvulnerability", time);
    }

    public void SetInvulnerability(bool i)
    {
        invulnerable = i;
    }

    private void RemoveInvulnerability()
    {
        invulnerable = false;
    }
}
