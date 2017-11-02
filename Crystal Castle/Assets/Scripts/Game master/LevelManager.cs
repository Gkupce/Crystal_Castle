using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public Level[] levels;
	public Level loadedLevel;

	int currentLevel = 0;

	public void LoadNextLevel()
	{
		if(loadedLevel != null)
		{
			GameObject.Destroy(loadedLevel);
		}
		loadedLevel = (Level)GameObject.Instantiate(levels[currentLevel]);
		currentLevel++;
	}
}
