using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class MemoryController : MonoBehaviour {
	public GameObject [] memories;
	public AudioClip [] memorySounds;
	private int currentMemory = 0; 
	private NoiseAndScratches script;
	public Camera camera;
	private bool active;
	private float flickerTime;


	// Use this for initialization
	void Start () {
		//turn off the noise
		script = camera.GetComponent<NoiseAndScratches>();
		script.enabled = false;
		//iterate through and set game objects to not active for safety
		for (int i = 0; i < memories.Length; i++) {
			memories[i].SetActive(false);
		}


		//script.enabled = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			memories [currentMemory].transform.position = new Vector3 (memories [currentMemory].transform.position.x, memories [currentMemory].transform.position.y, 
		        memories [currentMemory].transform.position.z - 1f);
			if (flickerTime > Time.time) {
				if (Random.value > 0.9) {
					script.enabled = true;
					return;
				} else {
					script.enabled = false;
					return;
				}
			}
			script.enabled = true;
		}
	}


	//This script controls the memory activation.
	//It calls the appropriate coroutine (sound, audio)
	public void StartMemory(int memory){
		StartCoroutine (MemoryOn (memory)); //start us up
	}

	//Activates the memory & noise, deactivates after ten seconds
	IEnumerator MemoryOn(int memory) {
		currentMemory = memory;
		active = true;
		memories [memory].SetActive (true);
		memories [memory].transform.position = new Vector3(memories [memory].transform.position.x, memories [memory].transform.position.y, 
		                                                   memories [memory].transform.position.z - 50f);
		script.enabled = true;
		flickerTime = Time.time + 3.0f;
		yield return new WaitForSeconds(10);
		memories [memory].SetActive (false);
		script.enabled = false;
		active = false;
	}
}
