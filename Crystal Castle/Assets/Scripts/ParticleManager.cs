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
	
    public void EmitAt(string name ,Vector3 position,int ammount)
    {
        particles[name].transform.position = position;
        particles[name].Emit(ammount);
    }
}
