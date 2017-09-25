using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryPart : MonoBehaviour {

	public static StoryPart Instance;
	protected int[] eventBreak;


	private void Awake () {
		if (Instance == null)
			Instance = this;
		else
			Destroy (this);
	}


	protected virtual void OnAwake () {		}


	private void Start () {
		OnStart ();
	}


	protected virtual void OnStart () {		}


	private void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			End ();
		OnUpdate ();
	}


	protected virtual void OnUpdate () {	}


	public abstract void Init ();


	public abstract void Next ();


	public abstract void End ();


	private void OnDestroy () {
		if (Instance == this)
			Instance = null;
	}
}
