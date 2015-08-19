using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

	public AudioSource gameAudio; //set from the inspector, this plays the audio
	//ALL AUDIO NEEDS TO BE NORMALISED IN THE INSPECTOR
	public AudioClip memory1;
	public AudioClip memory2;
	public AudioClip memory3;
	public AudioClip memory4;
	public AudioClip memory5;
	public AudioClip soundtrack;
	public AudioClip hospital1;
	public AudioClip hospital2;
	public float volume;//set this in the inspector.
	private static bool hasStarted;

	void Start() {
		if(!hasStarted) {
			GetComponent<AudioController>().playSound("Hospital2");
			hasStarted = true;
		}
	}

	//You'll need to have a referecnce to this script in the other scripts that need to access it.
	//It's best that the sounds are called by the scripts as and when they need them, and the function takes a
	//string that then plays a one shot of the appropriate sound.
	public void playSound(string sound) {
		switch (sound) {
		case "Memory1":
			gameAudio.PlayOneShot(memory1, volume);
			break;
		case "Memory2":
			gameAudio.PlayOneShot(memory2, volume);
			break;
		case "Memory3":
			gameAudio.PlayOneShot(memory3, volume);
			break;
		case "Memory4":
			gameAudio.PlayOneShot(memory4, volume);
			break;
		case "Memory5":
			gameAudio.PlayOneShot(memory5, volume);
			break;
		case "Soundtrack":
			gameAudio.PlayOneShot(soundtrack, volume);
			break;
		case "Hospital1":
			gameAudio.PlayOneShot(hospital1, volume);
			break;
		case "Hospital2":
			gameAudio.PlayOneShot(hospital2, volume);
			break;
		default:
			Debug.Log("No sound found");
			break;
		}
	}
}
