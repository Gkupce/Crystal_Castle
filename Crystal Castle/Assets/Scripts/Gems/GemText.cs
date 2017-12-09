using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemText : MonoBehaviour {


	public void Show (Vector3 pos) {
		transform.position = Camera.main.WorldToScreenPoint (pos);
	}

}
