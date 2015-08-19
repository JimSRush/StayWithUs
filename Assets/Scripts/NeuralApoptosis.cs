using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * To create the visual representation of the neurons decaying
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 */
public class NeuralApoptosis : MonoBehaviour {

	//uncomment these for when we have visual representations
	//public GameObject[] neurons;
	//private bool[] saved = new bool[]();
	public float clusterTimeToCompleteInMillis = 90000f;
	public float startTime;
	private bool hasStarted;

	// Update is called once per frame
	void Update () {
		//MAYBE GET COLORS? FROM TROY DO CHANGE HERE
		if (hasStarted && Time.time - startTime <= clusterTimeToCompleteInMillis) {
			//MAYBE GET COLORS? FROM TROY DO CHANGE HERE
			//for(int i = 0; i < neurons.Length; i++) {
			//	if(!saved[i]) {
					//Material material = new Material
					//neurons[i].GetComponent<MeshRenderer>().material = 
			//	}
			//}


		}
	}

	public void startApoptosis() {
		startTime = Time.time;

	}

	//for when we have visual represeent
	public void saveNeuron(int number) {
		//saved[number - 1] = true;

	}
}
