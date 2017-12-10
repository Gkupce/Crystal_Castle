using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile {

	PlayerHealth health = null;

    public float damage = 1;
    public int bounces = 0;
	public float poisonDamage = 0f;
	public float vampireRecovery = 0f;
    public bool destroyOnHit = true;

	public ParticleSystem[] particles;

	[HideInInspector]
	public bool[] playParticles = new bool [2];



	protected override void OnAwake () {
		health = GameObject.Find ("Player1").GetComponent<PlayerHealth> ();
	}


	protected override void WhenEnabled () {
		for (short i = 0; i < particles.Length; i++) {
            if (playParticles[i])
            {
                particles[i].Stop();
                particles[i].Play();
            }
		}
	}


	private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<EnemyHealth>().TakeDamage(damage);

			if (vampireRecovery > 0) {
				health.GetHealth (vampireRecovery);
				ParticleManager.Instance.EmitLifeStealParticles("LifeSteal", collider.bounds.ClosestPoint (transform.position), 7);

			}
            OnHit(collider);
        }
        else if (bounces > 0)
        {
            bounces--;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, collider.transform.position - transform.position,10f,(1 << collider.gameObject.layer));
            if(hit.collider != null && hit.transform == collider.transform)
            {
                transform.up = Vector3.Reflect(transform.up,hit.normal);
                Debug.DrawRay(hit.point, hit.normal, Color.red, 5f);
                rBody.velocity = transform.up * speed;
            }
        }
        else
        {
            OnHit(collider);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bounces > 0)
        {
            bounces--;
            transform.up = Vector3.Reflect(transform.up, collision.contacts[0].normal);
            Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.red, 5f);
            rBody.velocity = transform.up * speed;
        }
        else
        {
			OnHit(collision.collider);
        }
    }

	void OnHit (Collider2D collider)
    {
        if (poisonDamage != 0f && collider.gameObject.tag == "Enemy")
        {
            collider.GetComponent<EnemyHealth>().SetPoison(poisonDamage);
        }
        //Reset
        bounces = 0;
		vampireRecovery = 0;

        if (destroyOnHit)
        {
            poisonDamage = 0;
			OnBecameInvisible ();
        }
        ParticleManager.Instance.EmitAt("BulletHit", collider.bounds.ClosestPoint(transform.position), 5);
    }


	void OnBecameInvisible () {
		gameObject.SetActive (false);
	}
}
