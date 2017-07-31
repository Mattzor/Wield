using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagCollider : MonoBehaviour {

	public float value;

	AudioSource sfx;
	ParticleSystem ps;

	void Start(){
		sfx = GetComponent<AudioSource>();
		ps = GetComponentInChildren<ParticleSystem> ();
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			ps.Play ();
			other.transform.GetComponent<PlayerHealth> ().HealUp (value);
			sfx.Play ();
			Destroy (gameObject, 0.4f);
		}


	}

}
