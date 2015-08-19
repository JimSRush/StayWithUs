using UnityEngine;
using System.Collections;

/*
 * Script to give the signal attractor lights some functionality
 * Script added to each light
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 */ 
public class SignalLightController : MonoBehaviour {

	//a reference to this light
	public Light thisLight;
	//time it takes to blink
	public int blinkTimer = 0;
	//if light is currently blinking
	private bool isBlinking = false;
	
	// Update is called once per frame
	// blinks light if its meant to be blinking
	void Update () {
		if (isBlinking) {
			blinkTimer++;
			 
			if (blinkTimer < 50) {
				thisLight.enabled = true;
			} else {
				thisLight.enabled = false;
			}

			if (blinkTimer >= 100) {
				blinkTimer = 0;
			}
		}
	}

	//sets size of light and sets it to blink
	public void blink() {
		thisLight.range = 6;
		isBlinking = true;
	}

	//disables light
	public void switchOff() {
		thisLight.enabled = false;
		isBlinking = false;
	}

	//makes light solid on
	public void switchOn() {
		thisLight.enabled = true;
		thisLight.range = 3;
		isBlinking = false;
	}

	public void setColor(Color col) {
		thisLight.color = col;
	}
}
