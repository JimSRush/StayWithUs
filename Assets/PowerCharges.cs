using UnityEngine;
using System.Collections;

public class PowerCharges : MonoBehaviour {

	private static int charges = 2;
	private ParticleSystem particles;

	void Start() {
		particles = GetComponent<ParticleSystem>();
		particles.maxParticles = charges;
	}

	public void increment() {
		charges++;
		updateParticles ();
	}

	public void decrement() {
		charges--;
		updateParticles ();
	}

	public int get() {
		return charges;
	}

	public void set(int num) {
		charges = num;
		updateParticles ();
	}

	private void updateParticles() {
		particles.maxParticles = charges;
	}
}
