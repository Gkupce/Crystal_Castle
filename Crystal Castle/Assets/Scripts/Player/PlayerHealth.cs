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
		ParticleManager.Instance.EmitAt ("EnemyExplosion", transform.position, 4);
		StartCoroutine (Immortal ());
		heart.UpdateHeart (health / 100f);
	}

    protected override void OnHealthUp()
    {
        base.OnHealthUp();
        heart.UpdateHeart(health / 100f);
    }


    private IEnumerator Immortal () {
		immortal = true;
        while (GameController.Instance.allowControl == false)
        {
            continue;
        }
        yield return new WaitForSeconds (1f);
        while (GameController.Instance.allowControl == false)
        {
            continue;
        }
        immortal = false;
	}


    protected override void OnDeath () {
        base.OnDeath ();
        ParticleManager.Instance.EmitAt ("EnemyExplosion", transform.position, 10);
        GameController.Instance.PlayerDied ();
        gameObject.SetActive (false);

    }
}
