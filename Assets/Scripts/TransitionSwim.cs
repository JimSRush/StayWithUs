using UnityEngine;
using System.Collections;

public class TransitionSwim : MonoBehaviour {

	public bool reverseStrafe = false;
	private float strafeSpeed = 20.0f;
	public Camera cam;
	private float viewHeight;
	private float viewWidth;
	
	// Use this for initialization
	void Start () {
		//cam = Camera.main;
		viewHeight = (2f * cam.orthographicSize) -4f;
		viewWidth = (viewHeight * cam.aspect)/2;
	}

	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal"); //get input
		float v = Input.GetAxis ("Vertical");
		
		if (reverseStrafe) {
				transform.Translate ((h * strafeSpeed * Time.deltaTime) * -1, 0, 0);
		} else {
				transform.Translate (h * strafeSpeed * Time.deltaTime, 0, 0);
		}
		transform.Translate (0, v * (strafeSpeed * Time.deltaTime) * 1, 0);

		transform.Translate (0, 0, 50 * Time.deltaTime);

		if (transform.position.z > 800f || transform.position.y > Mathf.Abs(200) 
		    || transform.position.x > Mathf.Abs(200)) {
			AutoFade.LoadLevel("MainLevel2", 2, 2, Color.white);
		}
	}

	void OnCollisionEnter() {
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
	}

	void OnCollisionExit() {
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
	}
}
