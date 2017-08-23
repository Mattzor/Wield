using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordCollider : MonoBehaviour {

    public float damageMin;
	public float damageMax;
	public float damageCriticalStrike;
	private float damage;

    Transform playerTransform;
	CapsuleCollider swordCapsule;
	PlayerController pc;

    void Start()
    {
        playerTransform = transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent;
		swordCapsule = transform.GetComponent<CapsuleCollider> ();
		pc = playerTransform.GetComponent<PlayerController> ();

    }

	void Update(){
		if (pc.isAttacking) {
			swordCapsule.enabled = true;		
		} else {
			swordCapsule.enabled = false;		
		}
	}

    void OnTriggerEnter(Collider other)
    {
		int i = Random.Range (1, 6);
		if(i==5){
			damage = damageCriticalStrike;
		}else{
			damage = Random.Range (damageMin, damageMax);
		}

		if (other.tag == "SkeletonEnemy" && pc.isAttacking) {
			other.transform.parent.parent.GetComponent<SkelletonHealth> ().TakeDamage (damage);
            
		} else if (other.tag == "MonsterEnemy" && pc.isAttacking) {
			//Destroy(other.transform.gameObject, 1);
			other.transform.GetComponent<MonsterHealth> ().TakeDamage (damage);
		} else if (other.tag == "DemonEnemy" && pc.isAttacking) {
			other.transform.GetComponent<Demon_Health> ().TakeDamage (damage);
		}
    }
}
