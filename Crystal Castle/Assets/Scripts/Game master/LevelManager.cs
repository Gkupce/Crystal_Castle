using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public Level[] levels;

	[HideInInspector]
	public Level loadedLevel = null;

	int currentLevel = 0;

	public void LoadNextLevel()
	{
		if(loadedLevel != null)
		{
			GameObject.Destroy(loadedLevel.gameObject);
		}
		if(currentLevel < levels.Length)
		{
			loadedLevel = (Level)GameObject.Instantiate(levels[currentLevel]);
			currentLevel++;
		}
		else
		{
			loadedLevel = null;
		}
	}
}
