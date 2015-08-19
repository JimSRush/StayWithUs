using UnityEngine;
using System.Collections;

public class RotateMe : MonoBehaviour {

	public Renderer ren;
	private bool isRotating = false;

	// Use this for initialization
	void Start () {
		ren = GetComponent<Renderer>();
		float chanceToSpin = Random.value;

		//Debug.Log ("Random value is" + chanceToSpin);
		if (chanceToSpin > 0.9f) {
			isRotating = true;
		}

		//select a random number/chance to rotate
		//call update function

	}
	
	// Update is called once per frame
	void Update () {
		if (isRotating) {
			Rotate();
		}
	}


	void Rotate() {
		transform.Rotate (new Vector3 (1, 0, 0) * Time.deltaTime); //smoooth
	}


	//This is an abandoned method where I was trying to increase them in size & position when picked up, but abandoned.
	void PickUp() {
		float size = 1.0f;
		while (size <=2) {
			transform.localScale += new Vector3 (0.01F, 0.01F, 0.01F);
			//size = size + 0.01;
		}
	}






}
