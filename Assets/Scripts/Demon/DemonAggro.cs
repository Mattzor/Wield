using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAggro : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			DemonController dc = transform.parent.GetComponent<DemonController> ();
			dc.playerTransform = other.GetComponent<Transform>();
			dc.currentState = dc.pnir;
			dc.newMove = true;
			GetComponent<BoxCollider> ().enabled = false;
		}
	}

	public void Reset(){
		GetComponent<BoxCollider> ().enabled = true;
	}
}
