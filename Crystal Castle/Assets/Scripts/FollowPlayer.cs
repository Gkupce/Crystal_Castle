using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	public GameObject player;
    public float allowedDistance = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
    
    // Update is called once per frame
    private void LateUpdate()
    {
        if (GameController.Instance.allowControl)
        {
            Vector3 playerPos = new Vector3(
                player.transform.position.x,
                player.transform.position.y,
                transform.position.z
            );
            Vector3 nextPos;

            if (Vector3.Distance(transform.position, playerPos) > allowedDistance)
            {
                Vector3 diference = playerPos - transform.position;
                nextPos = transform.position + (diference.normalized * (diference.magnitude - allowedDistance));
            }
            else
            {
                nextPos = transform.position;
            }
            transform.position = nextPos;
        }
        else
        {
            //Do what should be done
        }
	}
}
