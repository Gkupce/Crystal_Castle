using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile {

    public float damage = 1;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
            OnHit();
        }
        else if (collision.tag != "Player")
        {
            OnHit();
        }
    }

    void OnHit()
    {
        gameObject.SetActive(false);
        ParticleManager.instance.EmitAt("BulletHit", transform.position, 5);
    }
}
