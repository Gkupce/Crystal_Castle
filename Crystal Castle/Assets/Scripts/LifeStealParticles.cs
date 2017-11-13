using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealParticles : MonoBehaviour {

	public Transform hearth;
	Camera cam;

	Vector3 hPos;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		hPos = cam.ScreenToWorldPoint (hearth.position + Vector3.forward * 10);
		transform.position = Vector3.Lerp (transform.position,hPos,10*Time.deltaTime);
		if (Vector3.Distance (transform.position, hPos) < 0.5f) {
			enabled = false;
		}
	}
}
