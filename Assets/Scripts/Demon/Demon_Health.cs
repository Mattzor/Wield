using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_Health : MonoBehaviour {


	public RectTransform healthBar;

	public float startHealth;
	private float health;

	DemonController dc;

	public bool isHealing;
	private bool demonHeal;
	private float time;

	// Use this for initialization
	void Start () {
		health = startHealth;
		dc = GetComponent<DemonController> ();
		healthBar.parent.parent.gameObject.SetActive (true);
		demonHeal = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


	}


	public void TakeDamage(float dmg){
		
		if (!isHealing && health > 0 && Time.time - time >0.3) {		
			int i = Random.Range (1, 6);
			if (i == 4) {
				return;
			}
			health -= dmg;
			if (health <= 0) {
				dc.Die ();
			}
			if (demonHeal && health < startHealth / 2) {
				demonHeal = false;
				dc.healthLow = true;

			}
			healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
			time = Time.time;
		}


	}

	public void Heal(){
		health += 10;
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
		isHealing = true;
	}

	public void Reset(){
		health = startHealth;
		demonHeal = true;
		healthBar.parent.parent.gameObject.SetActive (false);
	}

	public void Restart(){
		healthBar.parent.parent.gameObject.SetActive (true);
	}

}
