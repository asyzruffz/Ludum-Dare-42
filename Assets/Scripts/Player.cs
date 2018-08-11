using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Camera cam;
	public Turret character;
	GameController game;

	void Start () {
		game = GetComponent<GameController> ();
	}
	
	void Update () {
		if (!game.IsStarted() || game.IsEnded ()) {
			return;
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				character.SetAim (hit.transform);
				character.Shoot ();
			}
		}

		float dx = Input.GetAxis ("Horizontal");
		float dy = Input.GetAxis ("Vertical");
		Vector3 pos = new Vector3 (dx, 0, dy);
		pos.Normalize ();

		character.GetComponent<Controller3D> ().Move (pos);
	}
}
