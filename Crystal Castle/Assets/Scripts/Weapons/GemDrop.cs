using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemDrop : MonoBehaviour {

    GemManager gemManager;

	// Use this for initialization
	void Start () {
        gemManager = GetComponent<GemManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("BombL"))
        {
            gemManager.DropGem(0);
        }
        if (Input.GetButtonDown("BombR"))
        {
            gemManager.DropGem(1);
        }
    }
}
