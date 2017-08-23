using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSwordCollider : MonoBehaviour {

	public Animator anim;

	void OnStart(){
		//anim = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<Animator> ();

	}
 
    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player" && anim.GetBool("isAlive"))
        {
			other.transform.GetComponent<PlayerHealth>().TakeDamage(Random.Range(5,7));
        }

    }
}
