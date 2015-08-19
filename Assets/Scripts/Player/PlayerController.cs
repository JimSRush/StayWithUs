using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 15f;
	private float rotateSpeed = 10.0f;
	private Vector3 moveDir;
	private bool porting = false;
	private int onNeuronNo = 1;

	// Update is called once per frame
	void Update () {
		moveDir = new Vector3(0, 0, Input.GetAxisRaw ("Vertical")).normalized;
		//moveDir = new Vector3(Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
	}

	void FixedUpdate() {

		GetComponent<Rigidbody> ().MovePosition (GetComponent<Rigidbody> ().position + transform.TransformDirection (moveDir) * moveSpeed * Time.deltaTime);
		transform.Rotate (0, Input.GetAxis ("Horizontal") * rotateSpeed, 0);
		GameObject wayp = GameObject.Find("Sphere");
		GameObject wayp2 = GameObject.Find("Sphere1");
		
		//BARE IN MIND THIS CODE IS DUMBBBB AND WILL BE WAY BETTER<<< ITS JUST HACKY FOR NOW

		switch (onNeuronNo) {
			case 1:
				wayp = GameObject.Find ("Sphere");
				wayp2 = GameObject.Find ("Sphere1");
				break;
			case 2:
				wayp = GameObject.Find ("Sphere2");
				wayp2 = GameObject.Find ("Sphere3");
				break;
			case 3:
				wayp = GameObject.Find ("Sphere4");
				wayp2 = GameObject.Find ("Sphere5");
				break;
			case 4:
				wayp = GameObject.Find ("Sphere6");
				wayp2 = GameObject.Find ("Sphere7");
				break;
		}

		float dist1 = Vector3.Distance (transform.position, wayp.transform.position);
		float dist2 = Vector3.Distance (transform.position, wayp2.transform.position);

		if (dist1 < 1.0 || porting) {
			porting = true;
			Destroy (this.GetComponent (typeof(FauxGravityBody)));
			moveTo (wayp2);
			if (dist2 < 2.0) {
				porting = false;
				this.gameObject.AddComponent<FauxGravityBody> ();
				FauxGravityBody newGrav = (FauxGravityBody)this.gameObject.GetComponent (typeof(FauxGravityBody));

				switch (onNeuronNo) {
				case 1:
					newGrav.attractor = (FauxGravityAttractor)GameObject.Find ("neuron2").GetComponent (typeof(FauxGravityAttractor));
					onNeuronNo = 2;
					break;
				case 2:
					newGrav.attractor = (FauxGravityAttractor)GameObject.Find ("neuron3").GetComponent (typeof(FauxGravityAttractor));
					onNeuronNo = 3;
					break;
				case 3:
					newGrav.attractor = (FauxGravityAttractor)GameObject.Find ("neuron4").GetComponent (typeof(FauxGravityAttractor));
					onNeuronNo = 4;
					break;
				case 4:
					newGrav.attractor = (FauxGravityAttractor)GameObject.Find ("neuron3").GetComponent (typeof(FauxGravityAttractor));
					onNeuronNo = 3;
					break;
				}
			}
		}
	}

	void moveTo(GameObject p) {
		Vector3 dir = p.transform.position - transform.position;
		Vector3 move = dir.normalized * (moveSpeed*4) * Time.deltaTime;
		transform.position = transform.position + move;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
	}
}
