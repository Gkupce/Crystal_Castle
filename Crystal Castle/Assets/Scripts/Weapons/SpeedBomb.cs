using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBomb : MonoBehaviour {

	public GameObject projectile;


	public void Explode (int qty) {
		for (int e = 0; e < qty; e++) {
			GameObject bomb = Instantiate(projectile, transform.position, Quaternion.identity);
			bomb.GetComponent<SpeedBombProjectile> ().Set (transform, qty, e);
			Destroy (bomb, 1f);
		}
	}
}
