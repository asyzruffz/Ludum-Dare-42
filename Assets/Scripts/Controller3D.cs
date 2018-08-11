using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller3D : MonoBehaviour {

	public float movementSpeed = 1;

	Rigidbody body;

	void Start () {
		body = GetComponent<Rigidbody> ();
	}

	public void Move (Vector3 deltaPos) {
		Vector3 velocity = deltaPos * movementSpeed * 10 * Time.deltaTime;
		body.velocity = new Vector3 (velocity.x, body.velocity.y, velocity.z);
		transform.rotation = Quaternion.Euler (Vector3.up * transform.rotation.eulerAngles.y);
	}

}
