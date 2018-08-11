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

	void Start () {
        hasAim = (target != null);
	}
	
	void Update () {
        Facing ();
    }

	public void SetAim (Transform tar) {
		target = tar;
		hasAim = true;
	}

    void Facing () {
        Quaternion targetRot;
        if (hasAim && target != null) {
            targetRot = Quaternion.LookRotation (target.position - gun.position);
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
	}
}
