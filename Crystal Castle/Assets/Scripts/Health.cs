using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health = 5f;

    public void TakeDamage(float ammount)
    {
        if (health > 0)
        {
            health -= ammount;
            if (health <= 0)
            {
                OnDeath();
            }
        }
    }

    protected virtual void OnDeath()
    {

    }
}
