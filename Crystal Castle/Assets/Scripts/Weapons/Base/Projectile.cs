using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    protected Rigidbody2D rBody;
    public float speed = 10f;

    float deleteTimer = 5f;
    
	void Awake () {
        rBody = GetComponent<Rigidbody2D>();
	}

    private void OnEnable()
    {
        deleteTimer = 5f;
        rBody.velocity = transform.up * speed;
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
