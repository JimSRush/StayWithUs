using UnityEngine;
using System.Collections;

/*
 * Jus for testing use - resets character to nearest neuron when fallen off
 */
public class SafetyNet : MonoBehaviour {

	//private Transform returnPoint1, returnPoint2, returnPoint3, returnPoint4;
	//private GameObject balloonCatch1, balloonCatch2, balloonCatch3, balloonCatch4;
	//private bool fixing1, fixing2, fixing3, fixing4;
	//private GameObject player;

	// Use this for initialization
//	void Start () {
//		returnPoint1 = GameObject.Find ("center1").transform;
//		returnPoint2 = GameObject.Find ("center2").transform;
//		returnPoint3 = GameObject.Find ("center3").transform;
//		returnPoint4 = GameObject.Find ("center4").transform;
//		balloonCatch1 = GameObject.Find ("balloonCatch1");
//		balloonCatch2 = GameObject.Find ("balloonCatch2");
//		balloonCatch3 = GameObject.Find ("balloonCatch3");
//		balloonCatch4 = GameObject.Find ("balloonCatch4");
		//player = GameObject.Find ("Animation");
	//}

//	void FixedUpdate() {
//		Sticky sticky = (Sticky)player.GetComponent (typeof(Sticky));
//		if (sticky != null && !sticky.isPorting()) {
//			if (sticky.currentNeuron == 1) {
//				if (Vector3.Distance (player.transform.position, balloonCatch1.transform.position) > 20f || fixing1) {
//					fixing1 = true;
//					if(Vector3.Distance (player.transform.position, balloonCatch1.transform.position) > 1.0f) {
//						sticky.recoverPosition(balloonCatch1);
//					} else {
//						fixing1 = false;
//						sticky.isRecovering(false);
//					}
//
//					//player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
//					//player.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
//					//player.GetComponent<Rigidbody>().Sleep();
//					//player.transform.position = returnPoint1.position;
//					//player.transform.Rotate(returnPoint2.eulerAngles);
//				}
//			} else if (sticky.currentNeuron == 2) {
//				if (Vector3.Distance (player.transform.position, balloonCatch2.transform.position) > 20f || fixing2) {
//					fixing2 = true;
//					if(Vector3.Distance (player.transform.position, balloonCatch2.transform.position) > 1.0f) {
//						sticky.recoverPosition(balloonCatch2);
//					} else {
//						fixing2 = false;
//						sticky.isRecovering(false);
//					}
//					//player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
//					//player.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
//					//player.GetComponent<Rigidbody>().Sleep();
//					//player.transform.position = returnPoint2.position;
//					//player.transform.Rotate(returnPoint2.eulerAngles);
//				}
//			} else if (sticky.currentNeuron == 3) {
//				if (Vector3.Distance (player.transform.position, balloonCatch3.transform.position) > 20f || fixing3) {
//					fixing3 = true;
//					if(Vector3.Distance (player.transform.position, balloonCatch3.transform.position) > 1.0f) {
//						sticky.recoverPosition(balloonCatch3);
//					} else {
//						fixing3 = false;
//						sticky.isRecovering(false);
//					}
//					//player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
//					//player.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
//					//player.GetComponent<Rigidbody>().Sleep();
//					//player.transform.position = returnPoint3.position;
//					//player.transform.Rotate(returnPoint2.eulerAngles);
//				}
//			} else if (sticky.currentNeuron == 4) {
//				if (Vector3.Distance (player.transform.position, balloonCatch4.transform.position) > 20f || fixing4) {
//					fixing4 = true;
//					if(Vector3.Distance (player.transform.position, balloonCatch4.transform.position) > 1.0f) {
//						sticky.recoverPosition(balloonCatch4);
//					} else {
//						fixing4 = false;
//						sticky.isRecovering(false);
//					}
//					//player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
//					//player.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
//					//player.GetComponent<Rigidbody>().Sleep();
//					//player.transform.position = returnPoint4.position;
//					//player.transform.Rotate(returnPoint2.eulerAngles);
//				}
//			}
//		}
//	}
}
