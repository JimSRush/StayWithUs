using UnityEngine;
using System.Collections;

/*
 * Boosts the speed of the player
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 */ 
public class SpeedBoostPillScript : MonoBehaviour {

	//reference to the character
	public Swim character;
	//time to boost
	public int boostTime;
	
	// Update is called once per frame
	void Update () {
		//rotates the object that symbolises the speed boost pick up
		transform.Rotate(new Vector3 (45, 0, 45) * Time.deltaTime);
	}

	/*
	 * gives the character a speed boost when the pick up representing the speed up is
	 * collided with (or picked up in game
	 * 
	 * NOTE: this will activate if hit by a falling neuron
	 */ 
	void OnCollisionEnter(Collision col) {
		character.boost(boostTime);
		this.gameObject.SetActive(false);
	}
}
