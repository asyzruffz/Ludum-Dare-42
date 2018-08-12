using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : Singleton<GameController> {

	public FadeScreen fade;
	public FallLimit limit;
	public UIDisplayController ui;
	public TextMeshProUGUI endText;
	public GameObject thankText;

	[Header ("Timing")]
	public float initialTime = 5;
	public float subsequentTime = 2;

	[Header ("End Game")]
	[TextArea]
	public string winText;
	[TextArea]
	public string loseText;

	AudioController aud;
	bool isStarted = false;
	bool isEnded = false;
	bool isStucked = false;
	bool beginnerChance = true;
	float timer;
	
	void Start () {
		aud = GetComponent<AudioController> ();

		timer = initialTime;
	}

	void Update () {
		if (Input.GetButtonDown("Cancel")) {
			ExitGame ();
		}

		// Game ended, starts withdrawing the curtain
		if (!isEnded && (limit.IsGameOver () || isStucked)) {
			isEnded = true;
			fade.FadeToBlack (true);
			if (aud) {
				aud.SetRepeating (false);
				if (isStucked) {
					aud.PlayMusicType ("Lose");
					endText.text = loseText;
				} else {
					aud.PlayMusicType ("Tada");
					endText.text = winText;
					thankText.SetActive (true);
				}
			}
		}

		// The curtain finished closing after the game ended
		if (isEnded && !fade.IsFading()) {
			ui.SetGameOverPanelDisplay (true);
		}

		// Countdown time
		if (timer <= 0) {
			if (beginnerChance) {
				// chance time given at the start of the game
				timer += subsequentTime;
				beginnerChance = false;
				Debug.Log ("Chance given");
			} else {
				// You lose
				isStucked = true;
			}
		} else if (timer <= 1f) {
			if (!beginnerChance) {
				// Despair
			}
		}

		if (isStarted) {
			timer -= Time.deltaTime;
		}
	}

	public void GainPlayTime () {
		if (!beginnerChance) {
			timer = subsequentTime;
			// Say something..
		}
	}

	public void StartGame () {
		isStarted = true;
	}

	public void RestartGame () {
		SceneManager.LoadScene (0);
	}

	public void ExitGame () {
		Application.Quit ();
	}

	public bool IsStarted () {
		return isStarted;
	}

	public bool IsEnded () {
		return isEnded;
	}
}
