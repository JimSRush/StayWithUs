using UnityEngine;
using System.Collections;

/*
 * Any light called "WayLight" followed by a sequential number only, with an end travel point
 * called "LightEndPoint" followed by a sequential number only can be added to the scene
 * and given this script to move towards endpoint and reset to its own start point in a loop
 * 
 */
public class WayLightMover : MonoBehaviour {

	public float moveSpeed = 5.0f;
	private int totalNoOfLights = 60;
	private ArrayList lightsStartPos;

	void Start() {
		lightsStartPos = new ArrayList();
		for (int i = 1; i <= totalNoOfLights; i++) {
			lightsStartPos.Add (GameObject.Find ("WayLight" + i.ToString()).transform.position);
		}
	}
	// Update is called once per frame
	void Update () {
		//THEO - im actually very proud of myself for this bit of code which is
		//(24*2)-2 times faster than the last code
		//
		//FUTURE THEO - Its actually kinda shit lmao
		int light = int.Parse(gameObject.name.TrimStart("WayLight".ToCharArray()));
		moveTo (GameObject.Find ("LightEndPoint" + light.ToString()), (Vector3)lightsStartPos[light - 1]);
	}

	void moveTo(GameObject p, Vector3 startPoint) {
		float distToEnd = Vector3.Distance (transform.position, p.transform.position);
		
		if(distToEnd < 1.0f) {
			transform.position = startPoint;
		} else {
			Vector3 dir = p.transform.position - transform.position;
			Vector3 move = dir.normalized * (moveSpeed * 4) * Time.deltaTime;
			transform.position = transform.position + move;
		}
	}
}
