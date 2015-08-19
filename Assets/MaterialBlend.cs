using UnityEngine;
using System.Collections;

/*
 * Blends materials of the skyshere based on the time left to complete the current cluster
 * 
 * Authors: Theodore Cruickshank, Jim Rush
 */ 
public class MaterialBlend : MonoBehaviour {
	//total timer time
	private float totalTime;
	//material of the renderer
	private Material shade; 
	//time left for neuron cluster
	private float timer;
	//reference to the game controller
	public GameController controller;
	private float lerpTime;

	// Use this for initialization
	void Start () {
		shade = GetComponent<Renderer>().material;
		totalTime = controller.getTimer();
		timer = totalTime;
	}
	
	// Update is called once per frame
	void Update () {
		//is greater than enough here?
		if (controller.getTimer () > timer) {
			lerpTime += 1.0f * Time.deltaTime;
			timer = Mathf.Lerp (timer, controller.getTimer(), lerpTime);
		} else {
			timer = controller.getTimer ();
		}
		float blendPerc = (totalTime - timer) / totalTime;
		shade.SetFloat ("_Blend", blendPerc);
	}
}
