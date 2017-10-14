using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health = 5f;


    public void TakeDamage(float ammount)
    {
		print ("P");
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


	public IEnumerator Poisoned(float amount) {
		while (health > 0f) {
			yield return new WaitForSeconds (1f);
			ParticleManager.instance.EmitAt("Poison", transform.position, 7);
			TakeDamage (amount);

			print ("AM " + amount);
		}
	}
}
