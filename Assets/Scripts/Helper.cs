using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {

	public static Transform FindInChildren(GameObject gameObject, string name){
		foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
			if (t.name == name) {
				return t;
			}
		}
		return null;
	}

}
