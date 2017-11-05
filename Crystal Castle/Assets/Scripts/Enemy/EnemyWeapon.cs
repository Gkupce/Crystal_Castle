using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    public string projectileName;
    public float projectileSpeed = 4f;
    public int projectileAmountPerAttack = 1;
    public float angleBetweenProjectiles = 10f;
    public float attackRange = 5f;
    public float attackDelay = 2f;
	public bool archRange = false;

    float actualDelay = 0;
    Transform playerTransform;

	float rot_z = 0f;
    

	void Start () {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void OnEnable()
    {
        actualDelay = attackDelay;
    }

    void Update () {
        if (actualDelay > 0)
        {
            actualDelay -= Time.deltaTime;
            if (actualDelay > 0)
            {
                return;
            }
        }
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            actualDelay = attackDelay;
            Shoot();
        }
	}

    void Shoot()
    {
        int i = projectileAmountPerAttack - 1;

        Vector3 diff = playerTransform.position - transform.position;
        diff.Normalize();

        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

		if (archRange)
			ShootRange (i);
		else
			ShootAllAround (i);
    }


	private void ShootAllAround (int i) {
		while (i >= 0)
		{
			GameObject b = GameObjectPools.instance.GetPooledObject(projectileName);
			b.transform.position = transform.position;
			b.transform.rotation = Quaternion.Euler(0, 0, rot_z + angleBetweenProjectiles * i);
			b.SetActive(true);
			i--;
		}
	}


	private void ShootRange (int i) {
		int pair = i / 2;
		int a = 0;
		int k = -1;
		rot_z -= 90;

		while (i >= 0)
		{
			GameObject b = GameObjectPools.instance.GetPooledObject(projectileName);
			b.transform.position = transform.position;
			b.transform.rotation = Quaternion.Euler(0, 0, rot_z + angleBetweenProjectiles * pair * k);
			k *= -1;

			if (++a % 2 == 0) {
				pair--;
			}

			b.SetActive(true);
			i--;
		}
	}
}
