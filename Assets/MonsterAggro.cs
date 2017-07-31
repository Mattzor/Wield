using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAggro : MonoBehaviour {

	bool calm = true;
	Animator anim;

	void Start()
	{
		anim = transform.root.GetComponent<Animator> ();	
	}

	private void OnTriggerEnter(Collider other)
	{
		if (calm && other.tag == "Player")
		{
			transform.root.GetComponent<MonsterController>().playerTransform = other.GetComponent<Transform>();
			anim.SetBool("aggro", true);
			calm = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			calm = true;
			transform.root.GetComponent<MonsterController>().playerTransform = null;
			anim.SetBool("aggro", false);
			anim.SetBool ("playerInRange", false);
			anim.SetBool ("isIdle", true);


		}
	}
}
