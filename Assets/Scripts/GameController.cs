using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public FadeScreen fade;
	public FallLimit limit;
	public UIDisplayController ui;

	bool isStarted = false;
	bool isEnded = false;

	void Start () {
		
	}
	
	void Update () {
		if (!isEnded && limit.IsGameOver ()) {
			isEnded = true;
			fade.FadeToBlack (true);
		}

		if (isEnded && !fade.IsFading()) {
			ui.SetGameOverPanelDisplay (true);
		}
	}

	public void StartGame () {
		isStarted = true;
	}

	public void RestartGame () {
		SceneManager.LoadScene (0);
	}

	public bool IsStarted () {
		return isStarted;
	}

	public bool IsEnded () {
		return isEnded;
	}
}
