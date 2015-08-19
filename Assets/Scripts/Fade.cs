using UnityEngine;
using System.Collections;

/*
 * Fades scene in
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 * 
 */
public class Fade : MonoBehaviour {

	//canvas changing
	public CanvasGroup cg;
	public float fadeSpeed = 2.0f;

	//This script is supposedly attached to a canvas group
	//on awake, it calls the coroutine to fade in
	//alpha is changing, but the canvas isn't.
	void Awake() {
		cg = GetComponent<CanvasGroup> ();//assignment
		cg.alpha = 1f;
	}

	// Use this for initialization
	void Start () {
		FadeIn();
		Debug.Log ("Start");
	}


	public void FadeIn() {
		StartCoroutine (DoFadeIn());
		Debug.Log ("FadeIn");
	}

	IEnumerator DoFadeIn () {
		Debug.Log ("DoFadeIn");
		Debug.Log (cg.alpha);
		while (cg.alpha >=0) {
			cg.alpha -=(Time.deltaTime / 8); //increase the alpha
			Debug.Log (cg.alpha);
			yield return null; // over multiple frames
		}
		yield return null;
	}

	//IEnumerator DoFadeOut() {
	//	CanvasGroup cg = GetComponent<CanvasGroup> ();
	//}
}
