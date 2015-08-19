using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

	public GameObject [] waypoints;
	private int currentWaypoint;
	public float speed;

	// Use this for initialization
	void Start () {
		currentWaypoint = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
		float distanceToNextWaypoint = Vector3.Distance (waypoints [currentWaypoint].transform.position, transform.position); //find the distance between the cam and the waypoint
		moveTowards(waypoints[currentWaypoint]); //move towards the current waypoint
		
		//check to see if we've hit the current waypoint, and if we have, incremennt
		if (distanceToNextWaypoint <1.0f) {
			currentWaypoint++;
			if (currentWaypoint > waypoints.Length-1) {
				currentWaypoint = 0; //back to the beginning
			}
		}

	}

	void moveTowards(GameObject wp) {
		Vector3 direction = wp.transform.position - transform.position; // get the direction to the waypoint
		Vector3 move = direction.normalized * speed * Time.deltaTime;
		transform.position = transform.position + move;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), 0.5f * Time.deltaTime);
	}	
}
