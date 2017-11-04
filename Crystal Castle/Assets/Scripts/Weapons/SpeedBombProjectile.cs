using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedBombProjectile : PlayerProjectile {

	float freq = 2.0f;


	public void Set(Transform player, int qty, int n) {
		float angles = n * 360 / qty;
		float thera = Mathf.Deg2Rad * angles;
		transform.position = (Vector2)player.position + new Vector2 (Mathf.Cos (thera) * (thera > 270 || thera < 90 ? 1 : -1), Mathf.Sin (thera) * (thera < 180 ? 1 : -1));
		transform.eulerAngles = Vector3.forward * angles;
	}


	void Update () {
		transform.Rotate(Vector3.forward, freq);
		transform.Translate (Vector3.up * 1.3f * Time.deltaTime);
	}
}
