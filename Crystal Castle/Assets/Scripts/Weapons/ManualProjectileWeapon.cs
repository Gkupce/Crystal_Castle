using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualProjectileWeapon : ProjectileWeapon {
    public override void OnFireDown()
    {
        base.OnFireDown();
        Shoot();
    }
}
