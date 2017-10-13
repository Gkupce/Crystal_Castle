using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile {

    public float damage = 1;
    public int bounces = 0;

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
            OnHit();
        }
        else if (bounces > 0)
        {
            bounces--;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, collision.transform.position - transform.position,10f,(1 << collision.gameObject.layer));
            if(hit.collider != null && hit.transform == collision.transform)
            {
                transform.up = Vector3.Reflect(transform.up,hit.normal);
                Debug.DrawRay(hit.point,hit.normal,Color.red,5f);
            }
        }
        else
        {
            OnHit();
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
            OnHit();
        }
    }

    void OnHit () {
        //Reset
        bounces = 0;
        gameObject.SetActive(false);
        ParticleManager.instance.EmitAt("BulletHit", transform.position, 5);
    }
}
