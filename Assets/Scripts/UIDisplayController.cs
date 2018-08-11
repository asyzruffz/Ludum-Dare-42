using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplayController : MonoBehaviour {

	public GameObject menuPanel;
	public GameObject gameOverPanel;
	
	public void SetMenuPanelDisplay (bool enabled) {
		menuPanel.SetActive (enabled);
	}

	public void SetGameOverPanelDisplay (bool enabled) {
		gameOverPanel.SetActive (enabled);
	}
}
