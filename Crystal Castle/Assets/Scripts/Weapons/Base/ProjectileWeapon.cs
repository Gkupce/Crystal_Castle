using UnityEngine;
using System.Collections.Generic;

public class ProjectileWeapon : Weapon {

    public GameObject projectile;
    List<GameObject> projectiles = new List<GameObject>();

    protected void Shoot()
    {
        GameObject bullet = GetProjectile();

        bullet.transform.position = transform.position;
        
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        bullet.SetActive(true);
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
