using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* HealthUI
 * --Assumes that the Image array's length is equal to the maximum health that the referenced player actually has.
 * --Visual display of the player's health will show/update current health.
 */
public class HealthUI : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image[] hearts;
    

    // UpdateHealth(int)
    // --Updates how many heart sprites should be filled based on the current amount of health
    public void UpdateHealth(int currHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
