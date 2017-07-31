using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject skeletonEnemy;
	public GameObject monsterEnemy;

	public int skeletonMax;

	public float startWait;
	public float spawnInterval;

	private int spawnedSkeletonTot = 0;
	private int numOfSkeleton;
	private float spawnWait = 0.5f;

	// Use this for initialization
	void Start () {
		StartCoroutine (RunGame ());
	}

	IEnumerator RunGame(){
		yield return new WaitForSeconds (startWait);

		while (true) {
			for (int i = numOfSkeleton; i < skeletonMax; i++) {
			
				Vector3 spawnAt = new Vector3 (
					            Random.Range(0,10),
					            0, 
					            Random.Range(-5,5));
				GameObject skeleton = Instantiate (skeletonEnemy, spawnAt, Quaternion.identity);
				skeleton.GetComponent<SkelletonHealth>().SetGamecontroller (gameObject);
				numOfSkeleton++;
				spawnedSkeletonTot++;
				yield return new WaitForSeconds(spawnWait);
			}
			if (spawnedSkeletonTot > 15) {
				Vector3 spawnAt = new Vector3 (
					Random.Range(0,10),
					0, 
					Random.Range(-5,5));
				GameObject monster = Instantiate (monsterEnemy, spawnAt, Quaternion.identity);

			}
			yield return new WaitForSeconds (spawnInterval);

		}
	}

	public void SkeletonDecrease(){
		numOfSkeleton--;
	}

}
