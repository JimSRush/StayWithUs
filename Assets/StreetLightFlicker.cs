using UnityEngine;
using System.Collections;

public class StreetLightFlicker : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(Random.value > 0.2f) {
			GetComponent<Light>().enabled = true;
		} else {
			GetComponent<Light>().enabled = false;
		}
	}
}
