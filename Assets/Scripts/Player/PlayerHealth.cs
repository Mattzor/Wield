using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float health;

    float time;
    GUIText healthText;

    public void Start()
    {
        time = Time.time;
        healthText = GetComponent<GUIText>();
        healthText.text = "Health: " + health.ToString();
        
    }

    public void TakeDamage(float dmg)
    {
        //if (time - Time.time > 0.5)
        //{
            health -= dmg;
            if (health <= 0)
            {
                transform.GetComponent<PlayerController>().Die();
            }
        healthText.text = "Health: " + health.ToString();
            time = Time.time;
        //}
    }
    
  
}
