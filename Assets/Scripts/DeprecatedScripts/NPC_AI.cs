using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NPC_AI : MonoBehaviour {
	//This script is attached to a NPC, and controls basic AI (find the player when they're within a certain distance)

	public GameObject playerTarget; //player variable/location
	public Text alertBox; //this is the alert box in the UI
	private Transform thisTransform; // location of this NPC char
	public GameObject [] waypoints; //list of way points
	private int currentWaypoint = 1; //the first waypoint the NPC starts at
	private float speed = 1f;
	public Renderer r;//renderer object;
	public GameObject eagle; //child, that we're going to call methods on. This is the actual model. 
	
	void Awake() {
		thisTransform = transform;
	}


	// Inititaliase the script and find the player, and set the player as the target
	void Start () {
		//Find the player
		GameObject g = GameObject.FindGameObjectWithTag("Player");
		playerTarget = g;
		alertBox.text = "Hidden";
	}
	
	// Check if we're close to the player. If we are, follow! If not, patrol.
	void Update () {
		//Debug.DrawLine(playerTarget.position, thisTransform.position, Color.red); //for debugging & finding the player

		float distanceToPlayer = Vector3.Distance(playerTarget.transform.position, thisTransform.position); //find the distance to the player each update
	
		if (distanceToPlayer < 5) {
			speed = 4f; //faster, faster!
			alertBox.text = "You've been spotted, run!";
			playerFollow ();
		} else {
			speed = 1f;
			alertBox.text = "Hidden";//back to normal
			patrol ();
		}
	}


	//Factoring out here, in case I need to add sound/extraness
	void playerFollow() {

		moveTowards (playerTarget);
	}


	//This is the basic patrol method
	void patrol() {
	
		moveTowards (waypoints [currentWaypoint]); //move towards the currently selected waypoint

		//checks distance between the NPC position and the targeted waypoint 
		float dist = Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position);

		if (dist < 1) { //if the distance to the next waypoint is less than 1
			currentWaypoint++; //target the next one
			if (currentWaypoint > waypoints.Length-1) { //if we've come to the end of the waypoints
				currentWaypoint = 0; //back to the beginning	
			}
		} 
	}


	//This is a helper method, where it is passed a gameobject for the NPC to move towards. Can be called by patrol, or when the player gets too close.
	void moveTowards(GameObject p) {
		//Debug.Log ("Moving towards player");
		Vector3 dir = p.transform.position - thisTransform.position; //Get the vector pointing towards the object
		Vector3 move = dir.normalized * speed * Time.deltaTime; 	//smooth it out/normalize vector to 1
		thisTransform.position = thisTransform.position + move; //makes the position move in the new direction, i.e the thing the NPC is targeting
		thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);//smoothes the rotation towards the object
	}
}

