﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public bool canAttack = true;
    
	void Update () {
		if(GameController.Instance.allowControl)
		{
			if (Input.GetButtonDown("Fire"))
			{
				OnFireDown();
			}
			if (Input.GetButtonUp("Fire"))
			{
				OnFireUp();
			}
			OnUpdate();
		}
    }

    public virtual void OnFireDown() { }
    public virtual void OnFireUp() { }
    public virtual void OnUpdate() { }
}
