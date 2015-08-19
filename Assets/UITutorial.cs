using UnityEngine;
using System.Collections;
using UnityEngine.UI;



//This method has an array of tutorial sprites that are set in the UI
//drag them in, and away we go.
public class UITutorial : MonoBehaviour {
	
	public float timeBetweenInstructions;//the amount of time an instruction will displauy for
	public Image currentlyDisplayed;//the referenced image currently displayed
	public Sprite [] instructions; //the list of instructions
	
	// Use this for initialization
	void Start () {
		StartCoroutine (WaitAndUpdate (timeBetweenInstructions));     
	}
	
	//waits for a number of seconds then updates the sprite on the screen
	IEnumerator WaitAndUpdate(float timeBetweenInstructions) {
		
		for (int i = 0; i< instructions.Length; i++) {
			yield return new WaitForSeconds (timeBetweenInstructions);
			if (i == instructions.Length-1) {
				Destroy(currentlyDisplayed);//doesn't work at the moment
				currentlyDisplayed.enabled = false;        //neither does this
			} else {
				currentlyDisplayed.sprite = instructions [i];
			}
		}
	}
}