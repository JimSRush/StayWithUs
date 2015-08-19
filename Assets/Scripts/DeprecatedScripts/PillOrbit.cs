using UnityEngine;
using System.Collections;

public class PillOrbit : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 10);
	}
}
