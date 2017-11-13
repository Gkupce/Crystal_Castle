using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    Dictionary<string, ParticleSystem> particles = new Dictionary<string, ParticleSystem>();

    public static ParticleManager Instance;

    private void Awake()
    {
		if (Instance != null && Instance != this)
			Destroy (this);
		else
        	Instance = this;
    }

	void Start () {
		foreach(ParticleSystem s in GetComponentsInChildren<ParticleSystem>())
        {
            particles.Add(s.transform.name,s);
        }
	}
	
    public void EmitAt(string name, Vector3 position, int amount)
    {
        particles[name].transform.position = position;
        particles[name].Emit(amount);
    }


	public void EmitLifeStealParticles(string name, Vector3 position, int amount) {
		//EmitAt (name, position, amount);

		particles[name].transform.position = position;
		particles [name].Stop ();
		particles [name].Play ();

		particles [name].GetComponent<LifeStealParticles> ().enabled = true;
	}
}
