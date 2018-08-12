using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour {

	public Camera cam;

	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
	}

    // Let the rotation to always face the main camera
    void Update () {
        transform.LookAt (transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
