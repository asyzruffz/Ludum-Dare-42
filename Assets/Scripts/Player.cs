using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Camera cam;
	public Turret character;
	public GameObject enemyPrefab;

	GameController game;

	void Start () {
		game = GetComponent<GameController> ();
		SpawnEnemies ();
	}
	
	void Update () {
		if (!game.IsStarted() || !game.IsEnded ()) {
			return;
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				//Vector3 pos = hit.point;
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

	void SpawnEnemies () {
		int num = 300;
		for (int i = 0; i < num; i++) {
			Invoke ("Pop", i * 0.2f);
		}
	}

	void Pop () {
		Vector2 xz = Random.insideUnitCircle * 4;
		Instantiate (enemyPrefab, new Vector3 (xz.x, character.transform.position.y + 6, xz.y), Quaternion.identity, transform);
	}
}
