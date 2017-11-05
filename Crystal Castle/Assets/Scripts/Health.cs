using System.Collections;
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


	protected virtual void OnHit () {	}


    protected virtual void OnDeath() {	}


	public void SetPoison(float amount) {
		if (!poisoned) {
			poisoned = true;
			StartCoroutine (Poisoned (amount));
		}
	}


	IEnumerator Poisoned(float amount) {
		while (health > 0f) {
			yield return new WaitForSeconds (0.7f);
			ParticleManager.Instance.EmitAt("Poison", transform.position, 7);
			TakeDamage (amount);
		}
		poisoned = false;
	}
}
