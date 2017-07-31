using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordCollider : MonoBehaviour {

    public float damage;

    Transform playerTransform;

    void Start()
    {
        playerTransform = transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent;
    }

	void Update(){
		if (playerTransform.GetComponent<PlayerController> ().isAttacking) {
			transform.GetComponent<CapsuleCollider> ().enabled = true;		
		} else {
			transform.GetComponent<CapsuleCollider> ().enabled = false;		
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SkeletonEnemy" && playerTransform.GetComponent<PlayerController>().isAttacking)
        {
            other.transform.parent.parent.GetComponent<SkelletonHealth>().TakeDamage(damage);
            
        }else if(other.tag == "MonsterEnemy" && playerTransform.GetComponent<PlayerController>().isAttacking)
        {
            //Destroy(other.transform.gameObject, 1);
			other.transform.GetComponent<MonsterHealth>().TakeDamage(damage);
        }
    }
}
