using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {


	public float releaseTime;
	public float speed;

	private float startTime;
	public Vector3 direction;

	Rigidbody fireballRigidbody;

	// Use this for initialization
	void Start () {
		fireballRigidbody = GetComponent<Rigidbody> ();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 movement = direction * speed * Time.deltaTime;
		fireballRigidbody.MovePosition(transform.position + movement);
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.root.tag == "DemonEnemy" || other.transform.root.tag == "SkeletonRoot" || other.transform.root.tag == "MonsterEnemy") {
			return;
		} else if (other.tag == "Player") {
			other.transform.GetComponent<PlayerHealth> ().TakeDamage (20.0f);
			Debug.Log ("Fireball collided with player");		
		} 
		//Debug.Log (other.transform.root.gameObject.name);
		Destroy (gameObject, 0.2f);
	}

	public void SetDirection(float x, float y, float z){
		direction = new Vector3 (x,y,z);
	}
}
