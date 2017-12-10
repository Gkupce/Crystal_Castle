using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUnpause : MonoBehaviour {

	public void Unpause () {
		GameController.Instance.Pause ();
	}
}
