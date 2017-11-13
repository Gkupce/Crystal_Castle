using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
	const float MAX_SPEED = 7.0f;
	float speed = MAX_SPEED;
	private Rigidbody2D rBody;
	private Animator anim;

	public ParticleSystem focusParticles;
	public ParticleSystem sweatParticles;
	public float feintCooldown;
	private bool feintAvailable = true;



	private void Start () {
		rBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}



	private void Update () {
		if(!GameController.Instance.allowControl)
		{
			return;
		}
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		if(direction.magnitude > 1)
		{
			direction = direction.normalized;
		}
		if (Input.GetAxis ("Focus") > 0.1f) {
			FocusParticles (true);
			speed = MAX_SPEED / 2.3f;
		} else {
			FocusParticles (false);
			speed = MAX_SPEED;
		}

		if(direction.magnitude > 0.01f)
		{
			rBody.velocity = direction * speed;
		}
		else
		{
			rBody.velocity = Vector3.zero;
		}
		checkAnimation(direction);
	}



	private void checkAnimation(Vector3 direction)
	{
		
		if (direction.magnitude > 0.01f)
		{
			gameObject.GetComponent<Animator>().SetBool("Walking", true);
			if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
			{//Horizontal
				if (direction.x > 0)
				{//Right
					anim.SetInteger("Dir", (int)AnimDir.Right);
				}
				else
				{//Left
					anim.SetInteger("Dir", (int)AnimDir.Left);
				}
			}
			else
			{//Vertical
				if (direction.y > 0)
				{//Up
					anim.SetInteger("Dir", (int)AnimDir.Up);
				}
				else
				{//Down
					anim.SetInteger("Dir", (int)AnimDir.Down);
				}
			}
		}
		else
		{
			anim.SetBool("Walking", false);
		}

		if (feintAvailable && Input.GetButtonDown("Feint"))
		{
			anim.SetTrigger("Feint");
			feintAvailable = false;
		}
	}


	private void FocusParticles (bool status) {
		if (status) {
			if (focusParticles.isStopped)
				focusParticles.Play ();
		} else
			focusParticles.Stop ();
	}


	IEnumerator ActivateFeint(int i){
		yield return new WaitForSeconds(feintCooldown);
		feintAvailable = true;
		sweatParticles.Stop ();
	}


	private void StartSweating(){
		sweatParticles.Play ();
		StartCoroutine (ActivateFeint(0));
	}

	
	enum AnimDir
	{
		Down = 0,
		Up = 1,
		Right = 2,
		Left = 3
	}
}
