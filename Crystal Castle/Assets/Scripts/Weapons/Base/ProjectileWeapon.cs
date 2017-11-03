using UnityEngine;
using System.Collections.Generic;

public class ProjectileWeapon : Weapon {

    public GameObject projectile;
    List<GameObject> projectiles = new List<GameObject>();

    GemManager gemManager;

	// Gem's effects Constants
	private static float HOMING_ROT = 0.7F;
	private static float POISON_DAMAGE = 0.4F;



    private void Start()
    {
        gemManager = GetComponent<GemManager>();
    }


    protected void Shoot()
    {
        GameObject bullet = GetProjectile();

        bullet.transform.position = transform.position;


        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        SetGemsEffect(bullet);
        bullet.SetActive(true);
    }


    void SetGemsEffect(GameObject bullet)
    {
        for (int i = 0; i < gemManager.Types.Length; i++)
        {
            switch (gemManager.Types[i])
            {
                case GemManager.GemType.Homing:
                    Homing h = bullet.GetComponent<Homing>();
					h.rotSpeed = gemManager.Amounts[i] * HOMING_ROT;
                    h.enabled = true;
                    break;
                case GemManager.GemType.Bouncing:
                    PlayerProjectile p1 = bullet.GetComponent<PlayerProjectile>();
                    p1.bounces = gemManager.Amounts[i];
                    p1.enabled = true;
                    break;
			case GemManager.GemType.Poison:
				PlayerProjectile p2 = bullet.GetComponent<PlayerProjectile> ();
				p2.poisonDamage = gemManager.Amounts [i] * POISON_DAMAGE;
					p2.enabled = true;
					break;
            }
        }
    }


    GameObject GetProjectile()
    {
        foreach(GameObject g in projectiles)
        {
            if(g.activeSelf == false)
            {
                return g;
            }
        }
        GameObject go = Instantiate(projectile);
        projectiles.Add(go);
        return go;
    }


    public override void OnFireDown()
    {
        base.OnFireDown();
        Shoot();
    }
}
