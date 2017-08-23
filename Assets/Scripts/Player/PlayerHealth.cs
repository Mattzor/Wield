using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public RectTransform healthBar;
	public float health;
	public bool canTakeDMG;

    float time;   

	AudioSource takeDmgSfx;
	AudioSource recoverHPSfx;
	ParticleSystem ps;


    public void Start()
    {
        time = Time.time;        
		AudioSource[] sounds = GetComponents<AudioSource> ();
		takeDmgSfx = sounds [2];
		ps = GetComponentInChildren<ParticleSystem> ();
    }

    public void TakeDamage(float dmg)
    {
		if (canTakeDMG && Time.time - time > 0.3 && health > 0)
        {
            health -= dmg;
			takeDmgSfx.PlayDelayed (0f);
			if (health <= 0){
                transform.GetComponent<PlayerController>().Die();
            }        

        	time = Time.time;
			healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        }
    }

	public void HealUp(float hp){		
		health += hp;
		ps.Play ();
		if (health > 100) {
			health = 100;
		}
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}


	public void Reset(){
		health = 100;
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}
    
  
}
