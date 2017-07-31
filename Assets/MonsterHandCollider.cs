﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHandCollider : MonoBehaviour {


	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			other.GetComponent<PlayerHealth> ().TakeDamage (15);
		}
	}

}
