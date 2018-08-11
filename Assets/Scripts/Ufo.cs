using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour {

	Health health;
	Flight flight;

	void Start () {
		health = GetComponent<Health> ();
		flight = GetComponent<Flight> ();
	}
	
	void Update () {
		
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.CompareTag ("Bullet")) {
			health.ApplyDamage (25);
		}
	}

	public void StopFlying () {
		flight.SetFlightEnabled (false);
	}
}
