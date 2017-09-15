using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptController : StoryPart {

	public CharacterScript[] charPart;

	public UnityEngine.UI.Text dialogue;
	private Animator anim;

	private int counter = -1;

	private bool run = false;



	protected override void OnAwake () {
		eventBreak[0] = 15;
	}


	protected override void OnStart () {
		anim = GetComponent<Animator> ();
	}



	public override void Init () {
		anim.SetTrigger ("On");
		run = true;
	}



	protected override void OnUpdate () {
		if (run && (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown(0)))
			Next ();
	}


	public override void Next () {
		if (++counter >= Script.Intro.Length) {
			End ();
			return;
		}
		foreach (CharacterScript c in charPart) {
			if (Script.Intro [counter] == c.GetCharacter ()) {
				string line = c.GetLine ();
				if (line == CharacterScript.EVENT) {
					(c as SakisPart).PlayExalt ();
				}
				else
					ChangeText (line);
			}
		}
	}


	private void ChangeText (string t){
		dialogue.text = t;
	}


	public override void End () {
		anim.SetTrigger ("Off");
		enabled = false;
		//gameObject.SetActive (false);
	}
}
