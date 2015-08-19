using UnityEngine;
using System.Collections;

/*
 * Controls the free radicals that are attached to the neuron that this script is attached to
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 */
public class FreeRadicalController : MonoBehaviour {

	//speed at which the free radicals orbit the neuron
	private float orbitSpeed = 90f;
	//references to the free radicals themselves
	public GameObject[] freeRadicals;
	//reference to the character as a free radical attractor
	public RadicalAttractor character;
	//if the free radicals are orbitting
	private bool orbiting = true;
	//reference to the emitter controller
	public EmitterController emitters;
	//number of the neuron this script is connected to based on the number within the cluster
	public int thisNeuron;


	void Start() {
		foreach (GameObject g in freeRadicals) {
			g.GetComponentInChildren<Animator>().SetBool("benign", true);
		}
	}

	/*
	 *  For each orbit, attached to neuron, so this.transform is parent neuron
	 */
	void Update () {
		//if the neurons free radicals are orbiting
		if (orbiting) {
			foreach (GameObject g in freeRadicals) {
				g.transform.position = RotatePointAroundPivot (
				g.transform.position, transform.position, Quaternion.Euler (
				orbitSpeed * Time.deltaTime, orbitSpeed * Time.deltaTime, 0));
			}

			//if the neuron this script is on is the current neuron
			//prime free radicals
			if (emitters.getCurrentEmitter()-1 == thisNeuron) {
				orbiting = false;
				//Debug.Log ("turned off orbit!");
				//Prime the radicals!
				character.setRadicalSet(freeRadicals);
			}
		}
	}

	/*
	 * rotates a vector 3 around another vector 3 at a given angle
	 * for this, it is used to orbit the free radicals around the neurons
	 */
	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle) {
		return angle * ( point - pivot) + pivot;
	}
}