using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSwordCollider : MonoBehaviour {

 
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<PlayerHealth>().TakeDamage(10);
        }
    }
}
