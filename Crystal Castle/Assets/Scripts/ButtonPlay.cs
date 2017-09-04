using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class ButtonPlay : MonoBehaviour {

	public void Play() {
		SceneManager.LoadScene ((int)ScenesList.Scenes.GAME);
	}

}
