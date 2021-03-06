﻿using UnityEngine;
using System.Collections;

public class FauxGravityBody : MonoBehaviour {

	public FauxGravityAttractor attractor;
	public Transform myTransform;

	void Start() {
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		GetComponent<Rigidbody>().useGravity = false;
		myTransform = transform;
	}

	void Update() {
		attractor.Attract (myTransform);
	}
}
