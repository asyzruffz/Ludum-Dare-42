using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject enemyPrefab;
	public int numAtStart = 8;

	GameController game;
	Player player;

	void Start () {
		game = GetComponent<GameController> ();
		player = GetComponent<Player> ();
	}

	void Update () {
		if (!game.IsStarted () || game.IsEnded ()) {
			return;
		}
	}

	public void SpawnEnemies () {
		for (int i = 0; i < numAtStart; i++) {
			Invoke ("Spawn", i * 0.6f);
		}
	}

	public void Spawn () {
		Vector2 xz = Random.insideUnitCircle * 4;
		Instantiate (enemyPrefab, new Vector3 (xz.x, player.character.transform.position.y + 6, xz.y), Quaternion.identity, transform);
	}
}
