using UnityEngine;
using System.Collections;

/* Jim wrote this. Randomly seeds neuron objects in a three given spaces,
each space will take different poly neurons.
	inner: takes higher poly & animated
	middle: takes lower poly & static
	outer: takes lowest poly and static

 */

public class RandomSeed : MonoBehaviour {
	
	public GameObject closeNeuron;
	public GameObject middleNeuron;
	public GameObject farNeuron;
	public int noOfCloseNeurons;
	public int noOfMiddleNeurons;
	public int noOfFarNeurons;
	private float radius;//the radius o 
	private Bounds outside; //the bounds of the outside sphere
	private Bounds inner;
	private Bounds middle;

	// Use this for initialization
	void Start () {

		//set radii/bounds of the three spheres
		
		GameObject skySphere = GameObject.Find ("SkySphere");
		outside = skySphere.GetComponent<Renderer> ().bounds;
		middle = new Bounds(Vector3.zero, outside.size/4);
		inner = new Bounds(Vector3.zero, outside.size/8);
		radius = outside.extents.magnitude;



		//Iterate through and generate neurons for each bounding box	
		for (int i = 0; i < noOfCloseNeurons; i++) {
			Instantiate(closeNeuron, GeneratePositionInSmallSphere(), Random.rotation);
		}

		for (int i = 0; i < noOfMiddleNeurons; i++) {
			Instantiate(middleNeuron, GeneratePositionInMiddleSphere(), Random.rotation);
		}

		for (int i = 0; i < noOfFarNeurons; i++) {
			Instantiate(farNeuron, GeneratePositionInLargeSquare(), Random.rotation);
		}
	}
	

	//ToDO:
	//Currently there is no bounds checking for to make sure that the middle neurons aren't in the small and that the large aren't in the middle.
	//Logic needs to be:
	//	generate position in bounds of small/middle/large
	//	check for overlap.
	//	if overlapping, regenerate
	//not sure on the best way to do this, as all methods will need to return a vector3d.
	//I tried using a bool and a while loop to continue to generate points and only return one if it's in bounds
	//however unity complains if there's no guaranteed return path (i.e always have to return a vector).

	
	/*These methods randomly generates a position inside of a radius of a sphere*/
	Vector3 GeneratePositionInSmallSphere() {
		//returns a position in the radius of the sphere
		return Random.insideUnitSphere * inner.extents.magnitude;
	}
	
	Vector3 GeneratePositionInMiddleSphere() {
		//returns a position in the radius of the sphere
		Vector3 tryVector = Random.insideUnitSphere * middle.extents.magnitude;

		while (inner.Contains(tryVector)) {
			tryVector = Random.insideUnitSphere * middle.extents.magnitude;
		}
		return tryVector;
	}
	
	Vector3 GeneratePositionInLargeSquare() {
		//returns a position in the radius of the sphere
		Vector3 tryVector = Random.insideUnitSphere * outside.extents.magnitude / 1.8f;

		while(middle.Contains(tryVector) || inner.Contains(tryVector)) {
			tryVector = Random.insideUnitSphere * outside.extents.magnitude / 1.8f;
		}
		return tryVector;
	}


	//debugging to show the size of the bounds
	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (Vector3.zero, outside.extents.magnitude/1.8f);	
		Gizmos.DrawWireSphere (Vector3.zero, middle.extents.magnitude);
		Gizmos.DrawWireSphere (Vector3.zero, inner.extents.magnitude);
	}
}