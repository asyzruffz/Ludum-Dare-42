using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLimit : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")) {
			Debug.Log ("You run out of space!");
		} else if (other.CompareTag ("Enemy")) {
			Destroy (other.gameObject);
		}
	}

}
