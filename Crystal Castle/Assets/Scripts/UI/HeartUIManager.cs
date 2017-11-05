using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUIManager : MonoBehaviour {

	public Animator anim;
	public UnityEngine.UI.Image heart;



	public void UpdateHeart (float amount) {
		heart.fillAmount = amount;
		anim.SetTrigger ("Hit");
	}
}
