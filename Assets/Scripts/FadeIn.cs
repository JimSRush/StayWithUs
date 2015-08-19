using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {
	
	public CanvasGroup cg;//what we're changing
	
	public void Fade() {
		StartCoroutine (FadeMe());
	}
	IEnumerator FadeMe() {
		CanvasGroup c = GetComponent<CanvasGroup> ();
		while (c.alpha>0) {
			c.alpha -= Time.deltaTime /2;
			yield return null;
		}
		c.interactable = false;
		yield return null;
		LoadLevel ();
		
	}
	
	public void LoadLevel() {
		Application.LoadLevel ("Demo level");
		Debug.Log("Button was pressed");
	}
	
}
