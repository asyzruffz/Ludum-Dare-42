using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float bulletSpeed = 50;
	public float lifetime = 3;

	Rigidbody body;

	void Start () {
		body = GetComponent<Rigidbody> ();
		if (body) {
			body.velocity = transform.forward * bulletSpeed;
		}

		Destroy (gameObject, lifetime);
	}
}
