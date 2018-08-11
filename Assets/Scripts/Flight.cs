using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour {

    public float movementSpeed = 1;
    public float turningSpeed = 5;
    public float brakingDistance = 0.5f;
    public bool faceForwardMovement = true;
    public FlightStabilization stabilization;

    Vector3 pos;
    Vector3 diff;
    float angle = 0;
    Vector3 moveTarget, faceTarget;
    bool hasAim;
    bool straightOriented;
	Rigidbody body;
	bool flightEnabled = false;

    void Start () {
		body = GetComponent<Rigidbody> ();
		SetFlightEnabled (true);
		MoveTo (pos);
	}
	
	void Update () {
		
	}

    void FixedUpdate () {
		if (flightEnabled) {
			Move ();
		} else {
			pos = transform.position;
		}
    }

    void LateUpdate () {
		if (flightEnabled) {
			Facing ();
			StabilizeFloat ();
		}
    }

    void Move () {
        diff = moveTarget - pos;
        if (diff.magnitude < brakingDistance) {
            pos = Vector3.Lerp (pos, moveTarget, Time.fixedDeltaTime);
            straightOriented = true;
        } else {
            pos += movementSpeed * diff.normalized * Time.fixedDeltaTime;
            straightOriented = !faceForwardMovement;
        }
    }

    void Facing () {
        Quaternion targetRot;
        if (hasAim) {
            targetRot = Quaternion.LookRotation (faceTarget - pos);
            transform.rotation = Quaternion.Lerp (transform.rotation, targetRot, turningSpeed * Time.deltaTime);
        } else if (straightOriented) {
            targetRot = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp (transform.rotation, targetRot, turningSpeed * 0.5f * Time.deltaTime);
        } else if (faceForwardMovement) {
			if (diff.sqrMagnitude > 0) {
				targetRot = Quaternion.LookRotation (diff);
				transform.rotation = Quaternion.Lerp (transform.rotation, targetRot, turningSpeed * Time.deltaTime);
			}
        }
    }

    public void MoveTo (Vector3 dest) {
        moveTarget = dest;
    }

    public void FacingTowards (Vector3 aim) {
        faceTarget = aim;
        hasAim = true;
    }

    public void RemoveAim () {
        hasAim = false;
    }

    void StabilizeFloat () {
        Vector3 stableDir = new Vector3 (Mathf.Sin (angle * stabilization.velOffset.x + 0.2f), 
                                         Mathf.Sin (angle * stabilization.velOffset.y), 
                                         Mathf.Sin (angle * stabilization.velOffset.z + 1.5f));
        transform.position = pos + stableDir * stabilization.range * 0.5f;
        angle += stabilization.speed * Time.deltaTime;
    }

	public Vector3 GetPos () {
		return pos;
	}

	public void SetFlightEnabled (bool enabled) {
		if (enabled && !flightEnabled) {
			pos = transform.position;
			MoveTo (pos);
		}

		flightEnabled = enabled;
		if (body) {
			body.useGravity = !enabled;
			body.isKinematic = enabled;
		}
	}
}

[System.Serializable]
public struct FlightStabilization {
    public float range;
    public float speed;
    public Vector3 velOffset;
}