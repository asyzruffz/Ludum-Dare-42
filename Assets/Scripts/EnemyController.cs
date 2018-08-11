using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject enemyPrefab;

	GameController game;
	Player player;

	void Start () {
		game = GetComponent<GameController> ();
		player = GetComponent<Player> ();
		SpawnEnemies ();
	}

	void Update () {
		if (!game.IsStarted () || game.IsEnded ()) {
			return;
		}
	}

	void SpawnEnemies () {
		int num = 8;
		for (int i = 0; i < num; i++) {
			Invoke ("Spawn", i * 0.2f);
		}
	}

	public void Spawn () {
		Vector2 xz = Random.insideUnitCircle * 4;
		Instantiate (enemyPrefab, new Vector3 (xz.x, player.character.transform.position.y + 6, xz.y), Quaternion.identity, transform);
	}
}
