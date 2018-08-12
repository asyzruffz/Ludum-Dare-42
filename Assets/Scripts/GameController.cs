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

	[Header ("End Game")]
	[TextArea]
	public string winText;
	[TextArea]
	public string loseText;

	AudioController aud;
	bool isStarted = false;
	bool isEnded = false;
	
	void Start () {
		aud = GetComponent<AudioController> ();

		endText.text = loseText;
	}

	void Update () {
		if (Input.GetButtonDown("Cancel")) {
			ExitGame ();
		}

		if (!isEnded && limit.IsGameOver ()) {
			isEnded = true;
			fade.FadeToBlack (true);
			if (aud) {
				aud.SetRepeating (false);
				aud.PlayMusicType ("Tada");
			}
		}

		if (isEnded && !fade.IsFading()) {
			endText.text = winText;
			thankText.SetActive (true);
			ui.SetGameOverPanelDisplay (true);
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
