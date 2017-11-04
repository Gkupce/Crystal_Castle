using UnityEngine;

public class Gem : MonoBehaviour {
	
    public GemManager.GemType gemType;

    public void GrabAnim()
    {
        GetComponent<Collider2D>().enabled = false;
        transform.GetChild(0).GetComponent<Animator>().Play("GemGrab");
        ParticleManager.instance.EmitAt("GemGrab", transform.position, 7);
        Destroy(gameObject, 0.2f);
    }
}
