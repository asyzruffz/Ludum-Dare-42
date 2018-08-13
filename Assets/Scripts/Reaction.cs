using UnityEngine;
using TMPro;

public class Reaction : MonoBehaviour {

	public enum SpeechType {
		Epilogue,
		ThingGetsSerious,
		MayStuck,
		Stucked
	}

	public float respondDuration = 2;
	public GameObject speech;
	public TextMeshProUGUI dialog;
	
	private float waitTimer = 0;
	bool speechDisplayed;

	void Start () {
		
	}
	
	void Update () {
		if (speechDisplayed) {
			waitTimer += Time.deltaTime;

			if (waitTimer >= respondDuration) {
				waitTimer = 0;
				speech.SetActive (false);
				speechDisplayed = false;
			}
		}
	}

	public bool IsReacting () {
		return waitTimer > 0;
	}

	public void ReactWith (SpeechType speechType) {
		SetSpeech (speechType);
	}
	
	void SetSpeech (SpeechType speechType) {
		switch (speechType) {
			case SpeechType.Epilogue:
				dialog.text = "What is this narrow space? I don't like it!";
				break;
			case SpeechType.ThingGetsSerious:
				dialog.text = "I have no time, I need to kill faster!";
				break;
			case SpeechType.MayStuck:
				dialog.text = "At this rate I'm gonna be stuck here forever.";
				break;
			case SpeechType.Stucked:
				dialog.text = "Help, I'm stucked!";
				break;
		}

		speechDisplayed = true;
		speech.SetActive (true);
	}
}
