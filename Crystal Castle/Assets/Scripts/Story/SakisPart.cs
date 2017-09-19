using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakisPart : CharacterScript {

	private string[] saki1 = { 
		"I don't know but,",
		"let's hurry before the castle",
		"starts stinking to him.",
		"The Lord propably is on the last floor.",
		"Come on, stupid brother.",
		"The-",
		"The-the monsters...",
		"The-the monsters... ruined the carpets!!!",
		EVENT,
		"AHHHRRRGGGG!!!!!! NO!",
		"not that paint...",
		"It's from a famous french painter and",
		"its value is 3 millions at least!",
		"He's done it now.",
		"No mercy for this barbaric dumb.",
	};
		


	protected override void OnAwake () {
		character = Enums.Character.Saki;
	}
	

	protected override void ScriptListing () {
		lScripts.Add (saki1);
	}


	public void PlayExalt () {
		anim.SetTrigger ("Exalt");
	}
}
