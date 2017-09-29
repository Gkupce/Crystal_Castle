using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    
	void Update () {
        if (Input.GetButton("Fire1"))
        {
			StartCoroutine(Fire());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            OnFireUp();
        }
        OnUpdate();
    }

	IEnumerator Fire()
	{
		OnFireDown();
		yield return new WaitForSeconds(0.7f);
	}

    public virtual void OnFireDown() { }
    public virtual void OnFireUp() { }
    public virtual void OnUpdate() { }
}
