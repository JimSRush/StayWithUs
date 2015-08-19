using UnityEngine;
using System.Collections;

/* Jim wrote this. Randomly seeds neuron objects in a given space*/

public class RandomSeedTran : MonoBehaviour {

	public GameObject neuron;
	public Light light;
	public int noNeurons;
	public int noCharges;
	private float radius;//the radius o 
	public float size;
	public bool forTransition = false;
	// Use this for initialization
	void Start () {

		GameObject skySphere = GameObject.Find ("SkySphere");
		radius = skySphere.GetComponent<Renderer>().bounds.extents.magnitude-1000f;

		//get radius of sky sphere
		for (int i = 0; i < noNeurons; i++) {
			Instantiate(neuron, GeneratePosition(), Random.rotation);
			//Instantiate(FreeRadica?
		}
		for (int i = 0; i < noCharges; i++) {
			Instantiate(light, GeneratePosition(), Random.rotation);
		}
	}
	
	/*This method randomly generates a position inside of a radius of a sphere*/
	Vector3 GeneratePosition() {
		if (!forTransition) {
			return Random.insideUnitSphere * radius / size;
		} else { 
			Vector2 tubeRound = Random.insideUnitCircle * 200f;
			return new Vector3(tubeRound.x, tubeRound.y, Random.Range(-radius,radius));
		}
	}

}