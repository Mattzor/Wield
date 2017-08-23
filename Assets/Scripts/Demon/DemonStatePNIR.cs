
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonStatePNIR : IDemonState {
	
	/* PNIR = Player Not In Range
	 * Possible moves:
	 * ChasePlayer() move number 2
	 * 
	 */
	int move = 0;
	
	public int DecideNextMove(){
		int i = Random.Range (1, 5);

		if (i == 1) {
			move = 4;
		//} else if (i == 3) {
		// 	move = 1;
		} else {
			move = 2;
		}
		return move;
	}

}