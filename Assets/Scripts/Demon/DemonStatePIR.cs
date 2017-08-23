
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DemonStatePIR : IDemonState{
	
	/* PIR = Player In Range 
	 * Possible Moves from this state is:
	 * MeleeAttack() move number 3
	 * SpawnSkeletons() move number 1
	 * 
	 */

	public int DecideNextMove(){
		
		int i = Random.Range(1,7);
		if( i == 6){
			return 1;	
		}
		return 3;
	}

}