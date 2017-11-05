using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController Instance;
    public LevelManager levelManager;
    public GameObject player1;

    public bool allowControll {
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
                return;
            }
        #endif
		if (Instance != null && Instance != this)
			Destroy (this);
		else
			Instance = this;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if(levelManager != null)
		{
			PrepareNextLevel();
		}
		else
		{
			anim.SetTrigger("FadeOut");
		}
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

	public void LoadNextLevel()
	{
		inCinematic = true;
		currentCinematic = cinematics.NextLevel;
		anim.SetTrigger("FadeOut");
	}

	private void PrepareNextLevel()
	{
		levelManager.LoadNextLevel();
		player1.transform.position = levelManager.loadedLevel.getPlayer1InitialPos();
		StartCinematic(cinematics.FadeIn);
	}

	public void StartCinematic(cinematics cinematicToStart)
	{
		inCinematic = true;
		currentCinematic = cinematicToStart;
		if(currentCinematic == cinematics.FadeOut 
			|| currentCinematic == cinematics.FadeIn
			|| currentCinematic == cinematics.NextLevel
		) {
			anim.SetTrigger("FadeOut");
		}

	}

	public void AfterCinematic()
	{
		if(currentCinematic == cinematics.FadeOut)
		{
			StartCinematic(cinematics.FadeIn);
		}
		else if(currentCinematic == cinematics.NextLevel)
		{
			PrepareNextLevel();
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
		NextLevel,
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
