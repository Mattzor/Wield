using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


	public GameObject player;
	private PlayerHealth playerHealth;

	public GameObject skeletonEnemy;
	public GameObject monsterEnemy;
	public GameObject demonBoss;
	public GameObject bag;

	public GameObject mainCamera;

	private GameObject guiStartText;
	private GameObject guiGameOverText;
	private GameObject guiGameFinishedText;



	public float startWait;
	public int points = 0;
	public int pointsNeededForBoss;


	public bool running = false;
	private bool restart = false;

	/* Spawn Area Limits   {{{xmin,xmax},{zmin,zmax}}, ... }z         */
	private int[,,] enemyAreas = new int[4,2,2] {{{0,10},{-5,5}},
							                     {{-8,-30},{-1,13}},
		                                         {{-20,15},{11,23}},
		                                         {{13,25},{-20,8}}
	                                            };
	private int[,,] bagAreas = new int[2,2,2] {{{-25,-27},{17,20}},
		                                       {{26,30},{-17,-20}}};
			                                        


	/* Spawn Variables */
	public float spawnInterval;
	private Vector3 spawnAt;
	private int spawnArea;
	public bool spawnedBoss;


	/* Player Start Variables */
	Vector3 playerStartPos = new Vector3(0f,0f,0f);

	/* Boss Variables */
	Vector3 bossStartPos = new Vector3(0f,0f,-22f);

	/* Skeleton Variables */
	private GameObject[] skeletons;
	private int skeletonMax = 15;
	private int spawnedSkeletonTot;
	public int numOfSkeleton;
	private float skeletonSpawnWait = 0.5f;

	/* Monster Variables*/
	private GameObject[] monsters;
	private int monsterMax = 4;
	private int spawnedMonsterTot = 0;
	private int numOfMonster;
	private float monsterTimer;

	/* Bag Variables*/
	private GameObject[] bags; 
	private int bagsMax = 5;
	private float bagSpawnInterval = 0;
	private float bagTimer;


	private AudioSource backgroundMusic;


	// Use this for initialization
	void Start () {

		demonBoss.SetActive (false);
		backgroundMusic = GetComponent<AudioSource> ();

		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();

		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		guiStartText = GameObject.FindGameObjectWithTag ("GUIStartText");
		guiGameOverText = GameObject.FindGameObjectWithTag ("GUIGameOverText");
		guiGameFinishedText = GameObject.FindGameObjectWithTag ("GUIGameFinishedText");

		guiGameOverText.SetActive (false);
		guiGameFinishedText.SetActive (false);
	}


	void Update(){
		// Game start
		if( !running && Input.GetKeyUp(KeyCode.Return)){			
			guiStartText.SetActive (false);
			InitGame ();
			StartCoroutine ("RunGameNormalMode", 0f);
		}
		// Player died
		if (playerHealth.health <= 0) {			
			StopGame ();
			guiGameOverText.SetActive (true);
			restart = true;
		}
		// Restart game
		if (restart && Input.GetKeyUp (KeyCode.Return)) {
			RestartGame ();
			StartCoroutine ("RunGameNormalMode", 0f);
		}
		// Game finished
		if (running && spawnedBoss && demonBoss == null) {
			StopGame ();
			guiGameFinishedText.SetActive (true);	

		}
			
	}

	private void InitGame(){
		running = true;
		player.GetComponent<PlayerController> ().enabled = true;
		mainCamera.GetComponent<CameraController> ().enabled = true;
		backgroundMusic.loop = true;
		backgroundMusic.Play ();	
		spawnedSkeletonTot = 0;
		skeletons = new GameObject[skeletonMax];
		monsters = new GameObject[monsterMax];
		bags = new GameObject[bagsMax];
		bagTimer = Time.time;	
	}

	private void RestartGame(){
		restart = false;
		spawnedBoss = false;
		points = 0;
		spawnedMonsterTot = 0;
		spawnedSkeletonTot = 0;
		guiGameOverText.SetActive (false);
		ResetPlayer ();
		ResetBoss ();
		DestroyEnemies ();
		InitGame ();
	}

	private void StopGame(){
		running = false;
		spawnedBoss = false;
		player.GetComponent<PlayerController> ().enabled = false;
		mainCamera.GetComponent<CameraController> ().enabled = false;
	}



	IEnumerator RunGameNormalMode(){		
		yield return new WaitForSeconds (startWait);
		while (running) {
			// Check if the player have killed enough enemies to "spawn" the boss
			if (points >= pointsNeededForBoss) {
				spawnedBoss = true;
				demonBoss.SetActive (true);
				demonBoss.GetComponent<DemonController> ().Restart ();
				demonBoss.GetComponent<Demon_Health> ().Restart ();
				yield break;
			}
			// Spawn Skeletons
			for (int i = 0; i < skeletonMax; i++) {			
				if (skeletons [i] == null) {
					if(spawnedSkeletonTot >= skeletonMax){
						points += 1;
					}
					SpawnSkeleton (i);
					yield return new WaitForSeconds (skeletonSpawnWait);
				}
			}
			// Spawn Monster
			if (points > 10) {
				for (int i = 0; i < monsterMax && Time.time - monsterTimer > 15; i++) {
					if (monsters [i] == null) {
						if (spawnedMonsterTot >= monsterMax) {
							points += 3;
						}
						SpawnMonster (i);
						monsterTimer = Time.time;
					}
				}
			}
			// Spawn new health bags
			if ( Time.time - bagTimer >= bagSpawnInterval) {
				SpawnBags ();
				bagTimer = Time.time;
			}

			yield return new WaitForSeconds (spawnInterval);

		}
		DestroyEnemies();
	}


	private void SpawnSkeleton(int i){
		spawnArea = Random.Range (1, 4);
		spawnAt = new Vector3 (
			Random.Range(enemyAreas[spawnArea,0,0],enemyAreas[spawnArea,0,1]),
			0, 
			Random.Range(enemyAreas[spawnArea,1,0],enemyAreas[spawnArea,1,1]));

 		skeletons[i] = Instantiate (skeletonEnemy, spawnAt, Quaternion.identity);
		spawnedSkeletonTot++;				
	}


	private void SpawnMonster(int i){
		spawnAt = new Vector3 (
			Random.Range(0,10),
			0, 
			Random.Range(-5,5));
		monsters[i] = Instantiate (monsterEnemy, spawnAt, Quaternion.identity);

		numOfMonster++;
		spawnedMonsterTot++;
	}

	private void SpawnBags(){
		for (int i = 0; i < bagsMax; i++) {			
			if(bags[i] == null){
				int spawnArea = Random.Range (0, 2);
				spawnAt = new Vector3 (
					Random.Range(bagAreas[spawnArea,0,0],bagAreas[spawnArea,0,1]),
					0,
					Random.Range(bagAreas[spawnArea,1,0],bagAreas[spawnArea,1,1]));
				
				bags[i] = Instantiate (bag, spawnAt, Quaternion.identity);					
			}
		}
	}

	private void SpawnDemonBoss(){
		GameObject boss = Instantiate (demonBoss, bossStartPos, Quaternion.identity);
	}

	private void ResetPlayer(){
		player.GetComponent<PlayerController> ().isAlive = true;
		playerHealth.Reset ();
		player.transform.position = playerStartPos;

	}

	private void ResetBoss(){
		demonBoss.transform.position = bossStartPos;
		demonBoss.GetComponent<Demon_Health>().Reset ();
		demonBoss.GetComponent<DemonController> ().Reset ();
		demonBoss.SetActive (false);

	}

	private void DestroyEnemies(){
		GameObject[] sks = GameObject.FindGameObjectsWithTag ("SkeletonRoot");
		for (int i = 0; i < sks.Length; i++) {						
			Destroy (sks[i]);
		}
		GameObject[] ms = GameObject.FindGameObjectsWithTag ("MonsterEnemy");
		for (int i = 0; i < ms.Length; i++) {						
			Destroy (ms[i]);
		}
	}
}
