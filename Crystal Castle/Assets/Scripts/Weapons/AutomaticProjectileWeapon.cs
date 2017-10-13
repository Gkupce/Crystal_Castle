using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticProjectileWeapon : ProjectileWeapon
{
    bool isFiring = false;
    public float fireCooldown = 0.5f;
    public float minimumCooldown = 0.05f;
    float coolDownReduction = 0f;
    float actualCooldown = 0f;

    public override void OnFireDown()
    {
        isFiring = true;
    }

    public override void OnFireUp()
    {
        isFiring = false;
    }

    public override void OnUpdate()
    {
        actualCooldown = Mathf.Clamp(actualCooldown - Time.deltaTime, 0, actualCooldown);
		if (canAttack == false) {
			return;
		}
        if (isFiring)
        {
            if (actualCooldown <= 0)
            {
                Shoot();
                actualCooldown = fireCooldown - coolDownReduction;
            }
        }
    }

    public void ReduceCooldown(float amount)
    {
        if (fireCooldown - (coolDownReduction + amount) >= minimumCooldown)
        {
            coolDownReduction += amount;
            actualCooldown = Mathf.Min(actualCooldown, fireCooldown - coolDownReduction);
        }
    }
    /*
	public void RemoveCooldown () {
		coolDownReduction = 0.0f;
	}

	public void RemoveProperty (GemManager.GemType type) {
		switch (type) {
			case GemManager.GemType.Speed:
				RemoveCooldown ();
				break;
			case GemManager.GemType.Bouncing:
				RemoveBounces ();
				break;
		}
	}*/
}
