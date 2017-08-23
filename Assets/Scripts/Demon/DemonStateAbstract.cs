
using System.Collections;
using System.Collections.Generic;

public abstract class DemonStateAbstract : IDemonState{

	abstract public int DecideNextMove();
	
	/*public void Die(){
		anim.CrossFade ("Die_Demon");
		isAlive = false;
		Destroy (gameObject, 3);		
	}*/
	
	

}