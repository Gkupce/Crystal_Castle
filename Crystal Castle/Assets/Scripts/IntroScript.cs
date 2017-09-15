using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour {


	public static IntroScript Instance;

	public UnityEngine.UI.Text dialogue;
	private Script script = new Script ();
	private Animator anim;

	private int counter = -1;
	private int akaiCounter = -1;
	private int aoiCounter = -1;

	private bool run = false;



	private void Awake () {
		if (Instance == null)
			Instance = this;
		else
			Destroy (this);
	}



	private void Start () {
		anim = GetComponent<Animator> ();
	}



	private void OnDestroy () {
		if (Instance == this)
			Instance = null;
	}



	public void Init () {
		anim.SetTrigger ("On");
		run = true;
	}



	private void Update () {
		if (run && (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown(0)))
			Next ();
	}


	private void Next () {
		if (++counter >= script.intro.Length) {
			End ();
			return;
		}

		if (script.intro [counter] == Enums.Character.Aoi) {
			ChangeText ("Akai-\n" + script.aoi1 [++akaiCounter]);
		} else if (script.intro [counter] == Enums.Character.Saki) {
			ChangeText ("Aoi-\n" + script.saki1 [++aoiCounter]);

		}
	}


	private void ChangeText (string t){
		dialogue.text = t;
	}


	private void End () {
		anim.SetTrigger ("Off");
		enabled = false;
		//gameObject.SetActive (false);
	}
}
