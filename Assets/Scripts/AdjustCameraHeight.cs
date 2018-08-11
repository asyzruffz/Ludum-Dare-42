using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraHeight : MonoBehaviour {

	public Transform target;
	public float heightOffset;
	
	void Start () {
		
	}
	
	void Update () {
		float y = Mathf.Lerp (transform.position.y, target.position.y + heightOffset, Time.deltaTime);
		transform.position = new Vector3 (transform.position.x, y, transform.position.z);
	}
}
