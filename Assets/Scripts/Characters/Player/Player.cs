using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player
 * -- Player extends Character, as it is an entity that will take damage from enemy projectiles
 * -- The Player's information will need to be displayed to the user, So the player component needs
 * access to the health bar UI and the Game Over screen
 */
public class Player : Character
{
    public const int MaxPlayerHealth = 3;

    public GameObject GameOverUI;
    public float invulnerabilityTime;
    private HealthUI healthUI;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxPlayerHealth;

        healthUI = GameObject.Find("Health").GetComponent<HealthUI>();
    }

    protected override void HandleDamage()
    {
        // Update the Health UI
        healthUI.UpdateHealth(health);

        // Invulnerability Timer
        GiveInvulnerability(invulnerabilityTime);
    }

    // HandleDestruction()
    // --When the player is destroyed, the game is frozen and the Game Over UI panel is shown
    protected override void HandleDestruction()
    {
        Debug.Log("Player: HandleDestruction: Game Over");
        Time.timeScale = 0f;
        PauseMenu.allowPause = false;

        Destroy(this.gameObject);

        GameOverUI.SetActive(true);
        
    }
}
