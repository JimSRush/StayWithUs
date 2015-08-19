using UnityEngine;
using System.Collections;

public class Memory : MonoBehaviour {
	
	public AudioSource audio;

	public GameObject playerTarget; //player variable/location

	public bool hasPlayed;
	
	
	// Inititaliase the script and find the player, and set the player as the target
	void Start () {
		//Find the player
		GameObject g = GameObject.FindGameObjectWithTag("Player");
		playerTarget = g;
		audio = GetComponent<AudioSource>();
		
	}
	
	// Check if we're close to the player. 
	void Update () {
		//Debug.DrawLine(playerTarget.position, thisTransform.position, Color.red); //for debugging & finding the player
		
		float distanceToPlayer = Vector3.Distance(playerTarget.transform.position, transform.position); //find the distance to the player each update
		
		if (distanceToPlayer < 5 && hasPlayed == false) {
			PlayAudio();
		} 		
	}


	void PlayAudio() {
		audio.Play ();
		hasPlayed = true;
	}

	void stopAudio() {
		audio.Stop ();

	}
}
