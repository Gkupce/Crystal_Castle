﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour {

    Transform playerTransform;
    Rigidbody2D rBody;
    public float attackRange = 5f;
    public float movementSpeed = 3f;
    public float damage = 1f;

	void Start () {
        playerTransform = GameObject.FindWithTag("Player").transform;
        rBody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if(!GameController.Instance.allowControl)
		{
			return;
		}

		if(Vector3.Distance(playerTransform.position,transform.position) < attackRange)
        {
            rBody.velocity = (playerTransform.position - transform.position).normalized * movementSpeed;
        }
        else
        {
            rBody.velocity = Vector3.zero;
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Health playerHealth = collision.GetComponentInParent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
