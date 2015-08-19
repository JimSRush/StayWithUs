using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Increases the time left until the neuron cluster dies
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 */
public class IncreaseTimerPillScript : MonoBehaviour {

	//reference to the UI for the timer
	public UIUpdate ui;
	//amount of time to add to timer
	public int timeToAdd;

	// Update is called once per frame
	// rotates the object representing the timer increase pick up in game
	void Update () {
		transform.Rotate(new Vector3 (45, 0, 45) * Time.deltaTime);
	}

	/*
	 * adds time to the time left for neuron cluster to die
	 * upon this object being collided with.
	 * 
	 * NOTE: at the moment it is the character that collides with it, but if 
	 * a falling neuron were to hit it, it would activate
	 */ 
	void OnCollisionEnter(Collision col) {
		ui.addToTimer(timeToAdd);
		this.gameObject.SetActive(false);
	}
}
