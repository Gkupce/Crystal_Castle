﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float health = 5f;

    public void TakeDamage(float ammount)
    {
        health -= ammount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}