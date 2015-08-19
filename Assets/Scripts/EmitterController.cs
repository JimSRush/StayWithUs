using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *  Controls the electrical charge emitters collected by the character
 * 
 *  Authors: Theodore Cruickshank, Jim Rush
 */
public class EmitterController : MonoBehaviour {

	//total number of charges available in the cluster
	//public static int totalEmitters = 5;
	//references to the emitters
	public List<GameObject> emitters = new List<GameObject>();
	//to test which emitters are already connected
	private List<bool> connectedEmitters = new List<bool>();
	//reference to the script of the acceptors
	public AcceptorController acceptors;
	//the current emitter
	private int currentEmitter;
	//reference to the player
	public GameObject player;
	//reference to the camera
	//used to zoom in when the charater gets nearer to the current emitter
	public GameObject camera;

	// Use this for initialization
	void Start () {
		foreach(GameObject g in emitters) {
			connectedEmitters.Add(false);
		}
		setCurrentEmitter(1);
	}

	/*
	 * called every frame and controls camera distance to character based on their proximity to the emitter.
	 * 
	 * NOTE: it currently zooms in at a distance, and wont zoom out until the character has connected
	 * 			maybe make the zoom amount a function of the distance so that it changes if they swim away.
	 */ 
	void Update() {
		float distToEmitter = Vector3.Distance(player.transform.position, emitters[currentEmitter-1].transform.position);
		float distToCamera = Vector3.Distance(player.transform.position, camera.transform.position);

		if (distToEmitter < 20.0f && distToCamera > 5.0f) {
			//Debug.Log ("dist to cam " + distToCamera);
			camera.transform.position = Vector3.MoveTowards (camera.transform.position, player.transform.position, 5.0f * Time.deltaTime);
		}
	}

	/*
	 * Sets the current emitter as the number given, activates it, and deactivates
	 * all emitters that are not connected or current
	 */
	public void setCurrentEmitter(int current) {
		currentEmitter = current;
		//Debug.Log ("Setting " + current + " as current emitter");
		for (int i = 0; i < emitters.Count; i++) {
			//turn off all but current and connected emitters
			if ((i + 1) != currentEmitter && !connectedEmitters[i]) {
				emitters[i].SetActive(false);
			} else if((i + 1) == currentEmitter) {
				emitters[i].SetActive(true);
				connectedEmitters[i] = true;
			}
		}
	}

	/*
	 * returns the current emitter
	 */
	public int getCurrentEmitter() {
		return currentEmitter;
	}
}
