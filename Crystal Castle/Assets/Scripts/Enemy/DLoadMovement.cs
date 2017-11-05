using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLoadMovement : MonoBehaviour {

	private Transform playerTransform;
	private Rigidbody2D rBody;
	private Animator anim;
	public float attackRange = 5f;
	public float movementSpeed = 3f;
	public float damage = 1f;



	private void Start () {
		playerTransform = GameObject.FindWithTag ("Player").transform;
		rBody = GetComponent<Rigidbody2D> ();
		anim = transform.GetComponentInChildren<Animator> ();
	}

	private void Update () {
		if (!GameController.Instance.allowControl)
			return;

		SetAnimation ();

		if (Vector3.Distance (playerTransform.position, transform.position) < attackRange) {
			rBody.velocity = (playerTransform.position - transform.position).normalized * movementSpeed;
		}
		else {
			rBody.velocity = Vector3.zero;
		}
	}

	private void SetAnimation () {
		Vector3 diff = playerTransform.position - transform.position;

		if (Mathf.Abs (diff.x) < Mathf.Abs (diff.y)) {
			if (diff.y > 0)
				anim.SetInteger ("Dir", 1);
			else
				anim.SetInteger ("Dir", 0);
		} else {
			if (diff.x > 0)
				anim.SetInteger ("Dir", 2);
			else
				anim.SetInteger ("Dir", 3);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player") {
			Health playerHealth = collision.GetComponentInParent<Health>();
			if (playerHealth != null) {
				playerHealth.TakeDamage(damage);
			}
		}
	}
}
