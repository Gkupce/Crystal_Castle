using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {

    protected override void OnDeath()
    {
        base.OnDeath();
        ParticleManager.Instance.EmitAt("EnemyExplosion", transform.position, 10);
		GemThrower.Instance.ThrowGem (transform.position);
        Destroy(gameObject);
    }
}
