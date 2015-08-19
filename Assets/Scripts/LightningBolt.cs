/*
	This script is placed in public domain. The author takes no responsibility for any possible harm.
	Contributed by Jonathan Czeck

	This script is from the Unity Asset store, and while it has been edited somewhat, we did not write it (i.e please excuse from marking. We don't want to reinve
*/
using UnityEngine;
using System.Collections;

public class LightningBolt : MonoBehaviour
{
	public Transform target;
	private Vector3 originPoint;
	//public Transform signal;
	public int zigs = 100;
	public float speed = 1f;
	public float scale = 1f;
	public Light startLight;
	public Light endLight;
	public GameObject character;
	private bool set;
	public AcceptorController acceptors;
	public GameObject camera;
	private bool dropped;
	private GameObject setEmitter;
	
	Perlin noise;
	float oneOverZigs;
	
	private Particle[] particles;
	
	void Start()
	{
		//character = GameObject.Find ("Animation");
		oneOverZigs = 1f / (float)zigs;
		GetComponent<EllipsoidParticleEmitter>().emit = false;

		GetComponent<EllipsoidParticleEmitter>().Emit(zigs);
		particles = GetComponent<EllipsoidParticleEmitter>().particles;
		originPoint = target.transform.position;
	}
	
	void Update ()	{
		if (noise == null)
			noise = new Perlin();
			
		float timex = Time.time * speed * 0.1365143f;
		float timey = Time.time * speed * 1.21688f;
		float timez = Time.time * speed * 2.5564f;
		
		for (int i=0; i < particles.Length; i++)
		{
			Vector3 position = Vector3.Lerp(transform.position, target.position, oneOverZigs * (float)i);
			Vector3 offset = new Vector3(noise.Noise(timex + position.x, timex + position.y, timex + position.z),
										noise.Noise(timey + position.x, timey + position.y, timey + position.z),
										noise.Noise(timez + position.x, timez + position.y, timez + position.z));
			position += (offset * scale * ((float)i * oneOverZigs));
			
			particles[i].position = position;
			particles[i].color = Color.white;
			particles[i].energy = 1f;
		}
		
		GetComponent<ParticleEmitter>().particles = particles;
		
		if (GetComponent<ParticleEmitter>().particleCount >= 2)
		{
			if (startLight)
				startLight.transform.position = particles[0].position;
			//if (endLight)
			//	endLight.transform.position = particles[particles.Length - 1].position;
		}

		if (Vector3.Distance (target.position, character.transform.position) < 5.0f && !set && !dropped) {
			float distToCamera = Vector3.Distance (character.transform.position, camera.transform.position);
			if (distToCamera < 18f && distToCamera > 2f) {
				camera.transform.position = Vector3.MoveTowards (camera.transform.position, character.transform.position, -10.0f * Time.deltaTime);
			}

			acceptors.attract ();
			character.GetComponentInChildren<RadicalAttractor> ().attractRadicalSet ();

			foreach (GameObject g in acceptors.getAllowedAcceptors()) {
				if (Vector3.Distance (g.transform.position, character.transform.position) < 5.0f) {
					set = true;
					setEmitter = g;
					target.transform.position = setEmitter.transform.position;
					acceptors.signalConnection (int.Parse (g.name.TrimStart ("SignalLight".ToCharArray ())));
					return;
				}
			}
			target.transform.position = character.transform.position;
			//set the fact that the charater has the electricity
			character.GetComponent<RadicalAttractor> ().electricityActive (true);

			if (character.GetComponent<RadicalAttractor> ().isHit ()) {
				character.GetComponent<Swim> ().disable (1.0f);
				character.GetComponent<RadicalAttractor> ().temporarilyDisable (2);
				dropped = true;
			}
		} else if (dropped) {
			target.transform.position = Vector3.MoveTowards (target.transform.position, originPoint, 8.0f * Time.deltaTime);
			if (Vector3.Distance (target.position, character.transform.position) < 5.0f 
				&& !character.GetComponent<Swim> ().isDisabled ()) {
				dropped = false;
			}
		} else if (set) {
			target.transform.position = setEmitter.transform.position;
		}
	}	
}