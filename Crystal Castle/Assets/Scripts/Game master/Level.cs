using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public Vector3 player1StartPos;

	public Vector3 getPlayer1InitialPos()
	{
		return transform.position + player1StartPos;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(getPlayer1InitialPos(), Vector3.one);
	}
}
