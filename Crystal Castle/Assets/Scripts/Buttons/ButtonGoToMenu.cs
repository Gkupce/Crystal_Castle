using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGoToMenu : MonoBehaviour {
	
	public void onGoToMenuClick()
	{
		SceneManager.LoadScene(0);
	}
}
