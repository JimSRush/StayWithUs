using UnityEngine;
using System.Collections;

public class PlayerLight : MonoBehaviour {

	public Light light;
	private bool attack;
	private bool speed;
	
	// Update is called once per frame
	void Update () {
		if (speed) {
			light.color = Color.yellow;
			if (light.range < 5f) {
				light.range += 1.0f;
			} else {
				light.range = 0f;
			}
		}
		
		if(attack) {
			light.color = Color.blue;
			if (light.range < 20f) {
				light.range += 1.0f;
			} else {
				attack = false;
				light.range = 0f;
			}
		}
	}

	public void speedOn() {
		speed = true;
	}

	public void speedOff() {
		speed = false;
		light.range = 0f;
	}

	public void attackOn() {
		attack = true;
	}
}
