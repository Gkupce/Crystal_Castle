using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class LevelEditor : MonoBehaviour {

	public GameObject levelGO;
	public GameObject currentTile;
	public bool InEditMode;

	public Vector3 pos;

	//private void Awake()
	//{
	//	runInEditMode = true;
	//}

	// Update is called once per frame
	void Update () {
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		Debug.Log(pos);
	}

	//void OnDrawGizmos()
	//{
	//	Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
	//	Gizmos.color = Color.red;
	//	Gizmos.DrawWireCube(pos, Vector3.one);
	//}
}
