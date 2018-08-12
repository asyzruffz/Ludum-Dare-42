using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIState {

	public Ufo ufo;

	Health health;

	void Start () {
		health = ufo.GetComponent<Health> ();
	}

	public override void CheckForStateExit () {
		if (health.IsDead ()) {
			machine.SetCurrentState ("Dead");
		} else if (ufo.DonePatrolling ()) {
			machine.SetCurrentState ("Attack");
		}
	}
}
