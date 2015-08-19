using UnityEngine;
using System.Collections;

/*
 * Disables all free radicals from following the player for a given time.
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 * 
 */ 
public class DisableFollowPillScript : MonoBehaviour {

	//reference to the character (as the thing attracting the free radicals)
	public RadicalAttractor attractor;
	//time given as length of disable
	public int timeToDisable;
	
	// Update is called once per frame
	void Update () {
		//rotates the object itself in game space
		transform.Rotate(new Vector3 (45, 0, 45) * Time.deltaTime);
	}

	//removes the object when hit by charater (collected)
	void OnCollisionEnter(Collision col) {
		attractor.temporarilyDisable(5);
		this.gameObject.SetActive(false);
	}
}
