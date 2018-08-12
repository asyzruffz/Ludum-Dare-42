using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public float turningSpeed = 5;

	[Space]
    public bool hasAim;
	public Transform target;

	[Space]
	public Transform gun;
	public Transform muzzle;
	public GameObject bulletPrefab;

	Vector3 lastAimPos;
	AudioController aud;

	void Start () {
		aud = GetComponent<AudioController> ();
        hasAim = (target != null);
	}
	
	void Update () {
        Facing ();
    }

	public void SetAim (Transform tar) {
		target = tar;
		hasAim = true;
	}

	public void SetAim (Vector3 tarPos) {
		lastAimPos = tarPos;
		hasAim = true;
	}

	void Facing () {
        Quaternion targetRot;
		if (hasAim && target != null) {
			targetRot = Quaternion.LookRotation (target.position - gun.position);
			gun.rotation = Quaternion.Lerp (gun.rotation, targetRot, turningSpeed * Time.deltaTime);
			lastAimPos = target.position;
		} else if (hasAim) {
			targetRot = Quaternion.LookRotation (lastAimPos - gun.position);
			gun.rotation = Quaternion.Lerp (gun.rotation, targetRot, turningSpeed * Time.deltaTime);
		} else {
            targetRot = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);
            gun.rotation = Quaternion.Lerp (gun.rotation, targetRot, turningSpeed * 0.2f * Time.deltaTime);
        }
    }

    public void RemoveAim () {
        hasAim = false;
    }

	public void Shoot () {
		Instantiate (bulletPrefab, muzzle.position, gun.rotation);
		if (aud) {
			aud.PlaySoundType ("Shoot");
		}
	}
}
