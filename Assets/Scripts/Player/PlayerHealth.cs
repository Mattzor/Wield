using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float health;

    float time;
    GUIText healthText;
	AudioSource takeDmgSfx;
	AudioSource recoverHPSfx;

    public void Start()
    {
        time = Time.time;
        healthText = GetComponent<GUIText>();
        healthText.text = "Health: " + health.ToString();
		AudioSource[] sounds = GetComponents<AudioSource> ();
		takeDmgSfx = sounds [2];
		//recoverHPSfx = sounds [3];
    }

    public void TakeDamage(float dmg)
    {
        if (Time.time - time > 0.5)
        {
            health -= dmg;
			takeDmgSfx.PlayDelayed (0f);
            if (health <= 0)
            {
                transform.GetComponent<PlayerController>().Die();
            }
        healthText.text = "Health: " + health.ToString();

        time = Time.time;
        }
    }

	public void HealUp(float hp){		
		health += hp;
		if (health > 100) {
			health = 100;
		}
		//recoverHPSfx.PlayDelayed(0f);
	}
    
  
}
