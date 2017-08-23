using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonPlayerInRange : MonoBehaviour {

	private DemonController dc;

	// Use this for initialization
	void Start () {
		dc = transform.parent.GetComponent<DemonController> ();
	}
	

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			dc.currentState = dc.pir;
			dc.newMove = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			dc.currentState = dc.pnir;
			dc.newMove = true;
		}
	}
}
