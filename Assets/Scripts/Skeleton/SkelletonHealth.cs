using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelletonHealth : MonoBehaviour {

    public float health;

	GameObject gameController;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Animator anim = transform.GetComponent<Animator>();
        //anim.SetBool("isWalking", false);
        //anim.SetBool("Aggro", false);
        //anim.SetBool("isIdle", false);
        anim.SetBool("isAlive", false);
        anim.SetTrigger("die");

        Destroy(gameObject, 3);
    }

}
