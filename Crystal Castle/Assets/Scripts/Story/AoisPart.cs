using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoisPart : CharacterScript {


	private string[] aoi1 = {
		"Nanny's castle",
		"Think we made it in time?",
		"WHA-",
		"WHA-WHAT HAPPENED HERE???",
		"And sis-",
		"remember that paint you really liked?",
		"1..",
		"1.. 2..",
		"1.. 2.. 3...",
		"3 MILLIONS???",
		"Yeah!",
		"Let's kick him out ASAP."
	};


	protected override void OnAwake () {
		character = Enums.Character.Aoi;
	}
	

	protected override void ScriptListing () {
		lScripts.Add (aoi1);
	}
}
