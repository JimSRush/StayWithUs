using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * Updates the UI based on the information of a current cluster
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 */ 
public class UIUpdate : MonoBehaviour {
	//time left until neuron cluster dies
	public float timer = 90f;
	//TEMP: the text object showing time left
	public Text timerText;
	//if cluster complete
	private bool complete;
	//if cluster dead
	private bool dead;
	private bool initialised;

	/*
	 * sets the timer text
	 */ 
	void Start() {
		//timerText = GameObject.Find("Canvas").GetComponentInChildren<Text>();
	}

	/*
	 * Iterates the timer on screen, and checks if the cluster is dead based on the time left
	 * or complete based on the boolean
	 */ 
	void Update(){
		if (!complete && !dead && initialised) {
			timer -= Time.deltaTime;
			float t = Mathf.Abs (timer);
			float seconds = t % 60;
			float minutes = t / 60;
			string minSec = formatTimeString ((int)minutes, (int)seconds);
			;
			if (timer <= 0f) {
				minSec = "Neuron Cluster Dead!"; 
				dead = true;
			}
			//timerText.text = minSec;
		} else if(complete && !dead) {
			//timerText.text = "Cluster Complete!";
		}
	}

	/*
	 * TEMP: formats the time into a string the is more user friendly
	 */ 
	private string formatTimeString(int minutes, int seconds) {
		string returnString = "";

		if (minutes < 10) {
			returnString = returnString + "0" + minutes + " minutes ";
		} else {
			returnString = returnString + minutes + " minutes ";
		}

		if(seconds < 10) {
			returnString = returnString + "0" + seconds + " seconds remaining";
		} else {
			returnString = returnString + seconds + " seconds remaining";
		}

		return returnString;
	}

	/*
	 * Call this to say that the cluster was completed successfully
	 */ 
	public void alertComplete() {
		complete = true;
	}

	/*
	 * Call this to add time onto the timer 
	 */
	public void addToTimer(int time) {
		timer += time;
	}

	/*
	 * getter for the time float
	 */ 
	public float getTimer() {
		return timer;
	}

	public void initialise() {
		initialised = true;
	}
}
