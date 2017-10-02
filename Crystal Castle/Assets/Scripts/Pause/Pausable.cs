using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pausable : MonoBehaviour {
	public abstract void Pause();
	public abstract void Unpause();
}
