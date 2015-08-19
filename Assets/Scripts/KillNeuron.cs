using UnityEngine;
using System.Collections;


//@jim authored this
/*This class decides whether a neuron lives or dies, based on the random chance assigned at the beginning.
 If it dies,it slowly transitions between textures useing the blendfader.
 It also has the capacity to play an audio clip at a 3D point in space */
public class KillNeuron : MonoBehaviour {
	
	
	//public float chanceToLive = 0.9999f;
	public float blendSpeed; //the speed of the blend (not in seconds, but 0.5 is a good fade speed)
	private float fade = 0;//starting fade amount (0!)
	public float chanceToLiveInPercent; //set this for a chance to live, 0.9 for a 90% chance to live, 0.1 for a 10% chance to live
	public float timeToDie;//this is the length of time before the neuron dies (if it's going to die).
	//public AudioSource
	//public AudioClip deathSound;//this is the sound that the neuron makes when it dies
	private bool isDead;
	private bool countdownCompleted;
	private Material shade; //material for the shader to change
	
	
	
	//calculates the chance to die
	//if the chance to die is greater than the chance to live
	//we're dead
	void Start() {
		chanceToLiveInPercent = chanceToLiveInPercent / 100f; //turn it back into something readable between 0-1
		float die = Random.value;
		if (die > chanceToLiveInPercent) {
			isDead = true;
			kill();
		}
		GetComponent<Renderer>().material.SetFloat("_Blend", 1f);
	}
	
	
	//if the neuron is dead and the countdown has completed, blend between the textures AKA die
	void Update() {
		if (isDead && countdownCompleted) {
			fade += Time.deltaTime * blendSpeed;
			GetComponent<Renderer>().material.SetFloat("_Blend", fade);
		}
	}
	
	//generates a random float, between 0 and the timeToDie and starts the timer for that long
	private void kill() {
		float howLongToDie = Random.value * timeToDie;
		StartCoroutine (Countdown (howLongToDie));
		
		//can playclipatSource here to trigger the death sound if needed
		//start countdown timer
		
	}
	
	
	//counts down and then sets the bool to true, so the die sequence can begin in earnest on update
	IEnumerator Countdown(float timer) {
		yield return new WaitForSeconds (timer);
		countdownCompleted = true;
	}
	
	//countdown timer
	//once timer is complete
	
}
