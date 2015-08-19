using UnityEngine;
using System.Collections;

public class Swim : MonoBehaviour {

	public float strafeSpeed = 90f;
	public float moveSpeed = 6f;
	private float oldMoveSpeed;
	public int rotateSpeed = 10;
	public float friction = 2f;
	public float lerpSpeed = 2;
	public bool reverseStrafe = false;
	public bool reverseMouse = false;
	private float xDeg = 0f;
	private float yDeg = 0f;
	private bool porting;
	private float bezierTime = 0;
	private float arcTime = 6f;
	private GameObject currentTarget = null;
	private GameObject currentOrigin = null;
	private int boostTime;
	private int startTimeBoost;
	private bool boosting;
	private bool disabled;
	private float disabledUntil;
	public bool mouseHeld = false;
	private PowerCharges charges;
	private float oldV;
	private bool moved;
	public PlayerLight playerLight;

	// Use this for initialization
	void Start () {
		oldMoveSpeed = moveSpeed;
		charges = GetComponent<PowerCharges>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void FixedUpdate () {

		if (!porting && !disabled) {
			float h = Input.GetAxis ("Horizontal"); //get input
			float v = Input.GetAxis ("Vertical");

			if (reverseStrafe) {
				transform.Translate ((h * strafeSpeed * Time.deltaTime) * -1, 0, 0);
			} else {
				transform.Translate (h * strafeSpeed * Time.deltaTime, 0, 0);
			}

			if(v == oldV) {
				GetComponentInChildren<Animator>().SetBool("Swim", false);
			} else {
				GetComponentInChildren<Animator>().SetBool("Swim", true);
			}

			oldV = v;
			transform.Translate (0, 0, v * moveSpeed * Time.deltaTime);

			if (reverseMouse) {
				xDeg -= Input.GetAxis ("Mouse X") * rotateSpeed * friction;
				yDeg += Input.GetAxis ("Mouse Y") * rotateSpeed * friction;
			} else {
				xDeg += Input.GetAxis ("Mouse X") * rotateSpeed * friction;
				yDeg -= Input.GetAxis ("Mouse Y") * rotateSpeed * friction;
			}

			Quaternion fromRotation = transform.rotation;
			Quaternion toRotation = Quaternion.Euler (yDeg, xDeg, 0);
			transform.rotation = Quaternion.Lerp (fromRotation, toRotation, Time.deltaTime * lerpSpeed);

			if (boosting) {
				if (startTimeBoost + boostTime >= (int)Time.time) {
					moveSpeed = oldMoveSpeed * 2;
				} else {
					moveSpeed = oldMoveSpeed;
					boosting = false;
					playerLight.speedOff();
				}
			}

			if(Input.GetMouseButtonDown(0) && !mouseHeld){
				if(charges.get() > 0) {
					GetComponent<RadicalAttractor>().temporarilyDisable(2);
					charges.decrement();
					mouseHeld = true;
					playerLight.attackOn();
					return;
				}
			}

			if(Input.GetMouseButtonDown(1) && !mouseHeld) {
				if(charges.get () > 0) {
					boost(10);
					charges.decrement();
					mouseHeld = true;
					return;
				}
			}

			mouseHeld = false;

			//THIS IS TEMP SPEED BOOSTTING ON MOUSE RIGHT OR SPAC BAR FOR TESTING
			//Space bar is input mapped to "Jump" by default in the input settings
			//if (Input.GetMouseButton (1) || Input.GetButton ("Jump")) {
				//moveSpeed = oldMoveSpeed * 3;
				//code to make swim on mouse only
				//helpful for recording play videos for jim!
				//transform.Translate (1 * strafeSpeed * Time.deltaTime, 1 * moveSpeed * Time.deltaTime, 0);
				//Time.timeScale = 0.2f;
			//} else if (!boosting) {
				//moveSpeed = oldMoveSpeed;
			//}
		} else if (porting) {
			intraclusterMove (currentOrigin, currentTarget);
		} else if (disabled) {
			if(Time.time > disabledUntil) {
				disabled = false;
			}
		}
	}

	void OnCollisionExit(Collision col) {
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}

	public void boost(int seconds) {
		boostTime = seconds;
		startTimeBoost = (int)Time.time;
		boosting = true;
		playerLight.speedOn(); 
	}

	public void intraclusterMove(GameObject first, GameObject second) {
		porting = true;
		currentOrigin = first;
		currentTarget = second;
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
		
		bezierTime = bezierTime + (Time.deltaTime/arcTime);
		
		if (bezierTime >= 1){
			bezierTime = 0;
		}
		
		CurveX = (((1-bezierTime)*(1-bezierTime)) * startPoint.x) + (2 * bezierTime * (1 - bezierTime) * controlPoint.x) + ((bezierTime * bezierTime) * endPoint.x);
		CurveY = (((1-bezierTime)*(1-bezierTime)) * startPoint.y) + (2 * bezierTime * (1 - bezierTime) * controlPoint.y) + ((bezierTime * bezierTime) * endPoint.y);
		CurveZ = (((1-bezierTime)*(1-bezierTime)) * startPoint.z) + (2 * bezierTime * (1 - bezierTime) * controlPoint.z) + ((bezierTime * bezierTime) * endPoint.z);
		Vector3 dir = second.transform.position - transform.position;
		transform.position = new Vector3(CurveX, CurveY, CurveZ);
		//having both rotates gives a look over shoulder effect
		//transform.Rotate (0, Input.GetAxis ("Horizontal") * rotateSpeed, 0);
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);

		//this isnt right somehow, fix it 
		//think its the tranformed diferections
		//becuase character moves on its y not x to go forward
		//because the assest is the old walking one on its side.
		transform.LookAt (currentTarget.transform.position);
		GetComponentInChildren<Animator>().SetBool ("Lurch", true);
		GetComponentInChildren<Animator>().SetBool ("Lurch", false);

		if(second.tag.Equals("End")) {
			if (Vector3.Distance (transform.position, endPoint) < 50.0f) {
				AutoFade.LoadLevel("Transition", 2, 2, Color.white);
			}
		}

		if (Vector3.Distance (transform.position, endPoint) < 20.0f) {
			porting = false;
			currentOrigin = null;
			currentTarget = null;
		}
	}	

	public void disable(float time) {
		disabledUntil = Time.time + time;
		disabled = true;
	}

	public bool isDisabled() {
		return disabled;
	}
}
