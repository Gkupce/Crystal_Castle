using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {

    protected override void OnDeath()
    {
        base.OnDeath();
        ParticleManager.instance.EmitAt("EnemyExplosion", transform.position, 10);
        Destroy(gameObject);
    }
}
