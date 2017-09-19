using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {

    protected override void OnDeath()
    {
        base.OnDeath();
        ParticleManager.instance.EmitAt("EnemyExplosion", transform.position, 10);
        GameController.Instance.PlayerDied();
        gameObject.SetActive(false);
    }
}
