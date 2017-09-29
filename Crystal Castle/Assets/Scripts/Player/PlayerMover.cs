using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
	public float maxSpeed = 1.0f;
    Rigidbody2D rBody;
	
	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!GameController.Instance.allowControll)
        {
            return;
        }
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		if(direction.magnitude > 1)
		{
			direction = direction.normalized;
		}
		if(direction.magnitude > 0.01f)
		{
            rBody.velocity = direction * maxSpeed;
        }
        else
        {
            rBody.velocity = Vector3.zero;
        }
        checkAnimation(direction);
	}
	
	void checkAnimation(Vector3 direction)
	{
		if (direction.magnitude > 0.01f)
		{
			gameObject.GetComponent<Animator>().SetBool("Walking", true);
			if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
			{//Horizontal
				if (direction.x > 0)
				{//Right
					gameObject.GetComponent<Animator>().SetInteger("Dir", (int)AnimDir.Right);
				}
				else
				{//Left
					gameObject.GetComponent<Animator>().SetInteger("Dir", (int)AnimDir.Left);
				}
			}
			else
			{//Vertical
				if (direction.y > 0)
				{//Up
					gameObject.GetComponent<Animator>().SetInteger("Dir", (int)AnimDir.Up);
				}
				else
				{//Down
					gameObject.GetComponent<Animator>().SetInteger("Dir", (int)AnimDir.Down);
				}
			}
		}
		else
		{
			gameObject.GetComponent<Animator>().SetBool("Walking", false);
		}
	}

	enum AnimDir
	{
		Down = 0,
		Up = 1,
		Right = 2,
		Left = 3
	}
}
