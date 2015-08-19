using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag.Equals("Waypoint")) {
			FauxGravityAttractor attractor;
			GameObject neuron2 = GameObject.Find("neuron2");
			attractor = (FauxGravityAttractor) neuron2.GetComponent(typeof(FauxGravityAttractor));
			attractor.Attract(this.gameObject.transform);
		}
	}
}
