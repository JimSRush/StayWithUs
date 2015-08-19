using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
	
	public float speed = 5f;
	public float rot = 5f; //rotation speed
	public Sprite pill;
	public List <GameObject> inventory = new List< GameObject>(); //inventory!
	public Text inventoryText;
	public List <Sprite> inventoryUI;
	public int currentInv = 0;
	public int listPos = 0; //position of the list in the inventory to drop

	//Vector3 movement = new Vector3(0,0,0);
	Animator an;

	void Awake () {
		an = GetComponent<Animator> ();

	}


	void FixedUpdate() {
		CharacterController controller = GetComponent<CharacterController>();
		float h = Input.GetAxis ("Horizontal"); //get input
		float v = Input.GetAxis ("Vertical"); 

		if (Input.GetKey(KeyCode.E)) { //drops item
			DropItem ();
		}

	
		float currentSpeed = speed * v; //calculate the current speed based on the vertical input 

		transform.Rotate (0, h * rot, 0); //rotate the player along the horizontal 
		Animating (h, v); //Boolean check for state machine.
		Vector3 f = transform.TransformDirection(Vector3.forward); //forward vector
		Move (f, controller, currentSpeed);//moveeeeee

	}
	
	
	//Factoring out in case of expansion/adding footsteps or whatever
	void Move(Vector3 forward, CharacterController controller, float currentSpeed) {
		controller.SimpleMove(forward * currentSpeed);
	}



	//This function updates the statemachine, and sets the animating state to IsWalking 
	void Animating(float h, float v) {
		bool walking = h != 0f || v != 0f;
		an.speed = 2;
		an.SetBool ("IsWalking", walking);

	}


	//Puts the item back where it came from
	void DropItem() {

		if (inventory.Count == 0) {
			return;
		} else {
			GameObject toDrop = inventory [listPos]; //returns it to the scene
			toDrop.SetActive (true); 
			inventoryText.text = "You now have " + inventory.Count + " pills."; //update UI, currently not working
			listPos++;
		}

	}
	//Trigger listener, taken from Roll A Ball tutorials.

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Item") {

			other.gameObject.SetActive(false);
			inventory.Add(other.gameObject);
			inventoryUI.Add(pill); //trying to add inventory sprite here, but no go
			inventoryText.text = "You now have " + inventory.Count + " pills.";
			

			//http://docs.unity3d.com/ScriptReference/Object.Instantiate.html // use this to put them back in the scene
		}
	}
}
	


