using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    Rigidbody2D rBody;
    public float speed = 10f;

    float deleteTimer = 5f;
    
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        rBody.velocity = transform.up * speed;
	}

    private void OnEnable()
    {
        deleteTimer = 5f;
    }

    private void Update()
    {
        deleteTimer -= Time.deltaTime;
        if (deleteTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
