using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPauser : Pausable
{
	private float animatorSpeedBeforePause;

	public override void Pause()
	{
		Animator anim = gameObject.GetComponent<Animator>();
		animatorSpeedBeforePause = anim.speed;
		anim.speed = 0;
	}

	public override void Unpause()
	{
		gameObject.GetComponent<Animator>().speed = animatorSpeedBeforePause;
	}
}
