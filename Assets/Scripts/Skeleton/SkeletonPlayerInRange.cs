using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPlayerInRange : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<Animator>().SetBool("playerInRange", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<Animator>().SetBool("playerInRange", false);
        }
    }
}
