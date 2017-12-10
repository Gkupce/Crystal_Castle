using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryIntro : StoryPart {

	public CharacterScript[] charPart;

	public UnityEngine.UI.Text dialogue;
	private Animator anim;
    public Animator fade;


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
	}



	protected override void OnUpdate () {
		if (run && (Input.GetButtonDown("Proceed") || Input.GetButtonDown("Fire")))
			Next ();
	}


	public override void Next () {
        run = true;
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
        StartCoroutine(Finish());
    }


    IEnumerator Finish()
    {
        while (GameController.Instance.allowControl == false)
        {
            continue;
        }
        yield return new WaitForSeconds(1f);
        while (GameController.Instance.allowControl == false)
        {
            continue;
        }
        fade.SetTrigger("FadeOut");
        NextScene();
    }


    private void NextScene() {
        SceneManager.LoadScene((int)Enums.Scenes.GAME);
    }
}
