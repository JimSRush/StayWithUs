using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Sticky : MonoBehaviour {
	
	private float moveSpeed = 2; // move speed
	private float turnSpeed = 90;
	private float lerpSmooth = 8f; // smoothing speed
	private float gravity = 100f; // gravity acceleration
	//private bool isGrounded;
	//private float deltaGround = 2f; // character is grounded up to this distance - WAS 0.02f
	private Vector3 surfaceNormal; // current surface normal
	private Vector3 myNormal; // character normal
	//private float distGround; // distance from character position to ground
	public Rigidbody rb;
	private Transform myTransform;
	public BoxCollider boxCollider;
	private Animator an;
	private bool intraporting = false;
	private bool interporting = false;
	private float BezierTime = 0;
	private float arcTime = 6f;
	//private int flightAccesses = 0;
	private float rotateSpeed = 5.0f;
	private GameObject currentOpposingWaypoint;
	private bool arrived;
	public int currentNeuron = 1;
	//private bool recovering = false;
	//private bool correctingFall;
	
	
	private void Start(){
		myNormal = transform.up; 
		myTransform = this.transform;
		rb.freezeRotation = true; // stop us from spinning
		//distGround = boxCollider.extents.y - boxCollider.center.y; //how far to the ground?
		an = GetComponent<Animator> ();
	}
	
//	private void FixedUpdate(){
//		if(!porting) {
//			rb.AddForce (-gravity * rb.mass * myNormal);
//		}
//		//force on my head
//	}
	
	private void FixedUpdate(){
		if (!isPorting()) {
			rb.AddForce (-gravity * rb.mass * myNormal);
		}

		//change this all to collision detect because this is intensiveish
		foreach (GameObject p in GameObject.FindGameObjectsWithTag("IntraPoint")) {
			//just arrived, dont resend back!
			if (arrived) {
				if (Vector3.Distance (transform.position, currentOpposingWaypoint.transform.position) > 1.0f) {
					currentOpposingWaypoint = null;
					arrived = false;
					doRayCast ();
				}
				break;
			}

			//hit waypoint
			if (Vector3.Distance (transform.position, p.transform.position) < 1.0f || intraporting) {
				if (!intraporting) {
					currentOpposingWaypoint = findOpposingWaypoint (p);
				}

				if(currentOpposingWaypoint != null) {
					intraporting = true;
					intraclusterMove (p, currentOpposingWaypoint);
				} else {
					break;
				}

				//arrived at destination
				if (currentOpposingWaypoint != null && 
					Vector3.Distance (transform.position, currentOpposingWaypoint.transform.position) < 1.0f) {
					arrived = true;
					intraporting = false;
					BezierTime = 0;
					arcTime = 12f;
					doRayCast ();
				}
				return;
			}
		}

		foreach(GameObject p in GameObject.FindGameObjectsWithTag("InterPoint")) {
				//just arrived, dont resend back!
				if (arrived) {
					if (Vector3.Distance (transform.position, currentOpposingWaypoint.transform.position) > 1.0f) {
						currentOpposingWaypoint = null;
						arrived = false;
						doRayCast ();
					}
					break;
				}
				
				//hit waypoint
				if (Vector3.Distance (transform.position, p.transform.position) < 1.0f || interporting) {
					if (!interporting) {
						currentOpposingWaypoint = findOpposingWaypoint (p);
					}
					
					if(currentOpposingWaypoint != null) {
						interporting = true;
						interclusterMove (p, currentOpposingWaypoint);
					} else {
						break;
					}
					
					//arrived at destination
					if (currentOpposingWaypoint != null && 
					    Vector3.Distance (transform.position, currentOpposingWaypoint.transform.position) < 1.0f) {
						arrived = true;
						interporting = false;
						doRayCast ();
					}
					return;
				}
		}
		doRayCast ();
	}

	void doRayCast() {
		float h = Input.GetAxis ("Horizontal"); //get input
		float v = Input.GetAxis ("Vertical"); 
		Ray ray;
		RaycastHit hit;
		// movement code - turn left/right with Horizontal axis:
		myTransform.Rotate(0, h*turnSpeed*Time.deltaTime, 0);
		// update surface normal and isGrounded:
		ray = new Ray(myTransform.position, -myNormal); // cast ray downwards
		if (Physics.Raycast(ray, out hit)){ 
			//isGrounded = hit.distance <= distGround + deltaGround;
			surfaceNormal = hit.normal;
		}

		Debug.Log ("raycasting");
		Animating (h, v);
		myNormal = Vector3.Lerp (myNormal, surfaceNormal, lerpSmooth * Time.deltaTime);
		// find forward direction with new myNormal:
		Vector3 myForward = Vector3.Cross (myTransform.right, myNormal);
		// align character to the new myNormal while keeping the forward direction:
		Quaternion rot = Quaternion.LookRotation (myForward, myNormal);
		myTransform.rotation = Quaternion.Lerp (myTransform.rotation, rot, lerpSmooth * Time.deltaTime);
		myTransform.Translate (0, 0, v * moveSpeed * Time.deltaTime);
		//Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);

	}
	
	void Animating(float h, float v) {
		bool walking = h != 0f || v != 0f;
		an.speed = 2;
		an.SetBool ("IsWalking", walking);
	}
	
	void intraclusterMove(GameObject first, GameObject second) {
		intraporting = true;
		GameObject waypArc = GameObject.Find("BridgeArc1");
		Vector3 startPoint = first.transform.position;
		Vector3 controlPoint = waypArc.transform.position;
		Vector3 endPoint = second.transform.position;
		float CurveX; 
		float CurveY;
		float CurveZ;  
		
		//if(Vector3.Distance(second.transform.position, transform.position) < 50.0f) {
		//	arcTime = arcTime + 0.2f;
		//}
		
		BezierTime = BezierTime + (Time.deltaTime/arcTime);
		
		if (BezierTime >= 1){
			BezierTime = 0;
		}
		
		CurveX = (((1-BezierTime)*(1-BezierTime)) * startPoint.x) + (2 * BezierTime * (1 - BezierTime) * controlPoint.x) + ((BezierTime * BezierTime) * endPoint.x);
		CurveY = (((1-BezierTime)*(1-BezierTime)) * startPoint.y) + (2 * BezierTime * (1 - BezierTime) * controlPoint.y) + ((BezierTime * BezierTime) * endPoint.y);
		CurveZ = (((1-BezierTime)*(1-BezierTime)) * startPoint.z) + (2 * BezierTime * (1 - BezierTime) * controlPoint.z) + ((BezierTime * BezierTime) * endPoint.z);
		Vector3 dir = second.transform.position - transform.position;
		transform.position = new Vector3(CurveX, CurveY, CurveZ);
		//having both rotates gives a look over shoulder effect
		transform.Rotate (0, Input.GetAxis ("Horizontal") * rotateSpeed, 0);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
	}	

	void interclusterMove(GameObject first, GameObject second) {
		interporting = true;
		Vector3 dir = second.transform.position - transform.position;
		Vector3 move = dir.normalized * (moveSpeed*4) * Time.deltaTime;
		transform.position = transform.position + move;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), 5 * Time.deltaTime);
	}

	private GameObject findOpposingWaypoint(GameObject start) {
		if (start.name.Equals ("Waypoint1")) {
			//GameObject gameObject = GameObject.Find ("Waypoint1");
			//gameObject.GetComponent<ParticleSystem> ().Play ();
			//Debug.Log (gameObject.name);
			Debug.Log ("Flying to Wayp 2");
			currentNeuron = 2;
			return GameObject.Find ("Waypoint2");
			//} else if (start.name.Equals ("Waypoint2")) {
			//	Debug.Log ("Flying to Wayp 1");
			//	currentNeuron = 1;
			//	return GameObject.Find ("Waypoint1");
		} else if (start.name.Equals ("Waypoint3")) {
			Debug.Log ("Flying to Wayp 4");
			currentNeuron = 3;
			return GameObject.Find ("Waypoint4");
			//} else if (start.name.Equals ("Waypoint4")) {
			//	Debug.Log ("Flying to Wayp 3");
			//	currentNeuron = 3;
			//	return GameObject.Find ("Waypoint3");
		} else if (start.name.Equals ("Waypoint5")) {
			Debug.Log ("Flying to Wayp 6");
			currentNeuron = 4;
			return GameObject.Find ("Waypoint6");
			//} else if (start.name.Equals ("Waypoint6")) {
			//	Debug.Log ("Flying to Wayp 5");
			//	currentNeuron = 5;
			//	return GameObject.Find ("Waypoint5");
		} else if (start.name.Equals ("Waypoint7")) {
			Debug.Log ("Flying to Wayp 8");
			currentNeuron = 5;
			return GameObject.Find ("Waypoint8");
			//} else if (start.name.Equals ("Waypoint8")) {
			//	Debug.Log ("Flying to Wayp 7");
			//	currentNeuron = 7;
			//	return GameObject.Find ("Waypoint7");
		} else if (start.name.Equals ("Waypoint9")) {
			Debug.Log ("Flying to Wayp 10");
			currentNeuron = 6;
			return GameObject.Find ("Waypoint10");
		} else {
			return null;
		}
	}

//	public void recoverPosition(GameObject recoverPoint) {
//		recovering = true;
//		GetComponent<Rigidbody>().detectCollisions = false;
//		GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
//		GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
//		GetComponent<Rigidbody>().Sleep ();
//		Vector3 dir = recoverPoint.transform.position - transform.position;
//		Vector3 move = dir.normalized * (moveSpeed*4) * Time.deltaTime;
//		transform.position = transform.position + move;
//		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), 5 * Time.deltaTime);
//	}
//
	public bool isPorting() {
		return interporting || intraporting;
	}
//
//	public void isRecovering(bool recovering) {
//		this.recovering = recovering;
//	}
}
