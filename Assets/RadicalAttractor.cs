using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadicalAttractor : MonoBehaviour {

	private List<GameObject> primedAttractees = new List<GameObject>();
	private List<GameObject> currentAttractees = new List<GameObject>();
	private float attractionSpeed = 1f;
	public GameController controller;
	private int disableUntil;
	private bool disabled;
	private bool hit;
	private bool electricityIsActive;

	// Update is called once per frame
	void Update () {
		if (currentAttractees.Count > 0 && !disabled) {
			foreach (GameObject g in currentAttractees) {
				attractCurrentRadical (g);
			}
		} else if(disabled){
			if(disableUntil >=(int)Time.time) {
				foreach(GameObject g in currentAttractees) {
					g.GetComponent<SignalLightController>().blink();
					g.GetComponent<SignalLightController>().setColor(Color.blue);
				}
			} else {
				foreach(GameObject g in currentAttractees) {
					g.GetComponent<SignalLightController>().switchOn();
					g.GetComponent<SignalLightController>().setColor(Color.red);
				}
				disabled = false;
			}
		}
	}

	public void temporarilyDisable(int time) {
		disableUntil = (int)Time.time + time;
		disabled = true;
	}

	//Release the radicals on chase
	public void attractRadicalSet() {
		foreach (GameObject g in primedAttractees) {
			currentAttractees.Add (g);
			g.GetComponent<SignalLightController>().switchOn();
			//making attraction slightly faster for each radical following
			attractionSpeed += 0.01f;
			//Debug.Log("Attraction speed now: " + attractionSpeed);
			g.GetComponentInChildren<Animator>().SetBool("chasing", true);
		}
		//clear primed list
		primedAttractees = new List<GameObject>();
	}

	//Primes the radicals, ready for attack when character is electrified
	public void setRadicalSet(GameObject[] radicals) {
		foreach (GameObject g in radicals) {
			primedAttractees.Add(g);
			g.GetComponent<Light>().color = Color.red;
			g.GetComponent<SignalLightController>().blink();
			g.GetComponentInChildren<Animator>().SetBool("angered", true);
		}
	}

	private void attractCurrentRadical(GameObject radical) {
		//speed is a function so that the more radicals being attracted, the faster they become
		//Use attractionSpeed variable here to flatten out speed function slope instead of 0.5f
		float speed = (currentAttractees.Count * 0.5f) * Time.deltaTime;
		radical.transform.position = Vector3.MoveTowards(radical.transform.position, transform.position, speed);
		radical.GetComponentInChildren<Animator>().SetBool("chasing", true);
	}

	public void killRadicals() {
		foreach (GameObject g in currentAttractees) {
			g.GetComponent<Light>().color = Color.blue;
			g.AddComponent<Rigidbody>();
		}
		currentAttractees = new List<GameObject>();
		controller.signifyClusterChange();
	}

	void OnCollisionEnter(Collision col) {
		if(!disabled && electricityIsActive) {
			if(col.gameObject.tag.Equals("FreeRadical")) {
				killSingle(col.gameObject);
				hit = true;
			}
		}
	}

	void OnCollisionExit(Collision col) {
		if (col.gameObject.tag.Equals ("FreeRadical")) {
			hit = false;
		}
	}

	public void killSingle(GameObject rad) {
		rad.GetComponent<Light>().color = Color.blue;
		rad.AddComponent<Rigidbody>();
		currentAttractees.Remove(rad);
	}

	public bool isHit() {
		return hit;
	}

	public void electricityActive(bool active) {
		electricityIsActive = active;
	}
}
