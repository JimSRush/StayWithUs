using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

	//Helper method, so I can see the darn waypoints :-)
	void OnDrawGizmos() {

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, 0.5f);
	}
}

