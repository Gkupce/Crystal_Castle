﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health = 5f;
	private bool poisoned = false;
	protected bool immortal = false;


    public void TakeDamage(float amount)
    {
        if (health > 0 && !immortal)
        {
			health = Mathf.Clamp(health - amount, 0, 100);
			OnHit ();
            if (health <= 0)
            {
                OnDeath();
            }
        }
    }

    public void GetHealth(float amount)
    {
        health = Mathf.Clamp(health + amount, 0, 100);
        OnHealthUp();
    }

    protected virtual void OnHealthUp() { }

    protected virtual void OnHit () {	}


    protected virtual void OnDeath() {	}


	public void SetPoison(float amount) {
		if (!poisoned) {
			poisoned = true;
            GetComponentInChildren<SpriteRenderer>().color = new Color(0.9f,0.1f,0.9f);
			StartCoroutine (Poisoned (amount));
			StartCoroutine (HealPoison ());
		}
	}


	private IEnumerator Poisoned(float amount) {
        int hits = 7;
		while (health > 0f && hits > 0)
        {
            float waitTime = 0.7f;
            while(waitTime > 0f)
            {
                if (GameController.Instance.allowControl)
                {
                    waitTime -= Time.deltaTime;
                }
                yield return null;
            }
            ParticleManager.Instance.EmitAt("Poison", transform.position, 7);
			TakeDamage (amount);
            hits--;
		}
		poisoned = false;
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }


	private IEnumerator HealPoison()
    {
        while (GameController.Instance.allowControl == false)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds (3.0f);
        while (GameController.Instance.allowControl == false)
        {
            yield return new WaitForEndOfFrame();
        }
        poisoned = false;
	}
}
