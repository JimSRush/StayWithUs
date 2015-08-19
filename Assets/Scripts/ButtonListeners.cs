using UnityEngine;
using System.Collections;


//Jim wrote this.
//basic button listener for the main UI for the title screen
public class ButtonListeners : MonoBehaviour {

	private bool showingGuide;
	public GameObject guideImage;

	void Start() {
		guideImage.SetActive (false);
	}

	public void StartGame()
	{
		AutoFade.LoadLevel ("Main", 2, 2, Color.white);
	}
	
	public void Instructions()
	{
		if (showingGuide) {
			guideImage.SetActive (false);
			showingGuide = false;
		} else {
			guideImage.SetActive(true);
			showingGuide = true;
		}
	}

	public void Exit() {
		Application.Quit();
	}

}
