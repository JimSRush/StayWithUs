using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/*
 * 
 * The controller script placed on "Acceptors" group of a neuron. It controls the activity when 
 * the character has the electricity and they are looking for an acceptor to place it.
 * 
 *  Authors: Theodore Cruickshank, Jim Rush
 * 
 */
public class AcceptorController : MonoBehaviour {

	//number of acceptors in neural cluster
	//public static int totalAcceptors = 5;
	//reference to the character
	public Swim character;
	//reference to each of the acceptor objects
	public List<GameObject> acceptors = new List<GameObject>();
	//reference to each of the electricity emitters
	public List<SignalLightController> lightControllers = new List<SignalLightController>();
	//reference to the lights which enliven a neuron (makes it look as though its sparking)
	public List<ElectricalCharge> dendriteCharges = new List<ElectricalCharge>();
	//an array for finding which acceptors are connected
	private List<bool> connectedAcceptors = new List<bool>();
	//number of currently connected neurons
	private int currentlyConnected = 0;
	//stating the neuron from which the character grabbed the electricity
	public int starterNeuron = 1;
	//reference to the electricity emitter script 
	public EmitterController emitters;
	//TEMPORARY REFERENCE TO UI BRAIN SPRITES FOR BETA PRES
	public Sprite[] brains;
	public Image brainPerc;

	// switches all signalling lights off, because there is an electric charge to collect
	void Start () {
		foreach(SignalLightController l in lightControllers) {
			l.switchOff();
		}

		foreach (GameObject g in acceptors) {
			connectedAcceptors.Add(false);
		}
	}

	/* LOOK AT CHANGING THIS TO HAVE A FULL LIST THAT
	 * JUST REMOVES AS YOU GO
	 * CURRENTLY LIKE THIS INCASE WE WANT TO CHANGE MECHANIC
	 * OR ALLOW INGAME RESETTING ETC
	 *
	 * returns a collection of acceptors that are allowed to signal the player to connect
	 * will not signal the player to a acceptor if it is:
	 * 
	 *	already connected
	 *  the acceptor on the same neuron
	 *	the acceptor on the starter neuron.
	 *
	 *  this means that the neurons can only be connected in a total loop, and can never 
	 * form a disconnected graph.
	 */
	public List<GameObject> getAllowedAcceptors() {
		List<GameObject> allowed = new List<GameObject>();

		//if last neuron on the block, allow starter acceptor.
		if (currentlyConnected == (acceptors.Count - 1)) {
			allowed.Add(acceptors[starterNeuron-1]);
			return allowed;
		}

		//allow:
		//non connected acceptors;
		//acceptors not on same neuron;
		//acceptors not from first neuron in chain.
		for (int i = 0; i < connectedAcceptors.Count; i++) {
			if ((i + 1) != emitters.getCurrentEmitter() 
			    && !connectedAcceptors[i]
			    	&& (i + 1) != starterNeuron) {
				allowed.Add(acceptors[i]);
			}
		}
		return allowed;
	}

	// signals this script that an electrical charge has been connected to one
	// of the acceptors by the character
	public void signalConnection(int number) {
		//sets the acceptor as connected
		connectedAcceptors[number - 1] = true;
		//signals the electrical charge on the just connected neuron to emit
		emitters.setCurrentEmitter(number);
		//increments the number of neurons connected in the network
		currentlyConnected++;
		//turns the current neurons electrical charge on
		lightControllers [number - 1].switchOn ();
		//totally enliven the neuron that was just connected
		dendriteCharges[number-1].enliven(1.0f);
		//TEMP UI FOR BETA PRES
		//if(number == 1) {
		//	brainPerc.sprite = brains[5];
		//} else { 
		//	brainPerc.sprite = brains[number - 1];
		//}

		//turns off blinking of all unattached acceptor lights
		hide();

		if (readyToPort()) {
			//TEMPORARY CODE (The cluster2 bit lol)
			//character.intraclusterMove(acceptors[number-1], GameObject.Find("Cluster2"));

			//kill radicals!
			character.GetComponentInParent<RadicalAttractor>().killRadicals();
		}
	}

	/* Turns of all attractors that are able to accept the electrical charge from the
	 * character
	 */
	public void attract() {
		//if the player is on the last neuron to collect, its attractor
		//is the only on that need to be turned on
		if(currentlyConnected == (acceptors.Count - 1)) {
			lightControllers[starterNeuron - 1].blink();
			return;
		}

		//attract player by blinking all allowed attracter lights
		for (int i = 0; i < lightControllers.Count; i++) {
			if((i + 1) != emitters.getCurrentEmitter() 
				&& !connectedAcceptors[i]) {
					lightControllers[i].blink();
			} else {
				//makes lights solid on if they are connected
				lightControllers[i].switchOn();
			}
		}
			//always dissallow first acceptor if we are not at end of chain
			lightControllers[starterNeuron - 1].switchOff();
	}

	/*turns off all attractor lights that are not connected, eg stops all 
	* blinking attractors
	*/
	private void hide() {
		for (int i = 0; i < acceptors.Count; i++) {
			if(!connectedAcceptors[i]) {
				lightControllers[i].switchOff();
			}
		}
	}

	/*returns true if all acceptors are connected, and ready to port to next
	* cluster
	*/
	public bool readyToPort() {
		return currentlyConnected == acceptors.Count;
	}
}
