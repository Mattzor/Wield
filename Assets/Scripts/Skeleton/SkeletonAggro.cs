using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAggro : MonoBehaviour {

    bool calm = true;


    private void OnTriggerEnter(Collider other)
    {
        if (calm && other.tag == "Player")
        {
			transform.parent.GetComponent<SkeletonController> ().playerTransform = other.GetComponent<Transform>();
			transform.parent.GetComponent<SkeletonController> ().playerDead = true;
			transform.parent.GetComponent<Animator> ().SetBool("Aggro", true);
            calm = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            calm = true;
			transform.parent.GetComponent<SkeletonController> ().playerTransform = null;
			transform.parent.GetComponent<SkeletonController> ().playerDead = false;
			transform.parent.GetComponent<Animator> ().SetBool("Aggro", false);
			transform.parent.GetComponent<Animator> ().SetBool("playerInRange", false);

        }
    }
}
