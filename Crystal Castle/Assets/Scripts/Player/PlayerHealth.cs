using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {

	private HeartUIManager heart = null;
	public string heartUIName = string.Empty;


	private void Start () {
		heart = GameObject.Find (heartUIName).GetComponent<HeartUIManager> ();
	}


	protected override void OnHit () {
		StartCoroutine (Immortal ());
		heart.UpdateHeart (health / 100f);
	}


	private IEnumerator Immortal () {
		immortal = true;
		yield return new WaitForSeconds (1f);
		immortal = false;
	}


    protected override void OnDeath () {
        base.OnDeath ();
        ParticleManager.Instance.EmitAt ("EnemyExplosion", transform.position, 10);
        GameController.Instance.PlayerDied ();
        gameObject.SetActive (false);

    }
}
