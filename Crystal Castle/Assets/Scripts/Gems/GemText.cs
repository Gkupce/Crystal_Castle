using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemText : MonoBehaviour {

	public Animator anim;
	public UnityEngine.UI.Text text;


	public void Show (Gem gem) {
		SetText (gem);
		transform.position = Camera.main.WorldToScreenPoint (gem.transform.position);
		anim.SetTrigger ("Show");
	}


	private void SetText (Gem gem) {
		switch (gem.gemType)
		{
			case GemManager.GemType.Speed:
				text.text = "Speed+";
				break;
			case GemManager.GemType.Bouncing:
				text.text = "Bounces+";
				break;
			case GemManager.GemType.Homing:
				text.text = "Homing+";
				break;
			case GemManager.GemType.Poison:
				text.text = "Posion+";
				break;
			case GemManager.GemType.Vampire:
				text.text = "Life Steal+";
				break;
			default:
				text.text = "ERROR";
				break;
		}
	}
}
