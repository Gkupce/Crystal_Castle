using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    protected Rigidbody2D rBody;
    public float speed = 10f;

    float deleteTimer = 5f;
    
	private void Awake () {
        rBody = GetComponent<Rigidbody2D>();
		OnAwake ();
	}

	protected virtual void OnAwake () {	}

    private void OnEnable()
    {
        deleteTimer = 5f;
		CalculateSpeed();
		WhenEnabled ();
    }

	protected virtual void WhenEnabled () {	}

    private void Update()
    {
        if(GameController.Instance.allowControl == false)
        {
            return;
        }
        deleteTimer -= Time.deltaTime;
        if (deleteTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

	public void CalculateSpeed()
	{
		rBody.velocity = transform.up * speed;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + transform.up);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + transform.right);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, transform.position + transform.forward);
	}
}
