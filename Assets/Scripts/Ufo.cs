using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour {

	public GameObject bombPrefab;
	public Transform hatch;

	Health health;
	Flight flight;
	AudioController aud;

	void Start () {
		health = GetComponent<Health> ();
		flight = GetComponent<Flight> ();
		aud = GetComponent<AudioController> ();
	}
	
	void OnCollisionEnter (Collision other) {
		if (other.gameObject.CompareTag ("Bullet")) {
			health.ApplyDamage (25);
		}
	}

	public void StopFlying () {
		flight.SetFlightEnabled (false);
	}

	public void SpawnAnother () {
		EnemyController eCon = transform.parent.GetComponent<EnemyController> ();
		if (eCon) {
			eCon.Spawn ();
		}
	}
	
	public void DropBomb () {
		Instantiate (bombPrefab, hatch.position, Quaternion.Euler(new Vector3(20, 0, 12)));
		if (aud) {
			aud.PlaySoundType ("BombDrop");
		}
	}

	////////// --- AI --- //////////

	[Header ("AI")]
	public float patrolDelay = 2;
	public float patrolRound = 2;
	public float shotCooldown = 2;

	float timer = 0;
	int patrolCounter = 0;
	bool doneShot = false;

	public void ResetTimer() {
		timer = 0;
	}

	public void ResetCounter () {
		patrolCounter = 0;
	}

	public void ResetShot () {
		doneShot = false;
	}

	public void RandomSkipAttack () {
		float rand = Random.value;
		if (rand < 0.6f) {
			doneShot = true;
		}
	}

	public void PlayDeathSound () {
		if (aud) {
			aud.PlaySoundType ("Dead");
		}
	}

	public void PatrolUpdate () {
		if (!GameController.Instance.IsStarted () || GameController.Instance.IsEnded ()) {
			return;
		}

		if (timer >= patrolDelay) {
			Vector2 xz = Random.insideUnitCircle * 4;
			flight.MoveTo (new Vector3 (xz.x, flight.GetPos ().y, xz.y));
			patrolCounter++;
			ResetTimer ();
		}

		timer += Time.deltaTime;
	}

	public void AttackUpdate () {
		if (!GameController.Instance.IsStarted () || GameController.Instance.IsEnded ()) {
			return;
		}

		if (timer >= shotCooldown && !doneShot) {
			DropBomb ();
			doneShot = true;
		}

		timer += Time.deltaTime;
	}

	public bool DonePatrolling () {
		return patrolCounter > patrolRound;
	}

	public bool DoneShooting () {
		return doneShot;
	}
}
