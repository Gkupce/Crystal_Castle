using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	
	public bool allowControll {
		get {
			return inCinematic || pause;
		}
	}

	public bool pause = false;
	public bool inCinematic = true;

	private cinematics currentCinematic = cinematics.FadeIn;

	public void StartCinematic(cinematics cinematicToStart)
	{
		inCinematic = true;
		currentCinematic = cinematicToStart;
		//TODO actually start cinematic
	}

	public void AfterCinematic()
	{
		if(currentCinematic == cinematics.FadeOut)
		{
			StartCinematic(cinematics.FadeIn);
			currentCinematic = cinematics.FadeIn;
		}
		else
		{
			currentCinematic = cinematics.None;
			inCinematic = false;
		}
	}


	public enum cinematics
	{
		Start,
		FadeIn,
		FadeOut,
		End,
		GoingToStairs,
		None
	}
}
