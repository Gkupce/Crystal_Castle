using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemThrower : MonoBehaviour {

	public GameObject[] gemsPrefs = null;
	public static GemThrower Instance = null;

	private int throwChance = 30;


	private void Awake () {
		if (Instance != null && Instance != this)
			Destroy (this);
		else
			Instance = this;
	}


	public void ThrowGem (Vector3 pos) {
		if (Random.Range (0, 100) < throwChance)
			GetGem ().transform.position = pos;
	}


	private Gem GetGem () {
		int r = Random.Range (0, gemsPrefs.Length);
		Gem gem = Instantiate (gemsPrefs [r]).GetComponent<Gem> ();
		gem.StartCoroutine (gem.Vanish ());
		return gem;
	}
}
