using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyPauser : Pausable
{
	Vector2 vel;
	float angularVel;

	public override void Pause()
	{
		Rigidbody2D r = gameObject.GetComponent<Rigidbody2D>();
		angularVel = r.angularVelocity;
		r.angularVelocity = 0;
		vel = r.velocity;
		r.velocity = Vector2.zero;
		r.isKinematic = true;
	}

	public override void Unpause()
	{
		Rigidbody2D r = gameObject.GetComponent<Rigidbody2D>();
		r.isKinematic = false;
		r.angularVelocity = angularVel;
		r.velocity = vel;
	}
}
