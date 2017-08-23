using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour {

	public float walkSpeed;
	public float runSpeed;

	public Transform playerTransform;
	public GameObject skeleton;
	public GameObject fireball;
	public ParticleSystem healingFire;

	Rigidbody demonRigidbody;
	Animation anim;
	Transform rightHandTransform;
	Demon_Health dh;

	/* Audio */
	AudioSource[] audios;
	private int demonSpawnSFX = 0;
	private int skSpawnRoarSFX = 1;
	private int dieSFX = 2;

	private int move;


	public bool isAlive;
	public bool playerInRange;
	public bool newMove;
	public bool createFireball;
	public bool canSkip;
	public bool healthLow;

	private float time;	
	private float healTimer;

	/* Spawn skeletons variables */
	Vector3[] skeletonSpawnOffset;
	public bool spawnSkeletons;


	/* The different "states" of the Demon */
	public IDemonState idle;
	public IDemonState pnir;
	public IDemonState pir;
	public IDemonState currentState;


	// Use this for initialization
	void Start () {
		rightHandTransform = GameObject.FindGameObjectWithTag ("BossRightHand").transform;
		demonRigidbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animation> ();
		anim ["Die_Demon"].wrapMode = WrapMode.Once;

		audios = GetComponents<AudioSource> ();
		audios [demonSpawnSFX].Play ();

		dh = GetComponent<Demon_Health> ();

		isAlive = true;
		spawnSkeletons = true;

		skeletonSpawnOffset = new Vector3[4]{
			new Vector3(2f,0,2f),
			new Vector3(-2f,0,-2f),
			new Vector3(-2f,0,2f),
			new Vector3(2f,0,-2f)
		};

		idle = new DemonStateIdle ();
		pnir = new DemonStatePNIR ();
		pir = new DemonStatePIR ();
		currentState = idle;
		move = 0;

		healingFire = GameObject.FindGameObjectWithTag ("BossHealingFire").GetComponent < ParticleSystem> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isAlive) {
			if (healthLow) {
				move = 5;
				newMove = false;
				healthLow = false;
				time = Time.time;
			}
			if (Time.time - time > 3.5) {
				ResetMethodControlBooleans ();
				healingFire.Stop ();
				time = Time.time;
			}
			if (newMove) { // Time to decide next move.
				//if (canSkip && Random.Range (1, 7) == 5) { // 1/6 chance to skip a move.
				//	move = 0;
				//	canSkip = false;
				//} else {
					move = currentState.DecideNextMove ();
					canSkip = true;
				//}
				newMove = false;
				time = Time.time;
			}

			switch (move) {
			case 0:
				Idle ();
				break;
			case 1:	
				SpawnSkeletons ();
				break;			
			case 2:
				ChasePlayer ();
				break;
			case 3:
				MeleeAttack ();
				break;	
			case 4:
				RangeAttack ();
				break;
			case 5:
				Heal ();
				break;
			}
		}
	}

	/* Move number 0 */
	private void Idle(){
		anim.CrossFade ("Idle_Demon");
	}
	/* Move number 1 */
	private void SpawnSkeletons(){
		anim.CrossFade ("Roar_Demon");	
		if (spawnSkeletons) {
			spawnSkeletons = false;
			Invoke ("SpawnSkeletonsHelper", 2f);	
		}
		if (!audios [skSpawnRoarSFX].isPlaying) {
			audios [skSpawnRoarSFX].Play ();
		}
	}
	private void SpawnSkeletonsHelper(){
		Vector3 spawnAt;
		for (int i = 0; i < 4; i++) {
			spawnAt = transform.position + skeletonSpawnOffset[i];
			Instantiate (skeleton, spawnAt, transform.rotation);
			//GameObject sk = 
			//sk.transform.GetComponent<SkeletonController> ().playerTransform = playerTransform;
		}
	}
 	/* Move number 2 */
	private void ChasePlayer(){
		anim.CrossFade ("Run_Demon");
		transform.LookAt(playerTransform);
		Vector3 position = demonRigidbody.position;
		Vector3 movement = transform.forward * runSpeed * Time.deltaTime;
		demonRigidbody.MovePosition(position + movement);
	}
	/* Move number 3 */
	private void MeleeAttack(){
		transform.LookAt(playerTransform);
		anim.CrossFade ("Hit_Demon");
	}
	/* Move number 4 */
	private void RangeAttack(){
		transform.LookAt(playerTransform);
		anim.CrossFade ("Hit_Throw_Demon");
		float animTime = (float) System.Math.Round(anim ["Hit_Throw_Demon"].normalizedTime, 1);

		if (createFireball && animTime == 0.4f) {
			GameObject fb = Instantiate (fireball, rightHandTransform.position, Quaternion.identity);
			Vector3 dir = (playerTransform.position - rightHandTransform.position + new Vector3(0f,1.4f,0f)).normalized;
			fb.GetComponent<FireBallController> ().SetDirection (dir.x, dir.y, dir.z);
			createFireball = false;
		} else if (animTime >= 0.8f) {
			createFireball = true;
		}
	}
	/* Move number 5 */
	public void Heal(){
		anim.CrossFade ("Roar_Demon");
		if (!healingFire.isPlaying) {
			healingFire.Play ();
		}
		if (Time.time - healTimer > 0.5) {
			dh.Heal ();
			healTimer = Time.time;
		}
		if (!audios [skSpawnRoarSFX].isPlaying) {
			audios [skSpawnRoarSFX].Play ();
		}
	}
	/* Move number 10 */
	public void Die(){
		isAlive = false;
		StopAllAudioPlaying ();
		//audios [dieSFX].
		audios [dieSFX].Play ();
		anim.CrossFade ("Die_Demon");

		Destroy (gameObject, 2);
	}

	private void ResetMethodControlBooleans(){
		newMove = true;
		spawnSkeletons = true;	
		createFireball = true;
		dh.isHealing = false;
	}

	private void StopAllAudioPlaying(){
		for (int i = 1; i < audios.Length; i++) {
			if (audios [i].isPlaying) {
				audios [i].Stop ();
			}
		}
	}

	public void Reset(){
		currentState = idle;
		playerTransform = null;
		newMove = false;
		GetComponentInChildren<DemonAggro> ().Reset ();
	}

	public void Restart(){
		if (!audios [demonSpawnSFX].isPlaying) {
			audios [demonSpawnSFX].Play ();
		}
	}
}
