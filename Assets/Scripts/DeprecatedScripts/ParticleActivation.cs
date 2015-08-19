using UnityEngine;
using System.Collections;

/*
 * 
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 */ 
public class ParticleActivation : MonoBehaviour {

		
	public GameObject playerTarget; //player variable/location
	public GameObject particleThingy;
	
	// Inititaliase the script and find the player, and set the player as the target
	void Start () {
		//Find the player
		GameObject g = GameObject.FindGameObjectWithTag("Player");
		//Debug.Log ("Found the player");
		playerTarget = g;
		gameObject.GetComponent<ParticleSystem>().enableEmission = false;
	}
	
	// Check if we're close to the player. 
	void Update () {
		float distanceToPlayer = Vector3.Distance(playerTarget.transform.position, transform.position); //find the distance to the player each update
		//Debug.Log("Distance to player is" + distanceToPlayer);
		if (distanceToPlayer <1) {
			Debug.Log("Distance to player is" + distanceToPlayer);
			//gameObject.GetComponent<ParticleSystem>().Play();
		}
	}
}