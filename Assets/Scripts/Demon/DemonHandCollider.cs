using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHandCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			other.transform.GetComponent<PlayerHealth>().TakeDamage(Random.Range(10,16));
		}
	
	}
}
