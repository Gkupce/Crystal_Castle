using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile {

    public float damage = 1;
    public int bounces = 0;
	public float poisonDamage = 0f;
    public bool destroyOnHit = true;

	private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<EnemyHealth>().TakeDamage(damage);
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

        if (destroyOnHit)
        {
            poisonDamage = 0;
            gameObject.SetActive(false);
        }
        ParticleManager.instance.EmitAt("BulletHit", collider.bounds.ClosestPoint(transform.position), 5);
    }
}
