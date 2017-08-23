using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagCollider : MonoBehaviour {

	public float value;

	AudioSource sfx;


	void Start(){
		sfx = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {			
			other.transform.GetComponent<PlayerHealth> ().HealUp (value);
			sfx.Play ();
			Destroy (gameObject, 0.4f);
		}


	}

}
