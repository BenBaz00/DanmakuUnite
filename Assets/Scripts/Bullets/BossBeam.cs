using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeam : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character entity = collision.gameObject.GetComponent<Character>();

        if (entity != null)
        {
            if (entity.CompareTag("Player"))
            {
                entity.TakeDamage(damage);
            }
        }
    }
}
