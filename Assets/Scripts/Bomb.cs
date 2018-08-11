using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public float explodeTimer = 2;
	public float explosionRadius = 2;
	public float explosionPower = 10;

	void Start () {
		Invoke ("Explode", explodeTimer);
		Destroy (gameObject, explodeTimer + 0.2f);
	}
	
	public void Explode () {
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, explosionRadius);
		foreach (Collider hit in colliders) {
			Rigidbody body = hit.GetComponent<Rigidbody> ();

			if (body) {
				body.AddExplosionForce (explosionPower, explosionPos, explosionRadius, 3.0f);
			}
		}
	}
}
