using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public bool canAttack = true;
    
	void Update () {
		if(GameController.Instance.allowControll)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				OnFireDown();
			}
			if (Input.GetButtonUp("Fire1"))
			{
				OnFireUp();
			}
			OnUpdate();
		}
    }

    public virtual void OnFireDown() { }
    public virtual void OnFireUp() { }
    public virtual void OnUpdate() { }
}
