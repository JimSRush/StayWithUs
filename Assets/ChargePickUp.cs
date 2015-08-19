using UnityEngine;
using System.Collections;

public class ChargePickUp : MonoBehaviour {

	public PowerCharges characterCharges;

	void OnCollisionEnter() {
		characterCharges.increment();
		gameObject.SetActive (false);
	}
}
