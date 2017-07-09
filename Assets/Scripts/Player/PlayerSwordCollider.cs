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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SkeletonEnemy" && playerTransform.GetComponent<PlayerController>().isAttacking)
        {
            other.transform.parent.transform.GetComponent<SkelletonHealth>().TakeDamage(damage);
            
        }else if(other.tag == "MonsterEnemy" && playerTransform.GetComponent<PlayerController>().isAttacking)
        {
            Destroy(other.transform.parent.gameObject, 1);
        }
    }
}
