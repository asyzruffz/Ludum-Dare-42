using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour {

	public float fadeTime = 1;
    public bool playOnStart;

	ScreenTransitionImageEffect effect;
	float timer = 0;
	bool startFading = false;
	float startVal = 1;
	float endVal = 0;

	void Awake () {
		effect = GetComponent<ScreenTransitionImageEffect> ();
        startFading = playOnStart;
    }
	
	void Update () {
		if (startFading) {
			timer += Time.deltaTime;
			float t = Mathf.Clamp01 (timer / fadeTime);
			effect.maskValue = Mathf.Lerp (startVal, endVal, t);

			if (t >= 1) {
				startFading = false;
				timer = 0;
			}
		}
	}

	public void FadeToBlack(bool toBlack) {
		float val = toBlack ? 1 : 0;
		startFading = true;
		startVal = effect.maskValue;
		endVal = val;
    }

    public bool IsFading() {
        return startFading;
    }
}
