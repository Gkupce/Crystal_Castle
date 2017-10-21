using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health = 5f;
	private bool poisoned = false;


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
			ParticleManager.instance.EmitAt("Poison", transform.position, 7);
			TakeDamage (amount);
		}
		poisoned = false;
	}
}
