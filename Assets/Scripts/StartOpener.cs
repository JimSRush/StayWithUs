using UnityEngine;
using System.Collections;

/*
 * Gives the fade function for the button on the start screen.
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 */
public class StartOpener : MonoBehaviour {
	/*
	 * Fades scene into "Intro" scene
	 */ 
	public void fade() {
		AutoFade.LoadLevel("Intro", 3, 1, Color.black);
	}
}
