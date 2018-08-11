using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLimit : MonoBehaviour {

	bool gameEnd = false;

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			Debug.Log ("You run out of space!");
			gameEnd = true;
		} else if (other.gameObject.CompareTag ("Enemy")) {
			Destroy (other.gameObject);
		}
	}

	public bool IsGameOver () {
		return gameEnd;
	}
}
