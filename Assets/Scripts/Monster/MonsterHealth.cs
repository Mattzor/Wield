using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour {

	public float health;
	float time;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = transform.GetComponent<Animator> ();
		anim.SetBool("isAlive",true);
		time = Time.time;

	}

	public void TakeDamage(float dmg){
		if(anim.GetBool("isAlive") && Time.time - time > 0.3f){
			//anim.SetTrigger ("gotHit");
			time = Time.time;
			health -= dmg;
			if (health <= 0) {
				Die ();
			}
		}
	}

	void Die(){		
		anim.SetBool ("isAlive", false);
		anim.SetBool ("isIdle", false);
		anim.SetBool ("isWalking", false);
		anim.SetBool ("playerInRange", false);
		anim.SetTrigger ("die");
		Destroy (gameObject, 2);
	}

}
