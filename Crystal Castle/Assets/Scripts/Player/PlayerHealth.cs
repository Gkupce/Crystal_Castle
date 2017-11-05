using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {

	private UnityEngine.UI.Image life;
	public string lifeUIName = string.Empty;


	private void Start () {
		life = GameObject.Find (lifeUIName).GetComponent<UnityEngine.UI.Image> ();
	}


	protected override void OnHit () {
		life.fillAmount = health / 100f;
	}


    protected override void OnDeath () {
        base.OnDeath ();
        ParticleManager.instance.EmitAt ("EnemyExplosion", transform.position, 10);
        GameController.Instance.PlayerDied ();
        gameObject.SetActive (false);

    }
}
