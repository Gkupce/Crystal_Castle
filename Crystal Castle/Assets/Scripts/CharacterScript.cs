using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterScript : MonoBehaviour {

	public const string EVENT = "%";

	private int counter = 0;
	private int scriptCounter = 0;
	public List<string[]> lScripts = new List<string[]> ();

	protected Enums.Character character;
	protected Animator anim;

	private void Awake () {
		OnAwake ();
		anim = GetComponent <Animator> ();
		ScriptListing ();
	}


	protected virtual void OnAwake () {		}


	protected abstract void ScriptListing ();


	public Enums.Character GetCharacter () {
		return character;
	}


	public void SetCounter(int counter) {
		this.counter = counter;
	}


	public int GetCounter() {
		return counter;
	}


	protected int GetScriptCounter () {
		return scriptCounter;
	}


	public void NextScript () {
		scriptCounter++;
	}


	public string GetLine () {
		string line = lScripts [scriptCounter] [counter++];
		if (line == "%")
			return line;
		return name + "-\n" + line;
	}


	protected virtual void InitDialogue () {
		StoryIntro.Instance.Init ();
	}
		

	protected void Continue() {
		StoryIntro.Instance.Next ();
	}
}
