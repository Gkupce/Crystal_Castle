using UnityEngine;
using System.Collections;


public class Gem : MonoBehaviour {
	
    public GemManager.GemType gemType;
	private Animator anim;


	private void Awake () {
		anim = transform.GetComponentInChildren<Animator> ();
	}


    public void GrabAnim()
    {
        GetComponent<Collider2D>().enabled = false;
		anim.SetTrigger ("Grab");
        ParticleManager.Instance.EmitAt("GemGrab", transform.position, 7);
        Destroy(gameObject, 0.2f);
    }


	public IEnumerator Vanish () {
        while (GameController.Instance.allowControl == false)
        {
            continue;
        }
		yield return new WaitForSeconds (3f);
        while (GameController.Instance.allowControl == false)
        {
            continue;
        }
        anim.SetTrigger ("Vanish");
		Destroy (gameObject, 2f);
	}
}
