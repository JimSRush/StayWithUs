using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public List<UIUpdate> clusterInformations = new List<UIUpdate>();
	public Swim character;
	private int currentCluster = 1;
	//public MaterialBlend blender;
	public List<Light> killLights = new List<Light>();
	private int killLight;
	private int currentMemory = 0;
	private bool kill;
	private bool beckoning;
	private bool warned;
	public MemoryController memController; //script reference

	void Start() {
		startNextTimer ();
	}

	// Update is called once per frame
	void Update () {
		if (getTimer() <= 0f) {
			AutoFade.LoadLevel("Main", 2, 2, Color.black);
		}

		if (kill) {
			if (killLights[killLight-1].range < 50f) {
				killLights[killLight-1].range += 1.0f;
				//Debug.Log ("killing light " + (killLight-1));
			} else {
				//killLights[killLight].enabled = false;
				killLights[killLight-1].color = Color.green;
				kill = false;
				beckoning = true;
			}
		} if (beckoning) {
			//Debug.Log ("beconning light " + (killLight-1));
			checkPort();
			if(killLights[killLight-1].range < 5f) {
				killLights[killLight-1].range += 1.0f;
			} else {
				killLights[killLight-1].range = 0f;
			}
		}

		if (!warned && getTimer () < 20.0f) {
			GetComponent<AudioController>().playSound("Hospital1");
			warned = true;
		}
	}

	public void signifyClusterChange() {
		kill = true;
		killLight = currentCluster;
		currentCluster++;
		//memController.StartMemory(currentMemory);
		//currentMemory++; //move onto the next memory
		//startNextTimer();
		//trigger memory
	}

	public float getTimer() {
		return clusterInformations [currentCluster - 1].getTimer ();
	}

	public void startNextTimer() {
		clusterInformations [currentCluster - 1].initialise();
		warned = false;
	}

	private void checkPort() {
		if (Vector3.Distance (character.gameObject.transform.position, killLights[killLight - 1].transform.position) < 5.0f) {
			character.intraclusterMove(killLights[killLight-1].gameObject, clusterInformations[currentCluster-1].gameObject);
			memController.StartMemory(currentMemory);
			GetComponent<AudioController>().playSound("Memory" + (currentMemory+1));
			currentMemory++;
			startNextTimer();
		}
	}
}
