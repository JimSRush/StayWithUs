using UnityEngine;
using System.Collections;

public class JoggingScript : MonoBehaviour {
	public float maxHeight = 11f;
	public float minHeight = 10f;
	private float currentHeight;
	public float speed;
	public GameObject activateCarTrigger;
	public GameObject landingSite;
	private bool hasTriggered;
	public float distanceTrigger = 1f;
	public float hitForce;
	public GameObject car;
	private bool goingUp = true;
	private Vector3 hitFacing;
	public AudioSource leftFoot;//audio sounds for feet
	public AudioSource rightFoot;
	public AudioSource carSkid;
	public AudioSource combinedEnd;
	private bool switchFoot;
	private bool hasHit;
	//private CarScript carScript;
	public GameObject carsBoundPoint;
	private bool landed;
	public GameObject[] landedLampPostComponents;
	private int timeWatch;
	private bool closedEyes;
	private bool playingEnd;
	private bool belowGround;
	public GameObject fallThroughPoint;
	public GameObject endScene;
	private bool readyToGo;

	void Start() {
		//transform.LookAt (activateCarTrigger.transform.position); //face the waypoint
		hitFacing = Vector3.up;
		//carScript = car.GetComponent<CarScript>();
		car.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		//skip open scene
		if (Input.GetButton ("Submit")) {
			AutoFade.LoadLevel("Main", 1, 3, Color.white);
		}

		//have we triggered/has the car started/activated in the scene?
		if (hasTriggered && !hasHit) { //find the car position
			float distanceToGuy = Vector3.Distance (transform.position, car.transform.position);
			moveCar();

			if(distanceToGuy >= distanceTrigger) {
				hasHit = true;
			}
		} else if (!hasTriggered && !hasHit) {
			//check to see whether we've reached the waypoint, and if we have set it active in the scene
			float distanceToWaypoint = Vector3.Distance (transform.position, activateCarTrigger.transform.position);
			//Debug.Log (distanceToWaypoint);
			if(distanceToWaypoint < distanceTrigger + 10) {
				carSkid.Play ();
			}
			if (distanceToWaypoint < distanceTrigger) {
				car.SetActive (true);
				hasTriggered = true;
			} 
			jog (); //it's ok. we can keep jogging. Jog on m8.
		} else {
			KnockOver();
			moveCar();
		}
	}

	//bobs the camera up and down and moves it along the waypoint
	private void jog() {

		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + speed);//this is our forward movement, and remains constant hence out of the if block
		currentHeight = transform.position.y;

		if (!goingUp) { //if we're going down or still not at the bottom
			transform.position = new Vector3(transform.position.x, (currentHeight - ((maxHeight-minHeight)* 0.1f)), transform.position.z); //keep going down
			if(transform.position.y <= minHeight){
				goingUp=true; //if we've hit the bottom, we have to go back up
				if(switchFoot) {
					leftFoot.Play();
					switchFoot = false;
				} else {
					rightFoot.Play ();
					switchFoot = true;
				}

			}
			//needs to go down
		} else { //we're going up 
			transform.position = new Vector3(transform.position.x, (currentHeight + ((maxHeight-minHeight)* 0.1f)), transform.position.z);
			if(transform.position.y >=maxHeight) {
				goingUp = false; //we have to go back doooown
			}
			//needs to go up
		}
		//move forward
		//move up and down
	}

	private void moveCar() {
		float distanceToEnd = Vector3.Distance (carsBoundPoint.transform.position, car.transform.position);
		if (distanceToEnd >= distanceTrigger) {
			Vector3 dir = carsBoundPoint.transform.position - car.transform.position;
			Vector3 move = dir.normalized * (hitForce * 4) * Time.deltaTime;
			car.transform.position = car.transform.position + move;
			car.transform.rotation = Quaternion.Slerp (car.transform.rotation, Quaternion.LookRotation (carsBoundPoint.transform.position), 5 * Time.deltaTime);
		}
	}

	private void KnockOver(){
		//change camera angle by 90 degrees
		if (Vector3.Distance (transform.position, landingSite.transform.position) > 4.0f && !landed) {
			Vector3 dir = landingSite.transform.position - activateCarTrigger.transform.position;
			Vector3 move = dir.normalized * (hitForce * 4) * Time.deltaTime;
			transform.position = transform.position + move;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (hitFacing), 5 * Time.deltaTime);
		} else {
			if(!landed){
				//this.gameObject.AddComponent<Rigidbody> ();
				//this.GetComponent<Rigidbody>().velocity = Vector3.zero;
				//this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
				timeWatch = (int)Time.time;
				landed = true;
			} else {
				eyeCloseOpen();
			}
		}
	}

	private void eyeCloseOpen() {
		if (timeWatch + 6 < Time.time && !closedEyes) {
			foreach (GameObject g in landedLampPostComponents) {
				g.SetActive (false);
			}
			closedEyes = true;
		} else if (timeWatch + 10 < Time.time && closedEyes && !playingEnd) {
			combinedEnd.Play ();
			playingEnd = true;
		} else if (timeWatch + 10 < Time.time && closedEyes && playingEnd) {
			foreach (GameObject g in landedLampPostComponents) {
				g.SetActive (true);
			}
			if (timeWatch + 15 < Time.time) {
				fall ();
			}
		}

		if (readyToGo) {
			Debug.Log (Time.time);
			if (timeWatch + 48 < (int)Time.time) {
				Debug.Log ("Got to end");
				AutoFade.LoadLevel ("Main", 1, 3, Color.white);
			}
		}
	}

	private void fall() {
		if (Vector3.Distance (transform.position, fallThroughPoint.transform.position) > 1.0f && !readyToGo) {
			Vector3 dir = fallThroughPoint.transform.position - transform.position;
			Vector3 move = dir.normalized * Time.deltaTime;
			transform.position = transform.position + move;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (hitFacing), 1 * Time.deltaTime);
		} else {
			if(!readyToGo) {
				this.gameObject.AddComponent<Rigidbody> ();
				readyToGo = true;
//			}
//			Debug.Log ("helooo");
//			if(timeWatch + 20 < (int)Time.time) {
//				Debug.Log("Got to end");
//				AutoFade.LoadLevel("Main",1,3,Color.white);
			}
		}
	}
}
