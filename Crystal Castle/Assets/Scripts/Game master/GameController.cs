using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController Instance;

    public bool allowControl {
		get {
			return !(inCinematic || pause || playerIsDead);
		}
	}
    
	public bool pause = false;
	public bool inCinematic = true;
    private bool playerIsDead = false;

	private cinematics currentCinematic = cinematics.FadeIn;

    Animator anim;

    private void Awake()
    {
        #if UNITY_EDITOR
            if (Instance != null)
            {
                Debug.LogError("The game master is present twice in the scene.");
            }
        #endif
        Instance = this;
        anim = GetComponent<Animator>();
    }

	private void Update()
	{
		if (Input.GetButtonUp("Pause"))
		{
			Pausable[] pausables = GameObject.FindObjectsOfType<Pausable>();
			foreach (Pausable p in pausables)
			{
				if (pause)
				{
					p.Unpause();
				}
				else
				{
					p.Pause();
				}
			}
			pause = !pause;
			anim.SetBool("Paused", pause);
		}
	}

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

    public void PlayerDied()
    {
        playerIsDead = true;
        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
		SceneManager.LoadScene((int)Enums.Scenes.MENU);
    }
}
