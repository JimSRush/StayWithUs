using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * 
 * Script to control the electrical charge lights placed on the neuron asset dendrites
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 * 
 */
public class ElectricalCharge : MonoBehaviour {

	//number of dentrites on neuron
	public static int noOfDendrites = 8;
	//Zap speed of lights
	public float chargeSpeed = 2.0f;
	//references to the dendrite parent objects which should be dragged in in the Unity UI
	public GameObject[] dentrites = new GameObject[noOfDendrites];
	//array of which dedrites are active
	public bool[] activeCharges = new bool[noOfDendrites];
	//to allow procedural generation of neuron lights
	public bool randomiseActive = true;
	//to set the chance of more or less active dendrites being during randomisation
	private float activeChance = 0.05f;
	//two lights per dendrite plus one as the light counts start at 1 rather than 0
	private Vector3[] startPoints = new Vector3[(noOfDendrites * 2)+1];

	/*
	 * Sets up which dendrites are active on the neuron and sets the start point vectors of the lights
	 * attached to each active dendrite.
	 * 
	 * Note: if we want to make the switching on and off of dendrites dynamic, factor this code out into a method,
	 * call it here, and call it every time a change is made to the booleans. Turn back on all lights at the start of the method. 
	 * Also unstaticise the noOfDendrites variable and create the other arrays which require it in this start method.
	 */
	void Start () {
		//if it is set to randomise the liveliness of the neurons, envilen with this randomness
		if (randomiseActive) {
			enliven(activeChance);
		}

		//otherwise set them all on
		for (int i = 0; i < noOfDendrites; i++) {
			foreach (Component c in dentrites[i].GetComponentsInChildren<Component>()) {
				GameObject g = c.gameObject;
			
				if (g.tag.Equals ("Charge")) {
					//set the origin points on the light tracks
					startPoints [int.Parse (g.name.TrimStart ("Charge".ToCharArray ()))] = g.transform.position;
				}
			}
		}
	}

	//called every frame
	void Update () {
		for(int i = 0; i < noOfDendrites; i++) {
			if(activeCharges[i]) {
				//there are two lights per dendrite, referenced here
				GameObject dendriteTip = null;
				GameObject light1 = null;
				GameObject light2 = null;

				foreach(Component c in dentrites[i].GetComponentsInChildren<Component>()) {
					GameObject g = c.gameObject;

					if(g.tag.Equals("DendriteTip")) {
						dendriteTip = g;
					} else if(g.tag.Equals ("Charge")){
						if(light1 == null) {
							light1 = g;
						} else {
							light2 = g;
						}
					}
				}

				//startPoint index is called based on the integer in the variable name of the light which is named
				//beginning with "Charge"
				//
				//move both of the dendrite lights to the towards the tip
				moveTo(light1, dendriteTip, startPoints[int.Parse(light1.name.TrimStart("Charge".ToCharArray()))]);
				moveTo(light2, dendriteTip, startPoints[int.Parse(light2.name.TrimStart("Charge".ToCharArray()))]);

			}
		}
	}

	//enliven neurons by chance given as float value, 
	//0f is no activity
	//1f is full activity
	public void enliven(float chance) {
		int tempInt = 0;
		for(int i = 0; i < noOfDendrites; i++) {
			if(Random.value < chance) {
				tempInt++;
				activeCharges[i] = true;

				//turn on all used lights
				foreach(Component c in dentrites[i].GetComponentsInChildren<Component>()) {
					GameObject g = c.gameObject;
					
					if(g.tag.Equals("Charge")) {
						g.GetComponentInChildren<Light>().enabled = true;
					}
				}
			} else {
				activeCharges[i] = false;
				
				//turn off all unused lights
				foreach(Component c in dentrites[i].GetComponentsInChildren<Component>()) {
					GameObject g = c.gameObject;
					
					if(g.tag.Equals("Charge")) {
						g.GetComponentInChildren<Light>().enabled = false;
					}
				}
			}
		}
		//Debug.Log ("Enlivened: " + tempInt);
	}

	/*
	 * Light argument is progressed closer to tip per call to this method, or if too close to tip,
	 * is reset to its original position.
	 */
	void moveTo(GameObject light, GameObject tip, Vector3 lightStartPoint) {
		float distToEnd = Vector3.Distance (light.transform.position, tip.transform.position);
		
		if(distToEnd < 1.0f) {
			//reset position to start if the light is at the tip of the dendrite
			light.transform.position = lightStartPoint;
		} else {
			//move light towards end
			Vector3 dir = tip.transform.position - light.transform.position;
			Vector3 move = dir.normalized * (chargeSpeed * 4) * Time.deltaTime;
			light.transform.position = light.transform.position + move;
		}
	}
}
